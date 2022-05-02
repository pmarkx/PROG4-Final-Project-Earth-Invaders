using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class LifeReward : DynamicObject
    {
        public LifeReward(int xPosition, int yPosition) : base(xPosition, yPosition, "L", 1, false, 0)
        {
        }

        public override int Priority => 1;

        public override void Tick()
        {
            YPosition--;
            base.Tick();

        }
    }
}
