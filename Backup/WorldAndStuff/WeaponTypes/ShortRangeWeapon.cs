using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.WorldAndStuff.WeaponTypes
{
    class ShortRangeWeapon : DynamicEntity.Weapon
    {
        public ShortRangeWeapon(DynamicEntity host, int damage, int value, float attackRadius, float rechargeTime)
            : base(host, damage, value,  attackRadius, rechargeTime)
        {
            //
        }

        private float _lastTime = 0;
        public override void Fire(float direction, Microsoft.Xna.Framework.GameTime time)
        {
            float thisTime = HuntTheWumpus.GameAndStuff.HTW.Instance.TotalElapsedMilliseconds;
            if (_lastTime == 0) _lastTime = thisTime - RechargeTime;
            if (thisTime - _lastTime >= RechargeTime)
            {
                var dynms = Host.ParentWorld.getAllEnemyEntities();
                for (int i = 0; i < dynms.Count; i++)
                {
                    if (dynms[i] == Host) continue;
                    if (MathHelper.distanceFromPointToPoint(
                            Host.Location, dynms[i].Location) < AttackRadius &&
                        MathHelper.directionFromPointToPoint(
                            Host.Location, dynms[i].Location) == direction)
                        dynms[i].recieveAttack(Host, this);
                }
            }
        }
    }
}
