using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    class Wumpus5Weapon : WeaponTypes.ShortRangeWeapon
    {
        public Wumpus5Weapon (DynamicEntity host)
            : base(host,
            100, // set damage here (10-75)
            0,  // set value here
            50, // set attack radius here 
            750)  // set recharge time here ( milliseconds should be > 50)
        {
            // That's all, do nothing here
        }
    }
}
