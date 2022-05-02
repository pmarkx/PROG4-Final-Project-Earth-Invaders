using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Wall : GameObject
    {
        public Wall(int xPosition, int yPosition) : base(xPosition, yPosition, "W", 99, false, 0)
        {
            IsSolid = true;
        }

        public override int Priority => 3;
    }
}
