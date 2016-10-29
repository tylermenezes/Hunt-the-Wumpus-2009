using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.WorldAndStuff.Weapons.Projectiles;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.GraphicsAndStuff.Drawers.WeaponDrawers
{
    class PistolBulletDrawer : EntityDrawer
    {
        public PistolBulletDrawer(PistolBulletEntity bullet)
            : base(bullet)
        {
            //
        }

        public override void SetupDrawer()
        {
            Animations.Add(HuntTheWumpus.WorldAndStuff.EntityState.MOVING,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["bullet"],
                    1,
                    .1));
        }
    }
}
