using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GraphicsAndStuff;
using Microsoft.Xna.Framework.Graphics;
namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    class RocketWeapon : WeaponTypes.ProjectileWeapon, IPlayerWeapon
    {
        public RocketWeapon(DynamicEntity host)
            : base(host,
            150, // set damage here (10-75)
            10, // set value here
            1000, // set attack radius here 
            3000)  // set recharge time here ( milliseconds should be > 50)
        {
            // That's all, do nothing here

            WeaponWalkAnimation = new Animation(
                (Texture2D)CentralResourceRepository.Resources["hunterTEAM1rocket"],
                4,
                .1);

            WeaponStandAnimation = new Animation(
                (Texture2D)CentralResourceRepository.Resources["hunterTEAM1rocketREST"],
                1,
                .1);
        }

        protected override void loadProjectile()
        {
            Projectile = new Projectiles.RocketEntity(Host.ID);
        }

        public Animation WeaponWalkAnimation { get; private set; }
        public Animation WeaponStandAnimation { get; private set; }
    }
}
