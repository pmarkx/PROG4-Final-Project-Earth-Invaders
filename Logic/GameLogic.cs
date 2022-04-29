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

        public static Player ThePlayer => new Player(0, 0, 3, 0);
        public Map Map { get; set; }
        private Directions lastMove = Directions.nowhere;

        public enum Directions
        {
            nowhere, up, down
        }

        //private void LoadNext(string path)
        //{
        //    string[] lines = File.ReadAllLines(path);
        //    Map = new Map[int.Parse(lines[0]), int.Parse(lines[1])];
        //    for (int i = 0; i < GameMatrix.GetLength(0); i++)
        //    {

        //        for (int j = 0; j < GameMatrix.GetLength(1); j++)
        //        {
        //            GameMatrix[i, j] = ConvertToEnum(lines[i + 2][j], i + 2, j);
        //        }
        //    }
        //}

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

        //public GameLogic()
        //{
        //    levels = new Queue<string>();
        //    var lvls = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "maps"), "*.txt");
        //    foreach (var item in lvls)
        //    {
        //        levels.Enqueue(item);
        //    }
        //    LoadNext(levels.Dequeue());
        //}
        
        //private int[] WhereAmI()
        //{
        //    for (int i = 0; i < Map.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < Map.GetLength(1); j++)
        //        {
        //            if (Map[i, j] is Player)
        //            {
        //                return new int[] { i, j };
        //            }
        //        }
        //    }
        //    return new int[] { -1, -1 };
        //}
        //public void Move(Directions direction)
        //{
        //    switch(direction)
        //    {
        //        case Directions.up:
        //            lastMove = Directions.up;
        //            break;
        //        case Directions.down:
        //            lastMove = Directions.down;
        //            break;
        //    }
        //}
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
        private (int X, int Y) WhereAmI()
        {
            Map.IndexOf(x => x is Player);
            return (-1, -1);
        }

    }
}
