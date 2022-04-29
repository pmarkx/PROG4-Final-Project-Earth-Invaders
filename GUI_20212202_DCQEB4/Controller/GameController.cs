using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Logic.GameLogic;

namespace UI.Controller
{
    internal class GameController
    {
        IGameControl control;

        public GameController(IGameControl control)
        {
            this.control = control;
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
            }
        }
    }
}
