using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Models;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Logic
{
    public delegate void TickHappened();

    public class GameLogic : IGameModel, IGameControl
    {

        public static Player ThePlayer { get; }
        public IMap Map { get; set; }
        public TimeSpan GameTickInterval { get; set; }
        public TimeSpan EnemyMovementInterval { get; set; }

        private Directions lastMove = Directions.nowhere;
        private DispatcherTimer gameTimer;
        private DispatcherTimer enemyTimer;
        private bool enemyMoves = false;
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
            //Ezt a részt át lehetne vinni a Mapba de nem voltam biztos hogy szeretnétek.
            using StreamReader streamReader = new StreamReader("map.txt");
            var (MapsizeX, MapsizeY) = streamReader.ReadLine().Split(",") switch
            {
                var a => (int.Parse(a[0]), int.Parse(a[1])),
            };
            Map = new MapBackedByList(MapsizeX, MapsizeY);
            Map.PopulateMapFromStreamReader(streamReader, ThePlayer);

        }
        public void StartGame()
        {
            gameTimer = new DispatcherTimer();
            enemyTimer = new DispatcherTimer();
            gameTimer.Interval = GameTickInterval;
            enemyTimer.Interval = EnemyMovementInterval;
            gameTimer.Tick += GameTimer_Tick;
            enemyTimer.Tick += EnemyTimer_Tick;
            gameTimer.Start();
            enemyTimer.Start();
        }

        private void EnemyTimer_Tick(object? sender, EventArgs e)
        {
            enemyMoves = true;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if (enemyMoves)
            {
                foreach (var item in Map)
                {
                    item.Tick();
                }
                enemyMoves = false;
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

        //TODO: Create EnumWithActions
        public void Move(Directions direction)
        {
            lastMove = direction;

        }
        private void DoMove(Directions direction)
        {
            ThePlayer.Move(direction);

        }
        private (int X, int Y) WhereAmI()
        {
            return Map.IndexOf(x => x is Player);
        }


    }
}
