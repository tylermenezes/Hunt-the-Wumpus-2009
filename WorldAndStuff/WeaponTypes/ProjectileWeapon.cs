using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using HuntTheWumpus.GameAndStuff;
namespace HuntTheWumpus.WorldAndStuff.WeaponTypes
{
    abstract class ProjectileWeapon : DynamicEntity.Weapon
    {
        protected float _prevFire;
        public Weapons.Projectiles.ProjectileEntity Projectile { get; protected set; }
        public ProjectileWeapon(DynamicEntity host, 
            int damage, 
            int value, 
            float attackRadius, 
            float rechargeTime)
            : base(host, damage, value, attackRadius, rechargeTime)
        {
            Projectile = null;
            _prevFire = 0;
        }

        public override void Fire(float direction, Microsoft.Xna.Framework.GameTime time)
        {
            
            if (_prevFire == 0) _prevFire =  HTW.Instance.TotalElapsedMilliseconds - RechargeTime;
            if (HTW.Instance.TotalElapsedMilliseconds - _prevFire >= RechargeTime)
            {
                _prevFire = HTW.Instance.TotalElapsedMilliseconds;
                loadProjectile();
                Projectile.fire(Host.ParentWorld,
                    Host.Position,
                    this,
                    time);
            }
        }

        protected abstract void loadProjectile();
    }
}
