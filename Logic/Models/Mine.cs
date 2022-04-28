using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Mine : GameObject
    {
        public Mine(int xPosition, int yPosition) : base(xPosition, yPosition, "M", 1, false, 0)
        {
            IsSolid = true;
        }

    }
}
