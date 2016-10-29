using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class Wompus4Entity : AI.EnemyEntity
    {
        public Wompus4Entity(string id, Vector3 position, World world)
            : base(EntityType.WOMPUS4, id, new Vector2(30, 30),
            100, // set health here
            2 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            1, // set level here (1-5) one for each wumpus
            400) // set sight radius here (100-1000)
        {
            new Weapons.Wompus4Weapon(this); // set Weapon Here
        }
    }
}
