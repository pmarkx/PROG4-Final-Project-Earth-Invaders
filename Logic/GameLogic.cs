using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Models;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Timers;

namespace Logic
{
    public delegate void TickHappened();

    public class GameLogic : IGameModel, IGameControl
    {

        public static Player ThePlayer { get; }
        public IMap Map { get; set; }
        public TimeSpan GameTickInterval { get; set; }
        public TimeSpan EnemyMovementInterval { get; set; }
        public TimeSpan EnemySpawnInterval { get; set; }
        public TimeSpan BulletMoveInterval { get; set; }
        public TimeSpan ShootingBetweenInterval { get; set; }

        private Directions lastMove = Directions.nowhere;
        private Timer gameTimer;
        private Timer enemyTimer;
        private Timer enemySpawnTimer;
        private Timer bulletMoveTimer;
        private Timer shootingBetweenTimer;

        private bool enemyMoves = false;
        private bool enemySpawns = false;
        private bool bulletMoves = false;
        private bool canShoot = false;
        public event TickHappened GameTickHappened;


        private GameObject ConvertToEnum(char v, int x, int y)
        {
            switch (v)
            {
                case 'f': return new Floor(x, y);
                case 'm': return new Mine(x, y);
                case 'w': return new Wall(x, y);
                case 'e': return new Enemy(x, y);
                case 'p': return ThePlayer;
                default: throw new Exception("unknown character!");
            }
        }

        private Queue<string> levels;

        static GameLogic()
        {
            ThePlayer = new Player(0, 0, 3, 0);
        }
        public GameLogic()
        {
            using StreamReader streamReader = new StreamReader("map.txt");
            Map = new MapBackedByList(streamReader, ThePlayer);

        }
        public void StartGame()
        {
            gameTimer = new Timer();
            enemyTimer = new Timer();
            enemySpawnTimer = new Timer();
            bulletMoveTimer = new Timer();
            shootingBetweenTimer = new Timer();
            gameTimer.Elapsed += GameTimer_Tick;
            enemyTimer.Elapsed += EnemyTimer_Tick;
            enemySpawnTimer.Elapsed += EnemySpawnTimer_Tick;
            bulletMoveTimer.Elapsed += BulletMoveTimer_Tick;
            shootingBetweenTimer.Elapsed += ShootingBetweenTimer_Tick;
            CopyTimerIntervals();
            StartTimers();
        }

        private void ShootingBetweenTimer_Tick(object? sender, EventArgs e)
        {
            canShoot = true;
        }

        private void BulletMoveTimer_Tick(object? sender, EventArgs e)
        {
            bulletMoves = true;
        }

        private void EnemySpawnTimer_Tick(object? sender, EventArgs e)
        {
            enemySpawns = true;
        }
        public void RefreshTimers()
        {
            StopTimers();
            CopyTimerIntervals();
            StartTimers();
        }
        private void CopyTimerIntervals()
        {
            gameTimer.Interval = GameTickInterval.TotalMilliseconds;
            enemyTimer.Interval = EnemyMovementInterval.TotalMilliseconds;
            enemySpawnTimer.Interval = EnemySpawnInterval.TotalMilliseconds;
            bulletMoveTimer.Interval = BulletMoveInterval.TotalMilliseconds;
            shootingBetweenTimer.Interval = ShootingBetweenInterval.TotalMilliseconds;
        }

        private void StopTimers()
        {
            gameTimer.Stop();
            enemyTimer.Stop();
            enemySpawnTimer.Stop();
            bulletMoveTimer.Stop();
            shootingBetweenTimer.Stop();
        }
        private void StartTimers()
        {
            gameTimer.Start();
            enemyTimer.Start();
            enemySpawnTimer.Start();
            bulletMoveTimer.Start();
            shootingBetweenTimer.Start();
        }
        //TODO: Create EnumWithActions
        public void Move(Directions direction)
        {
            lastMove = direction;

        }

        private void EnemyTimer_Tick(object? sender, EventArgs e)
        {
            enemyMoves = true;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if (enemyMoves)
            {
                foreach (var item in Map.Where(x => x is Enemy))
                {
                    item.Tick();
                }
                enemyMoves = false;
                Map.CollisionDetect();
                Map.CheckDie();
            }
            if (bulletMoves)
            {
                foreach (var item in Map.Where(x => x is Mine))
                {
                    item.Tick();
                }
                bulletMoves = false;
                Map.CollisionDetect();
                Map.CheckDie();
            }
            foreach (var item in Map.Where(x => !(x is Mine) && !(x is Enemy)))
            {
                item.Tick();
            }

            if (enemySpawns)
            {
                Map.EnemyRushing();
                enemySpawns = false;
            }
            if (lastMove != Directions.nowhere)
            {
                DoMove(lastMove);
                lastMove = Directions.nowhere;
            }
            Map.CollisionDetect();
            Map.CheckDie();
            GameTickHappened?.Invoke();
        }


        private void DoMove(Directions direction)
        {
            ThePlayer.Move(direction);

        }
        private (int X, int Y) WhereAmI()
        {
            return Map.IndexOf(x => x is Player);
        }

        public void Shoot()
        {
            if (canShoot)
            {
                var (X, Y) = (ThePlayer.XPosition, ThePlayer.YPosition - 1);
                Map.SpawnSomething(new Mine(X, Y));
                canShoot = false;
            }
            
        }
    }
}
