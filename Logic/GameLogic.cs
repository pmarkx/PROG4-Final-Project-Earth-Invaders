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
        public TimeSpan LifeSpawnInterval { get; set; }
        public TimeSpan AmmoSpawnInterval { get; set; }

        public TimeSpan LifeMoveInterval { get; set; }
        public TimeSpan AmmoMoveInterval { get; set; }

        public bool GameOver { get; set; }

        public long Score { get; private set; }

        public int Life { get; private set; }

        public int Ammo { get; private set; }
        public bool IsPause { get; set; }

        private Constants.Directions lastMove = Constants.Directions.nowhere;
        private Timer gameTimer;
        private Timer enemyTimer;
        private Timer enemySpawnTimer;
        private Timer bulletMoveTimer;
        private Timer shootingBetweenTimer;
        private Timer lifeSpawnTimer;
        private Timer lifeMoveTimer;
        private Timer ammoSpawnTimer;
        private Timer ammmoMoveTimer;


        private bool enemyMoves = false;
        private bool enemySpawns = false;
        private bool bulletMoves = false;
        private bool canShoot = false;
        private bool lifeSpawn = false;
        private bool lifeMove = false;
        private bool ammoSpawn = false;
        private bool ammoMove = false;
        public event TickHappened GameTickHappened;
        public static int EnemyDied = 0;


        static GameLogic()
        {
            ThePlayer = new Player(0, 0, Constants.DefaultLifes, Constants.DefaultAmmo);
        }

        public GameLogic()
        {
            using StreamReader streamReader = new StreamReader("startMap.txt");

            Map = new Map(streamReader, ThePlayer);
            GameOver = false;
            IsPause = false;
            RepopulateScoreLifeAmmo(streamReader);
        }


        public void StartGame()
        {
            gameTimer = new Timer();
            enemyTimer = new Timer();
            enemySpawnTimer = new Timer();
            bulletMoveTimer = new Timer();
            shootingBetweenTimer = new Timer();
            lifeSpawnTimer = new Timer();
            lifeMoveTimer = new Timer();
            ammoSpawnTimer = new Timer();
            ammmoMoveTimer = new Timer();
            gameTimer.Elapsed += GameTimer_Tick;
            enemyTimer.Elapsed += EnemyTimer_Tick;
            enemySpawnTimer.Elapsed += EnemySpawnTimer_Tick;
            bulletMoveTimer.Elapsed += BulletMoveTimer_Tick;
            shootingBetweenTimer.Elapsed += ShootingBetweenTimer_Tick;
            lifeSpawnTimer.Elapsed += LifeSpawnTimer_Elapsed;
            lifeMoveTimer.Elapsed += LifeMoveTimer_Elapsed;
            ammoSpawnTimer.Elapsed += AmmoSpawnTimer_Elapsed;
            ammmoMoveTimer.Elapsed += AmmmoMoveTimer_Elapsed;
            CopyTimerIntervals();
            StartTimers();
        }

        private void AmmmoMoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ammoMove = true;
        }

        private void LifeMoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lifeMove = true;
        }

        public void Shoot()
        {
            if (canShoot && ThePlayer.Ammo >= 1)
            {
                ThePlayer.Ammo--;
                var (X, Y) = (ThePlayer.XPosition, ThePlayer.YPosition - 1);
                Map.SpawnSomething(new Mine(X, Y));
                canShoot = false;
            }

        }

        public void Save()
        {
            using StreamWriter streamWriter = new StreamWriter("quicksave.txt");
            Map.SaveState(streamWriter, this.Score, this.Life);

        }
        public void Load()
        {
            using StreamReader streamReader = new StreamReader("quicksave.txt");
            Map = new Map(streamReader, ThePlayer);
            GameOver = false;
            RepopulateScoreLifeAmmo(streamReader);
        }

        private void RepopulateScoreLifeAmmo(StreamReader streamReader)
        {
            Score = !streamReader.EndOfStream ? long.Parse(streamReader.ReadLine()) : Constants.DefaultScore;
            ThePlayer.Life = !streamReader.EndOfStream ? int.Parse(streamReader.ReadLine()) : Constants.DefaultLifes;
            ThePlayer.Ammo = !streamReader.EndOfStream ? int.Parse(streamReader.ReadLine()) : Constants.DefaultAmmo;
            ThePlayer.IsLive = ThePlayer.Life > 0;

        }
        private void LifeSpawnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lifeSpawn = true;
        }
        private void AmmoSpawnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ammoSpawn = true;
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
            lifeSpawnTimer.Interval = LifeSpawnInterval.TotalMilliseconds;
            ammoSpawnTimer.Interval = AmmoSpawnInterval.TotalMilliseconds;
            ammmoMoveTimer.Interval = AmmoMoveInterval.TotalMilliseconds;
            lifeMoveTimer.Interval = LifeMoveInterval.TotalMilliseconds;
        }

        private void StopTimers()
        {
            gameTimer.Stop();
            enemyTimer.Stop();
            enemySpawnTimer.Stop();
            bulletMoveTimer.Stop();
            shootingBetweenTimer.Stop();
            lifeSpawnTimer.Stop();
            ammoSpawnTimer.Stop();
            ammmoMoveTimer.Stop();
            lifeMoveTimer.Stop();
        }
        private void StartTimers()
        {
            gameTimer.Start();
            enemyTimer.Start();
            enemySpawnTimer.Start();
            bulletMoveTimer.Start();
            shootingBetweenTimer.Start();
            lifeSpawnTimer.Start();
            ammoSpawnTimer.Start();
            ammmoMoveTimer.Start();
            lifeMoveTimer.Start();
        }
        //TODO: Create EnumWithActions
        public void Move(Constants.Directions direction)
        {
            lastMove = direction;

        }

        private void EnemyTimer_Tick(object? sender, EventArgs e)
        {
            enemyMoves = true;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            lock (Map)
            {
                ObjectMove();
                ObjectSpawn();




                Map.CollisionDetect();
                Map.CheckDie();
                GameTickHappened?.Invoke();
                Score += 1;
                Life = ThePlayer.Life;
                Ammo = ThePlayer.Ammo;
                if (GameLogic.EnemyDied > 0)
                {
                    Score += GameLogic.EnemyDied * 300;
                    GameLogic.EnemyDied = 0;
                }
                if (!ThePlayer.IsLive)
                    GameOverTrigger();
            }
        }


        private void DoMove(Constants.Directions direction)
        {
            ThePlayer.Move(direction);

        }
        private (int X, int Y) WhereAmI()
        {
            return Map.IndexOf(x => x is Player);
        }
        private void GameOverTrigger()
        {
            StopTimers();
            GameOver = true;
        }
        private void ObjectMove()
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
            if (ammoMove)
            {
                foreach (var item in Map.Where(x => x is AmmoBox))
                {
                    item.Tick();
                }
                ammoMove = false;
                Map.CollisionDetect();
                Map.CheckDie();
            }
            if (lifeMove)
            {
                foreach (var item in Map.Where(x => x is LifeReward))
                {
                    item.Tick();
                }
                lifeMove = false;
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
            foreach (var item in Map.Where(x => !(x is Mine) && !(x is Enemy) && !(x is AmmoBox) && !(x is LifeReward)))
            {
                item.Tick();
            }
        }
        private void ObjectSpawn()
        {
            if (enemySpawns)
            {
                Map.EnemyRushing();
                enemySpawns = false;
            }
            if (lifeSpawn)
            {
                Map.LifeRewardRushing();
                lifeSpawn = false;
            }
            if (ammoSpawn)
            {
                Map.AmmoRewardRushing();
                ammoSpawn = false;
            }
            if (lastMove != Constants.Directions.nowhere)
            {
                DoMove(lastMove);
                lastMove = Constants.Directions.nowhere;
            }
        }

        public void Pause()
        {
            if (IsPause)
            {
                IsPause = false;
                gameTimer.Start();
            }
            else
            {
                IsPause=true;
                gameTimer.Stop();
            }
        }
    }
}
