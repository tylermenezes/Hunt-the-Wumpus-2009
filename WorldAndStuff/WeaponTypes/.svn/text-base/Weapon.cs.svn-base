using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.WorldAndStuff
{
    public abstract partial class DynamicEntity : Entity, IDynamicObject
    {
        public abstract class Weapon
        {
            public Weapon(DynamicEntity host,
                int damage,
                int value,
                float attackRadius,
                float rechargeTime)
            {
                AttackRadius = attackRadius;
                Host = host;
                Damage = damage;
                Value = value;
                RechargeTime = rechargeTime;
            }

            public float RechargeTime { get; private set; }
            public float AttackRadius { get; private set; } // Tells how far it can reach
            public int Value { get; private set; }
            public DynamicEntity Host
            {
                get;
                private set;
            }
            public int Damage
            {
                get;
                protected set;
            }

            // locate affected entities and call their recieveAttack(Host, this) method
            // Algorythim differs for different weapons ex. gun shoots, knife slashes
            public abstract void Fire(float direction, Microsoft.Xna.Framework.GameTime time);
        }
    }
}
