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

        public GameController(IGameControl control)
        {
            this.control = control;
            DispatcherTimer gameTime = new DispatcherTimer();
            gameTime.Tick += GameTimer_Tick;
            gameTime.Interval = new TimeSpan(1); // 1 tick is 100ms
            gameTime.Start();        
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            control.GameTick();
            TickTick.Invoke();
        }

        public delegate void TickHappened();
        public event TickHappened TickTick; 

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
            }
        }
    }
}
