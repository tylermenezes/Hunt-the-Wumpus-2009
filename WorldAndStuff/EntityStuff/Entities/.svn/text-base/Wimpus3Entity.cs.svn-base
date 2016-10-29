using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class Wimpus3Entity : AI.EnemyEntity
    {
        public Wimpus3Entity(string id)
            : base(EntityType.WIMPUS3, id, new Vector2(15, 25),
            70, // set health here
            5 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            1, // set level here (1-5) one for each wumpus
            700) // set sight radius here (100-1000)
        {
            ActiveWeapon = new Weapons.Wimpus3Weapon(this); // set Weapon Here
        }
    }
}
