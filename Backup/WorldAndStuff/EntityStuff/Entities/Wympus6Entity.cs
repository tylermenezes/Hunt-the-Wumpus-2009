using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class Wympus6Entity : AI.EnemyEntity
    {
        public Wympus6Entity(string id, Vector3 position, World world)
            : base(EntityType.WAMPUS1, id, new Vector2(30, 30),
            75, // set health here
            4 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            1, // set level here (1-5) one for each wumpus
            400) // set sight radius here (100-1000)
        {
            new Weapons.Wympus6Weapon(this); // set Weapon Here
        }
    }
}
