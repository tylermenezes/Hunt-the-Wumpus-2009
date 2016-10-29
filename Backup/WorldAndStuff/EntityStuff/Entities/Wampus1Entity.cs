using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class Wampus1Entity : AI.EnemyEntity
    {
        public Wampus1Entity(string id)
            : base(EntityType.WAMPUS1, id, new Vector2(30, 30),
            150, // set health here
            4 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            3, // set level here (1-5) one for each wumpus
            300) // set sight radius here (100-1000)
        {
            ActiveWeapon = new Weapons.Wampus1Weapon(this); // set Weapon Here
        }
    }
}
