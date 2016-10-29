using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class Wempus2Entity : AI.EnemyEntity
    {
        public Wempus2Entity(string id, Vector3 position, World world)
            : base(EntityType.WEMPUS2, id, new Vector2(30, 30),
            50, // set health here
            3 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            1, // set level here (1-5) one for each wumpus
            400) // set sight radius here (100-1000)
        {
            new Weapons.Wempus2Weapon(this); // set Weapon Here
        }
    }
}
