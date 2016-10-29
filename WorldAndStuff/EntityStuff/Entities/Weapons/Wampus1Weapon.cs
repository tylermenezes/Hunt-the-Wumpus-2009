using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    class Wampus1Weapon : WeaponTypes.ShortRangeWeapon
    {
        public Wampus1Weapon (DynamicEntity host)
            : base(host,
            25, // set damage here (10-75)
            0,  // set value here
            100, // set attack radius here 
            1000)  // set recharge time here ( milliseconds should be > 50)
        {
            // That's all, do nothing here
        }
    }
}
