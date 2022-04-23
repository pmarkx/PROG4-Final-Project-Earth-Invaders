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
        static public Player ThePlayer = new Player(0, 0, 3, 0);
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
        GameObject[,] Map { get; set; }
        public GameLogic()
        {
            StreamReader streamReader = new StreamReader("map.txt");
            string[] Mapsize = streamReader.ReadLine().Split(",");
            Map=new GameObject[int.Parse(Mapsize[0]),int.Parse(Mapsize[1])];

            if (!streamReader.EndOfStream)
            {
                for (int i = 0; i < Map.GetLength(0); i++)
                {
                    string help=streamReader.ReadLine();
                    for (int j = 0; j < Map.GetLength(1); j++)
                    {
                        switch (help[j])
                        {
                            case 'F':
                                Map[i, j] = new Floor(i, j);
                                break;
                            case 'M':
                                Map[i, j] = new Mine(i, j);
                                break;
                            case 'W':
                                Map[i, j] = new Wall(i, j);
                                break;
                            case 'E':
                                Map[i, j] = new Enemy(i, j);
                                break;
                            case 'P':
                                Map[i, j] = ThePlayer;
                                break;

                        }
                    }
                }
            }
            streamReader.Close();
        }
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
                Map[old_i, old_j] = new Floor(old_i,old_j);
            }
        }

    }
}
