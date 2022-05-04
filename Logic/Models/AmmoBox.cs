using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class AmmoBox : DynamicObject
    {
        public AmmoBox(int xPosition, int yPosition) : base(xPosition, yPosition, "A", 1, false, 0)
        {
        }

        public override int Priority => 2;

        public override void Tick()
        {
            YPosition--;
            base.Tick();
        }
    }
}
