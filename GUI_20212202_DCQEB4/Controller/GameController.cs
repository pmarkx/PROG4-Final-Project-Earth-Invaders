using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using UI.Renderer;
using static Logic.GameLogic;

namespace UI.Controller
{
    internal class GameController
    {
        IGameControl control;
        public event TickHappened GameTickHappened;
        public DispatcherTimer UITimer;
        public GameController(IGameControl control)
        {
            this.control = control;
            control.GameTickInterval = new TimeSpan(0,0,0,0,101);
            control.EnemyMovementInterval = new TimeSpan(0, 0, 0, 0, 499);
            control.EnemySpawnInterval = new TimeSpan(0, 0, 0, 0,995);
            control.BulletMoveInterval = new TimeSpan(0, 0, 0, 0, 242);
            control.ShootingBetweenInterval = new TimeSpan(0, 0, 0, 1);
            control.GameTickHappened += Control_GameTickHappened;
            UITimer = new DispatcherTimer();
            UITimer.Interval=new TimeSpan(3);
            control.StartGame();
        }

        private void Control_GameTickHappened()
        {
            this.GameTickHappened?.Invoke();
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    control.Move(Directions.up);
                    break;
                case Key.Down:
                    control.Move(Directions.down);
                    break;
                case Key.Space:
                    control.Shoot();
                    break;
            }
        }
    }
}
