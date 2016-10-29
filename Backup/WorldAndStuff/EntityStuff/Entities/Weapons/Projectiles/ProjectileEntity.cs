using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using HuntTheWumpus.GameAndStuff;
namespace HuntTheWumpus.WorldAndStuff.Weapons.Projectiles
{
    public class ProjectileEntity : DynamicEntity
    {
        protected bool _fired { get; private set; }
        private float _startTime;
        protected Vector2 _startPosition { get; private set; }
        public ProjectileEntity(EntityType type, string id, Vector2 size, float speed) 
            : base(type, id, size, 1, speed)
        {
            ActiveWeapon = null;
            _fired = false;
        }

        public void fire(World world, Vector3 startingPosition, Weapon weapon, GameTime startTime)
        {
            State = EntityState.MOVING;
            ActiveWeapon = weapon;
            _startPosition = new Vector2(startingPosition.X, startingPosition.Y);
            initialize(world, startingPosition);
            _fired = true;
            _startTime = HTW.Instance.TotalElapsedMilliseconds;
            Console.WriteLine(_startTime);
        }

        public override void think(GameTime time)
        {
            if (_fired) this.MoveForward(time.ElapsedGameTime.Milliseconds * Speed);
            CollisionDetection.testForCollisions(this);
            if (MathHelper.distanceFromPointToPoint(_startPosition, this.Location) > ActiveWeapon.AttackRadius)
                this.projectileDestroyed();
        }

        public override void obstacleCollide(Entity[] entities)
        { 
            foreach (Entity e in entities)
            {
                if (e != ActiveWeapon.Host && e.Class != EntityClass.WEAPON)
                {
                    e.recieveAttack(ActiveWeapon.Host, ActiveWeapon);
                    projectileDestroyed();
                }
            }          
        }

        public override void recieveInteraction(PlayerEntity sender)
        {
            // Do Nothing
        }

        public virtual void projectileDestroyed()
        {
            destroy();
        }
    }
}
