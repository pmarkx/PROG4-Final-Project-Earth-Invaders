using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Logic.Models
{
    public class Map : IMap
    {
        public Map(int indexX, int indexY)
        {
            MapArray = new GameObject[indexX, indexY];
        }

        private GameObject[,] MapArray { get; set; }

        public IEnumerator<GameObject> GetEnumerator()
        {
            for (int i = 0; i < MapArray.GetLength(0); i++)
            {
                for (int j = 0; j < MapArray.GetLength(1); j++)
                {
                    yield return MapArray[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public GameObject this[int index1, int index2]
        {
            get
            {
                return MapArray[index1, index2];
            }
            set
            {
                MapArray[index1, index2] = value;
            }
        }

        public int GetLength(int dimension)
        {
            return MapArray.GetLength(dimension);
        }

        public (int X, int Y) IndexOf(Func<GameObject, bool> condition)
        {
            for (int i = 0; i < GetLength(0); i++)
            {
                for (int j = 0; j < GetLength(1); j++)
                {
                    if (condition(this[i, j]))
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        public void PopulateMapFromStreamReader(StreamReader streamReader, Player thePlayer)
        {
            if (!streamReader.EndOfStream)
            {
                for (int i = 0; i < GetLength(0); i++)
                {
                    string line = streamReader.ReadLine();
                    for (int j = 0; j < GetLength(1); j++)
                    {
                        switch (line[j])
                        {
                            case 'f':
                                this[i, j] = new Floor(i, j);
                                break;
                            case 'm':
                                //this[i, j] = new Mine(i, j);
                                break;
                            case 'w':
                                this[i, j] = new Wall(i, j);
                                break;
                            case 'e':
                                this[i, j] = new Enemy(i, j);
                                break;
                            case 'p':
                                this[i, j] = thePlayer;
                                break;
                        }
                    }
                }
            }
        }

        public void CheckDie()
        {
            for (int i = 0; i < GetLength(0); i++)
            {
                for (int j = 0; j < GetLength(1); j++)
                {
                    if (this[i, j].IsLive==false)
                    {
                        this[i, j] = new Floor(i, j);
                    }
                }
            }
        }

        public void CollisionDetect()
        {
            throw new NotImplementedException();
        }

        public void EnemyRushing()
        {
            throw new NotImplementedException();
        }
    }
}
