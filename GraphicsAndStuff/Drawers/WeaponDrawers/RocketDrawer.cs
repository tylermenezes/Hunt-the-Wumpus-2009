using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.WorldAndStuff.Weapons.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using HuntTheWumpus.WorldAndStuff;
namespace HuntTheWumpus.GraphicsAndStuff.Drawers.WeaponDrawers
{
    class RocketDrawer : EntityDrawer
    {
        public RocketDrawer(RocketEntity rocket)
            : base(rocket)
        {
            //
        }

        public override void SetupDrawer()
        {
            Animations.Add(HuntTheWumpus.WorldAndStuff.EntityState.MOVING,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["rocket"],
                    1,
                    .1));
            Animations.Add(HuntTheWumpus.WorldAndStuff.EntityState.ATTACKING,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["explosion"],
                    6,
                    .1,
                    false));

            Animations[EntityState.ATTACKING].AnimationStopped += new AnimationChangeEvent(RocketDrawer_AnimationStopped);
            scale = 2;
        }

        void RocketDrawer_AnimationStopped()
        {
            ((RocketEntity)Entity).endExplosion();
        }
    }
}
