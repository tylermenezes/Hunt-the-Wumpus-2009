using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GraphicsAndStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HuntTheWumpus.GameAndStuff;

namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    class ShotgunWeapon : WeaponTypes.ProjectileWeapon, IPlayerWeapon
    {
        public ShotgunWeapon (DynamicEntity host)
            : base(host,
            10, // set damage here (10-75)
            40, // set value here
            500, // set attack radius here 
            500)  // set recharge time here ( milliseconds should be > 50)
        {
            // That's all, do nothing here

            WeaponWalkAnimation = new Animation(
                (Texture2D)CentralResourceRepository.Resources["hunterTEAM1shotgun"],
                4,
                .1);

            WeaponStandAnimation = new Animation(
                (Texture2D)CentralResourceRepository.Resources["hunterTEAM1shotgunREST"],
                1,
                .1);
        }

        protected override void loadProjectile()
        {
            Projectile = new Projectiles.ShotgunBulletEntity(Host.ID);
        }

		public override void Fire( float direction, Microsoft.Xna.Framework.GameTime time ) {

			if (_prevFire == 0)
				_prevFire = HTW.Instance.TotalElapsedMilliseconds - RechargeTime;
			if (HTW.Instance.TotalElapsedMilliseconds - _prevFire >= RechargeTime) {
				_prevFire = HTW.Instance.TotalElapsedMilliseconds;
				loadProjectile();
				Projectile.fire(Host.ParentWorld,
					Host.Position,
					this,
					time);
				loadProjectile();
				Projectile.fire(Host.ParentWorld,
					new Vector3(Host.Position.X, Host.Position.Y, Host.Position.Z + .10f),
					this,
					time);
				loadProjectile();
				Projectile.fire(Host.ParentWorld,
					new Vector3(Host.Position.X, Host.Position.Y, Host.Position.Z - .10f),
					this,
					time);
				loadProjectile();
				Projectile.fire(Host.ParentWorld,
					new Vector3(Host.Position.X, Host.Position.Y, Host.Position.Z + .05f),
					this,
					time);
				loadProjectile();
				Projectile.fire(Host.ParentWorld,
					new Vector3(Host.Position.X, Host.Position.Y, Host.Position.Z - .05f),
					this,
					time);
			}
		}

        public Animation WeaponWalkAnimation { get; private set; }
        public Animation WeaponStandAnimation { get; private set; }
    }
}
