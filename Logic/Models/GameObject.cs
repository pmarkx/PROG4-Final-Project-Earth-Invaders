using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public abstract class GameObject
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public string Name { get; }
        public int Life { get; set; }
        public bool IsLive { get; protected set; }
        public bool WeaponOn { get; }
        public int Ammo { get; set; }
        public bool IsSolid { get; protected set; }

        protected GameObject(int xPosition, int yPosition, string name, int life, bool weaponOn, int ammo)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Name = name;
            Life = life;
            IsLive = true;
            WeaponOn = weaponOn;
            Ammo = ammo;
        }

        public virtual void Tick()
        {
            if (Life>=0)
            {
                IsLive = false;
            }
        }
    }
}
