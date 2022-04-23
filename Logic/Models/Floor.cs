using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Floor : GameObject
    {
        public Floor(int xPosition, int yPosition) : base(xPosition, yPosition, "F", 99, false, 0)
        {
            IsSolid = false;
        }


    }
}
