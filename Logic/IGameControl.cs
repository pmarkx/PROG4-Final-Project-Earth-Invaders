using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logic.GameLogic;

namespace Logic
{
    public interface IGameControl
    {
        void Move(Constants.Directions direction);
        void Shoot();
        void Save();
        void Load();

        TimeSpan GameTickInterval { get; set; }
        TimeSpan EnemyMovementInterval { get; set; }
        TimeSpan EnemySpawnInterval { get; set; }
        TimeSpan BulletMoveInterval { get; set; }
        TimeSpan ShootingBetweenInterval { get; set; }
        TimeSpan LifeSpawnInterval { get; set; }
        TimeSpan AmmoSpawnInterval { get; set; }
        public bool GameOver { get; }
        public long Score { get; }
        public int Life { get; }
        public int Ammo { get; }

        public event TickHappened GameTickHappened;
        public void RefreshTimers();
        public void StartGame();
    }
}