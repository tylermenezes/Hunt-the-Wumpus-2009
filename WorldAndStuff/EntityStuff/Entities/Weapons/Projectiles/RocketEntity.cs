using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace HuntTheWumpus.WorldAndStuff.Weapons.Projectiles
{
    class RocketEntity : ProjectileEntity
    {

        private Vector2 _endLocation;
        public bool _exploded = false;

        public RocketEntity(string id)
            : base(EntityType.ROCKET,
            id + HuntTheWumpus.GameAndStuff.HTW.Instance.TotalElapsedMilliseconds, // create unique id
            new Vector2(30, 10), 9 / 16f)
        {
            _endLocation =
                new Vector2(
                    GraphicsAndStuff.Cursor.Instance.Location.X,
                    GraphicsAndStuff.Cursor.Instance.Location.Y);
            this.State = EntityState.MOVING;
        }

        public void endExplosion()
        {
            this.destroy();
        }
        public override void think(GameTime time)
        {
            if (!_exploded)
            {
                if (_fired) this.MoveForward(time.ElapsedGameTime.Milliseconds * Speed);
                CollisionDetection.testForCollisions(this);

                if (this.Location == _endLocation)
                    explode(Location);
                Console.WriteLine(Location);
                if (MathHelper.distanceFromPointToPoint(_startPosition, this.Location) > ActiveWeapon.AttackRadius)
                    explode(Location);
            }
        }
        private void explode(Vector2 location)
        {
            _exploded = true;
            this.State = EntityState.ATTACKING;
            var dynms = ParentWorld.getAllEnemyEntities();
            foreach (DynamicEntity ent in dynms)
            {
                if (ent.State != EntityState.DEAD &&
                    ent.Class != EntityClass.WEAPON &&
                    MathHelper.distanceFromPointToPoint(
                    this.Location, ent.Location) < 50)
                ent.recieveAttack(ActiveWeapon.Host, ActiveWeapon);
            }
        }
        public override void obstacleCollide(Entity[] entities)
        {
            Vector2 location = new Vector2();
            bool hit = false;
            foreach (Entity e in entities)
            {
                if (e != ActiveWeapon.Host && e.Class != EntityClass.WEAPON)
                {
                    location = e.Location;
                    hit = true;
                    break;
                }
            }
            if (hit)
                explode(location);          

        }
    }
}
