using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Player : DynamicObject
    {
        public Player(int xPosition, int yPosition, int life = 1, int ammo = 0) : base(xPosition, yPosition, "P", life, true, ammo)
        {
            IsSolid = true;
        }
        public override void Tick()
        {
            
        }
    }
}
