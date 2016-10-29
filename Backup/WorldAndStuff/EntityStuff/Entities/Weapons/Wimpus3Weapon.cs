using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    class Wimpus3Weapon : WeaponTypes.ShortRangeWeapon
    {
        public Wimpus3Weapon (DynamicEntity host)
            : base(host,
            15, // set damage here (10-75)
            0, // set value here
            30, // set attack radius here 
            500)  // set recharge time here ( milliseconds should be > 50)
        {
            // That's all, do nothing here
        }
    }
}
