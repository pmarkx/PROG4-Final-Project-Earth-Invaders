using System.Collections.Generic;

namespace Logic.Models
{
    public abstract class GameObject
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public string Name { get; }
        public int Life { get; set; }
        public bool IsLive { get; set; }
        public bool WeaponOn { get; }
        public int Ammo { get; set; }
        public bool IsSolid { get; protected set; }

        /// <summary>
        /// The  Bigger The better.
        /// </summary>
        public abstract int Priority { get; }

        protected GameObject(int xPosition, int yPosition, string name, int life, bool weaponOn, int ammo, bool isSolid = true)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Name = name;
            Life = life;
            IsLive = true;
            WeaponOn = weaponOn;
            Ammo = ammo;
            IsSolid = isSolid;
        }

        public virtual void Tick()
        {
            if (Life <= 0)
            {
                IsLive = false;
            }
        }
        public virtual void Move(Constants.Directions direction)
        {
            switch (direction)
            {
                case Constants.Directions.up:
                    XPosition--;
                    break;
                case Constants.Directions.down:
                    XPosition++;
                    break;
                case Constants.Directions.left:
                    YPosition--;
                    break;
                case Constants.Directions.right:
                    YPosition++;
                    break;
                case Constants.Directions.nowhere:
                default:
                    break;
            }
        }
        public virtual void Collided(IEnumerable<GameObject> collidedWith)
        {

        }
    }
}
