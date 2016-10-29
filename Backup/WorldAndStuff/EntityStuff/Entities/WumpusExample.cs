using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    class WumpusExample : AI.EnemyEntity
    {
        public WumpusExample(string id)
            : base(EntityType.WUMPUS5, id, new Vector2(30, 30),
            10, // set health here
            3 / 16f, // set speed here (pixels/milisecond around 2/16-5/16 is good)
            1, // set level here (1-5) one for each wumpus
            200) // set sight radius here (100-1000)

        {
            new Weapons.Wumpus5Weapon(this); // set Weapon Here
        }
    }
}
