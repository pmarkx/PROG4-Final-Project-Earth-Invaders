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

        TimeSpan GameTickInterval { get; set; }
        TimeSpan EnemyMovementInterval { get; set; }
        TimeSpan EnemySpawnInterval { get; set; }
        TimeSpan BulletMoveInterval { get; set; }
        TimeSpan ShootingBetweenInterval { get; set; }
        public bool GameOver { get; }
        public long Score { get; }
        public long Life { get; }
        public long Ammo { get; }

        public event TickHappened GameTickHappened;
        public void RefreshTimers();
        public void StartGame();
    }
}