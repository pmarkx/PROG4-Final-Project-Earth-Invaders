using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class Constants
    {
        /*
         *  new TimeSpan(0,0,0,0,100);
            control.EnemyMovementInterval = new TimeSpan(0, 0, 0, 0, 300);
            control.EnemySpawnInterval = new TimeSpan(0, 0, 0, 0,400);
            control.BulletMoveInterval = new TimeSpan(0, 0, 0, 0, 200);
            control.ShootingBetweenInterval = new TimeSpan(0, 0, 0, 2);*/

        public static TimeSpan GameTickInterval = new TimeSpan(0, 0, 0, 0, 99);
        public static TimeSpan EnemyMovementInterval = new TimeSpan(0, 0, 0, 0, 301);
        public static TimeSpan EnemySpawnInterval = new TimeSpan(0, 0, 0, 0,399);
        public static TimeSpan BulletMoveInterval = new TimeSpan(0, 0, 0, 0, 199);
        public static TimeSpan ShootingBetweenInterval = new TimeSpan(0, 0, 0, 2);
        public static TimeSpan UIRefreshInterval = new TimeSpan(3);
        public static long DefaultScore = 0;
        public static int DefaultLifes = 3;
        public static int DefaultAmmo = 3;

        public enum Directions
        {
            nowhere, up, down, left, right
        }
    }
}
