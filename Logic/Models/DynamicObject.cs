﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public abstract class DynamicObject : GameObject
    {
        public DynamicObject(int xPosition, int yPosition, string name, int life = 1, bool weaponOn = false, int ammo = 0) : base(xPosition, yPosition, name, life, weaponOn, ammo)
        {
        }
        public override void Tick()
        {
            XPosition++;
            base.Tick();
        }
    }
}