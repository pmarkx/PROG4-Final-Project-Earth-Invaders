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
        }

        public override void Tick()
        {
            base.Tick();
        }
        public override void Collided(IEnumerable<GameObject> collidedWith)
        {
            base.Collided(collidedWith);
            foreach (var item in collidedWith)
            {
                switch (item)
                {
                    case Enemy e:
                        Life--;
                        e.Life--;
                        break;
                    case Wall w:
                        Life = 0;
                        break;
                    default:
                        break;
                };
            }
        }
    }
}
