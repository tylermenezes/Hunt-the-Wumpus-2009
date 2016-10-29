using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GraphicsAndStuff;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    class PistolWeapon : WeaponTypes.ProjectileWeapon, IPlayerWeapon
    {
        public PistolWeapon (DynamicEntity host)
            : base(host,
            15, // set damage here (10-75)
            0, // set value here
            500, // set attack radius here 
            50)  // set recharge time here ( milliseconds should be > 50)
        {
            // That's all, do nothing here

            WeaponWalkAnimation = new Animation(
                (Texture2D)CentralResourceRepository.Resources["hunterTEAM1pistol"],
                4,
                .1);

            WeaponStandAnimation = new Animation(
                (Texture2D)CentralResourceRepository.Resources["hunterTEAM1pistolREST"],
                1,
                .1);
        }

        protected override void loadProjectile()
        {
            Projectile = new Projectiles.PistolBulletEntity(Host.ID); // Add the projectile entity that will be shot
        }

        public Animation WeaponWalkAnimation { get; private set; }
        public Animation WeaponStandAnimation { get; private set; }
    }
}
