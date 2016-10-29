using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class Wumpus5Entity : AI.EnemyEntity
    {
        public Wumpus5Entity(string id)
            : base(EntityType.WUMPUS5, id, new Vector2(50,78),
            250, // set health here
            3 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            5, // set level here (1-5) one for each wumpus
            300) // set sight radius here (100-1000)
        {
            ActiveWeapon = new Weapons.Wumpus5Weapon(this); // set Weapon Here
        }
    }
}
