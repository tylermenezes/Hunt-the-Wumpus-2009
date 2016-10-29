using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using HuntTheWumpus.GameAndStuff;
namespace HuntTheWumpus.WorldAndStuff.AI
{
    public abstract partial class EnemyEntity : DynamicEntity
    {
        public int Level { get; private set; }          // Tell how much its worth
        public float SightRadius { get; private set; }  // Tells how far it can see (sense)

        public EnemyEntity(EntityType type,
            string id,
            Vector2 size,
            int health,
            float speed,
            int level,
            int sightRadius)
            : base(type, id, size, health, speed)
        {
            Level = level;
            SightRadius = sightRadius;
            State = EntityState.RESTING;
        }

        private float _pastTime;
        private bool _attacking = false;
        private bool _blocked = false;
        private bool _evadingObstacle = false;
        private float _evasionStartTime = 0;
		private int noAction = 0;

        public override void think(GameTime time)
        {
			noAction++;
			if (noAction > 700) {
				base.die();
				Console.WriteLine(base.Type.ToString() + " died of a broken heart");
			}
            if (_attacking == true)
            {
				noAction = 0;
                if (HTW.Instance.TotalElapsedMilliseconds - _pastTime > ActiveWeapon.RechargeTime)
                {
                    _attacking = false;
                    this.State = EntityState.RESTING;
                }
            }
            else
            {
                if (_evadingObstacle)
                {
                    if (HTW.Instance.TotalElapsedMilliseconds - _evasionStartTime > 500)
                        _evadingObstacle = false;
                }
                else
                    Compass.checkForCreatures(this, Compass.AlgorythmType.CIRCLE);
                switch (base.State)
                {
                    case EntityState.DEAD:
                        return;
                    case EntityState.RESTING:
                        break;
                    case EntityState.MOVING:
                        if (!_blocked)
                            MoveForward(Speed * time.ElapsedGameTime.Milliseconds);
                        if (this.CollisionTop)
                            _blocked = true;
                        else
                            _blocked = false;
                        CollisionDetection.testForCollisions(this);
                        break;
                    case EntityState.ATTACKING:
                        ActiveWeapon.Fire(this.Rotation, time);
                        _attacking = true;
                        _pastTime = HTW.Instance.TotalElapsedMilliseconds;
                        break;
                }
            }
        }

        // Called by collision engine
        private System.Random _rand = new System.Random();
        private int _evasionDirection = 1;
        public override void obstacleCollide(Entity[] entities)
        {
            if (_evadingObstacle == false)
            {
                _evadingObstacle = true;
                _evasionStartTime = HTW.Instance.TotalElapsedMilliseconds;
                _evasionDirection = _rand.Next(-1, 1);
            }
            this.moveBackward(Speed * 16);
            this.Rotation += _evasionDirection * (float)Math.PI / 20;
        }

        public override void recieveAttack(DynamicEntity sender, DynamicEntity.Weapon weapon)
        {
            if (this.Target != WumpusTarget.ENEMY)
            {
                if (sender.Type == EntityType.PLAYER)
                    this.playerEnter((PlayerEntity)sender,
                        MathHelper.distanceFromPointToPoint(
                            sender.Location,
                            this.Location));
                else
                    this.enemyEnter((EnemyEntity)sender,
                        MathHelper.distanceFromPointToPoint(
                            sender.Location,
                        this.Location));
                this.Target = WumpusTarget.WEAPON; 
            }
            base.recieveAttack(sender, weapon);
        }
        public override void recieveInteraction(PlayerEntity sender)
        {
            // This means the is dead and must be collected
            if (this.State == EntityState.DEAD)
            {
                sender.raisePoints(this.Level*5);
                this.destroy();
            }
        }
    }
}
