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
        }

        public override int Priority => 0;

        public override void Collided(IEnumerable<GameObject> collidedWith)
        {
            base.Collided(collidedWith);
            foreach (var item in collidedWith)
            {
                switch (item)
                {
                    case Enemy e:
                        this.Life = 0;
                        e.Life = 0;
                        GameLogic.EnemyDied++;
                        break;
                    case Wall w:
                        Life = 0;
                        break;
                    default:
                        break;
                };
            }
        }

        public override void Tick()
        {
            YPosition++;
            base.Tick();
        }
    }
}
