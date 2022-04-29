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
        public enum Directions
        {
            up, down
        }

        public GameObject[,] GameMatrix { get; set; }


        private void LoadNext(string path)
        {
            string[] lines = File.ReadAllLines(path);
            GameMatrix = new GameObject[int.Parse(lines[0]), int.Parse(lines[1])];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(lines[i + 2][j], i+2, j);
                }
            }
        }

        private GameObject ConvertToEnum(char v, int x, int y)
        {
            switch (v)
            {
                case 'f': return new Floor(x,y);
                case 'm': return new Mine(x,y);
                case 'w': return new Wall(x,y);
                case 'e': return new Enemy(x,y);
                case 'p': return ThePlayer;
                default: throw new Exception("unknown character!");
            }
        }

        static public Player ThePlayer = new Player(0, 0, 3, 0);

        GameObject[,] Map { get; set; }

        private Queue<string> levels;

        public GameLogic()
        {
            levels = new Queue<string>();
            var lvls = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "maps"), "*.txt");
            foreach (var item in lvls)
            {
                levels.Enqueue(item);
            }
            LoadNext(levels.Dequeue());
        }
        //public GameLogic()
        //{
        //    StreamReader streamReader = new StreamReader("map.txt");
        //    string[] Mapsize = streamReader.ReadLine().Split(",");
        //    Map = new GameObject[int.Parse(Mapsize[0]), int.Parse(Mapsize[1])];

        //    if (!streamReader.EndOfStream)
        //    {
        //        for (int i = 0; i < Map.GetLength(0); i++)
        //        {
        //            string help = streamReader.ReadLine();
        //            for (int j = 0; j < Map.GetLength(1); j++)
        //            {
        //                switch (help[j])
        //                {
        //                    case ' ':
        //                        Map[i, j] = new Floor(i, j);
        //                        break;
        //                    case 'M':
        //                        Map[i, j] = new Mine(i, j);
        //                        break;
        //                    case 'W':
        //                        Map[i, j] = new Wall(i, j);
        //                        break;
        //                    case 'E':
        //                        Map[i, j] = new Enemy(i, j);
        //                        break;
        //                    case 'P':
        //                        Map[i, j] = ThePlayer;
        //                        break;

        //                }
        //            }
        //        }
        //    }
        //    streamReader.Close();
        //}
        private int[] WhereAmI()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] is Player)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }
        public void Move(Directions direction)
        {
            foreach (var item in Map)
            {
                item.Tick();
            }
            var coords = WhereAmI();
            int i = coords[0];
            int j = coords[1];
            int old_i = i;
            int old_j = j;
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
                Map[old_i, old_j] = new Floor(old_i, old_j);
            }
        }
    }
}
