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
    public class GameLogic
    {
        public static Player ThePlayer = new Player(0, 0, 3, 0);
        public enum Directions
        {
            up, down
        }
        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    Move(Directions.up);
                    break;
                case Key.Down:
                    Move(Directions.down);
                    break;
            }
        }
        Map Map { get; set; }
        public GameLogic()
        {
            //Ezt a részt át lehetne vinni a Mapba de nem voltam biztos hogy szeretnétek.
            using StreamReader streamReader = new StreamReader("map.txt");
            var (MapsizeX, MapsizeY) = streamReader.ReadLine().Split(",") switch
            {
                var a => (int.Parse(a[0]), int.Parse(a[1])),
            };
            Map = new Map(MapsizeX, MapsizeY);
            Map.PopulateMapFromStreamReader(streamReader, ThePlayer);
        }

        private (int X, int Y) WhereAmI()
        {
            Map.IndexOf(x => x is Player);
            return (-1, -1);
        }

        public void GameTick()
        {
            foreach (var item in Map)
            {
                item.Tick();
            }
        }

        public void Move(Directions direction)
        {
            (int iOriginal, int jOriginal) = WhereAmI();
            int i = iOriginal;
            int j = jOriginal;
            switch (direction)
            {
                case Directions.up:
                    if (i - 1 >= 0)
                    {
                        i--;
                    }
                    break;
                case Directions.down:
                    if (i + 1 < Map.GetLength(0))
                    {
                        i++;
                    }
                    break;
                default:
                    break;
            }
            if (Map[i, j] is Floor)
            {
                Map[i, j] = ThePlayer;
                Map[iOriginal, jOriginal] = new Floor(iOriginal, jOriginal);
            }
        }

    }
}
