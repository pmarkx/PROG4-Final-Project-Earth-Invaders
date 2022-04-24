using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Enemy : DynamicObject
    {
        public Enemy(int xPosition, int yPosition) : base(xPosition, yPosition, "E", 3, false, 0)
        {
            IsSolid=true;
        }
        public override void Tick()
        {
            XPosition--;
            if (Life<=0)
            {
                IsLive = false;
            }
        }
    }
}
