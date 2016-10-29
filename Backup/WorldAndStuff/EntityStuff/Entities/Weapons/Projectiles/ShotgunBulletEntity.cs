using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace HuntTheWumpus.WorldAndStuff.Weapons.Projectiles
{
    class ShotgunBulletEntity : ProjectileEntity
    {
        public ShotgunBulletEntity(string id)
            : base(EntityType.SHOTGUNBULLET,
            id + HuntTheWumpus.GameAndStuff.HTW.Instance.TotalElapsedMilliseconds + Random.fish.Next(100000), // create unique id
            new Vector2(30, 10), 9 / 16f)
        {
            //
        }
    }
}
