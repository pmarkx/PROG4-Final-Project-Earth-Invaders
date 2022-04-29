﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Logic.Models
{
    public interface IMap :IEnumerable<GameObject>
    {
        GameObject this[int index1, int index2] { get; set; }

        IEnumerator<GameObject> GetEnumerator();
        int GetLength(int dimension);
        (int X, int Y) IndexOf(Func<GameObject, bool> condition);
        void PopulateMapFromStreamReader(StreamReader streamReader, Player thePlayer);
        void CheckDie();
    }
}