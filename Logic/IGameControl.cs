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
        void Move(Directions direction);

        TimeSpan GameTickInterval { get; set; }
        TimeSpan EnemyMovementInterval { get; set; }
        public event TickHappened GameTickHappened;
        public void StartGame();
    }
}