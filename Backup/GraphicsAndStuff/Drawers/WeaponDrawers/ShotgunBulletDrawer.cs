using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.WorldAndStuff.Weapons.Projectiles;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.GraphicsAndStuff.Drawers.WeaponDrawers
{
    class ShotgunBulletDrawer : EntityDrawer
    {
        public ShotgunBulletDrawer(ShotgunBulletEntity bullet)
            : base(bullet)
        {
            //
        }

        public override void SetupDrawer()
        {
            Animations.Add(HuntTheWumpus.WorldAndStuff.EntityState.MOVING,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["shotgunbullet"],
                    1,
                    .1));
        }
    }
}
