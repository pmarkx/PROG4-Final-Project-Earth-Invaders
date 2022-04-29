using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Models;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class GameLogic : IGameModel, IGameControl
    {

        public static Player ThePlayer { get; }
        public IMap Map { get; set; }
        private Directions lastMove = Directions.nowhere;

        

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

        public void GameTick()
        {
            foreach (var item in Map)
            {
                item.Tick();
            }
            if (lastMove!=Directions.nowhere)
            {
                Move(lastMove);
                lastMove = Directions.nowhere;
            }
            Map.CollisionDetect();
        }

        public void Move(Directions direction)
        {
            //(int X, int Y) = WhereAmI();
            //switch (direction)
            //{
            //    case Directions.up:
            //        if (X - 1 >= 0)
            //        {
            //            Map[X, Y].XPosition++;
            //        }
            //        break;
            //    case Directions.down:
            //        if (X + 1 < Map.GetLength(0))
            //        {
            //            Map[X, Y].XPosition--;
            //        }
            //        break;
            //    default:
            //        break;
            //}
            ThePlayer.Move(direction);
            
        }
        private (int X, int Y) WhereAmI()
        {
            return Map.IndexOf(x => x is Player);
        }
       

    }
}
