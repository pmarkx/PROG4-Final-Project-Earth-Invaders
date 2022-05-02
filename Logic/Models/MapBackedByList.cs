using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class MapBackedByList : IMap
    {
        private List<GameObject> MapList;
        private int maxX;
        private int maxY;
        private static Random Rand = new Random();

        public MapBackedByList(int indexX, int indexY)
        {
            maxX = indexX;
            maxY = indexY;
            MapList = new List<GameObject>();
        }

        public MapBackedByList(StreamReader streamReader, Player player)
        {
            var (MapsizeX, MapsizeY) = streamReader.ReadLine().Split(",") switch
            {
                var a => (int.Parse(a[0]), int.Parse(a[1])),
            };
            maxX = MapsizeX;
            maxY = MapsizeY;
            MapList = new List<GameObject>();

            PopulateMapFromStreamReader(streamReader, player);
        }
        public GameObject this[int index1, int index2]
        {
            get
            {
                lock (this)
                {
                    var moreThanOneQuery = MapList.Where(x => x.XPosition == index1 && x.YPosition == index2);
                    if (moreThanOneQuery.Count() > 1)
                    {
                        return moreThanOneQuery.OrderByDescending(x => x.Priority).First();
                    }
                    else
                        return MapList.FirstOrDefault(x => x.XPosition == index1 && x.YPosition == index2) ?? new Floor(index1, index2);
                }
            }
            set
            {
                value.XPosition = index1;
                value.YPosition = index2;
                MapList.Add(value);
            }
        }

        public void CheckDie()
        {
            lock (this)
                for (int i = 0; i < MapList.Count; i++)
                {
                    if (!MapList[i].IsLive)
                        MapList.Remove(MapList[i]);
                }
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            return MapList.GetEnumerator();
        }

        public int GetLength(int dimension)
        {
            return dimension switch
            {
                0 => maxX,
                1 => maxY,
                _ => 0
            };
        }

        public (int X, int Y) IndexOf(Func<GameObject, bool> condition)
        {
            foreach (var item in MapList)
            {
                if (condition(item))
                    return (item.XPosition, item.YPosition);
            }
            return (-1, -1);
        }

        public void PopulateMapFromStreamReader(StreamReader streamReader, Player thePlayer)
        {

            for (int i = 0; i < GetLength(0); i++)
            {
                string line = streamReader.ReadLine();
                for (int j = 0; j < GetLength(1); j++)
                {
                    switch (line[j])
                    {
                        case 'm':
                            this[i, j] = new Mine(i, j);
                            break;
                        case 'w':
                            this[i, j] = new Wall(i, j);
                            break;
                        case 'e':
                            this[i, j] = new Enemy(i, j);
                            break;
                        case 'l':
                            this[i, j] = new LifeReward(i, j);
                            break;
                        case 'p':
                            this[i, j] = thePlayer;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void CollisionDetect()
        {
            foreach (var item in MapList)
            {
                if (MapList.Count(x => x.XPosition == item.XPosition && x.YPosition == item.YPosition) > 1)
                {
                    item.Collided(MapList.Where(x => x.XPosition == item.XPosition && x.YPosition == item.YPosition));
                }
            }
        }

        public void EnemyRushing()
        {
            lock (this)
            {
                Enemy enemy = new Enemy(Rand.Next(1, maxX), maxY);
                MapList.Add(enemy);
            }
        }

        public void LifeRewardRushing()
        {
            lock (this)
            {
                LifeReward lifeReward = new(Rand.Next(1, maxX), maxY);
                MapList.Add(lifeReward);
            }
        }
        public void AmmoRewardRushing()
        {
            lock (this)
            {
                LifeReward lifeReward = new(Rand.Next(1, maxX), maxY);
                MapList.Add(lifeReward);
            }
        }
        public void SpawnSomething(GameObject gameObject)
        {
            lock (this)
                MapList.Add(gameObject);
        }

        public void SaveState(StreamWriter streamWriter)
        {
            streamWriter.WriteLine($"{GetLength(0)},{GetLength(1)}");
            for (int i = 0; i < GetLength(0); i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j = 0; j < GetLength(1); j++)
                {
                    switch (this[i, j])
                    {
                        case Enemy:
                            line.Append('e');
                            break;
                        case Wall:
                            line.Append('w');
                            break;
                        case Player:
                            line.Append('p');
                            break;
                        case Mine:
                            line.Append('m');
                            break;
                        case LifeReward:
                            line.Append('l');
                            break;
                        default:
                        case Floor:
                            line.Append('f');
                            break;
                    }
                }
                streamWriter.WriteLine(line.ToString());
            }
        }
        public void SaveState(StreamWriter streamWriter, long Score, int Lifes)
        {
            this.SaveState(streamWriter);
            streamWriter.WriteLine(Score);
            streamWriter.WriteLine(Lifes);
        }


    }
}
