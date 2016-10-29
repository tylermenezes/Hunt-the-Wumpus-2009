using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
namespace HuntTheWumpus.WorldAndStuff
{
    public delegate void EntityAction(EntityActionEventArgs e);
    public abstract partial class DynamicEntity : Entity, IDynamicObject
	{
        public event EntityAction EntityRecievedAttack;
        public event EntityAction EntityRecievedInteraction;


        public delegate void EntityDiedEventHandler(DynamicEntity player);
        public event EntityDiedEventHandler EntityDiedEvent;

        public Weapon ActiveWeapon { get; protected set; }
		public int Health {get; set; }
		public bool CollisionLeft {
			get {
				return CollisionDetection.isCollision(CollisionState.COLLISION_LEFT, this);
			}
		}
		public bool CollisionRight {
			get {
				return CollisionDetection.isCollision(CollisionState.COLLISION_RIGHT, this);
			}
		}
		public bool CollisionTop {
			get {
				return CollisionDetection.isCollision(CollisionState.COLLISION_TOP, this);
			}
		}
		public bool CollisionBottom {
			get {
				return CollisionDetection.isCollision(CollisionState.COLLISION_BOTTOM, this);
			}
		}

        /// <summary>
        /// Speed of motion is pixels per millisecond
        /// </summary>
        public float Speed { get; private set; }  

        // Initialization
        public DynamicEntity(
            EntityType type,
            string id,
            Vector2 size,
            int health,
            float speed)
            : base(type, id, size)
        {
            Health = health;
            Speed = speed;
            base.State = EntityState.RESTING;
        }

        public override void initialize(World world, Vector3 position)
        {
            base.initialize(world, position);
            world.spawn(this);
        }
#region AIStuff
        public abstract void think(GameTime time);
        public virtual void die()
        {
            this.State = EntityState.DEAD;
            if (EntityDiedEvent != null) EntityDiedEvent(this);
        }
		/// <summary>
		/// Called when an entity is hit.
		/// </summary>
		/// <param name="sender">Whosoever sent it</param>
		/// <param name="weapon">What the entity was hit by</param>
        public override void recieveAttack(DynamicEntity sender, DynamicEntity.Weapon weapon)
        {
            this.Health -= weapon.Damage;
            Console.WriteLine(this.Type + " Attacked: " + this.Health);
            if (this.Health <= 0)
            {
                die();
            }
        }

		/// <summary>
		/// Moves an entity forward.
		/// </summary>
		/// <param name="distance">How far, in some sort of unit.</param>
        protected virtual void MoveForward(float distance) // in meters
        {
            // hmm... seems it's not as hard as I first thought,
            // assuming this will work, of course
            base.X += distance * (float)Math.Cos((double)base.Rotation);
            base.Y -= distance * (float)Math.Sin((double)base.Rotation);
            base.State = EntityState.MOVING;
        }

		protected virtual void moveBackward(float distance){
			MoveForward(distance * -.75f);	
		}

		/// <summary>
		/// Moves an entity left.
		/// </summary>
		/// <param name="distance">How far, in some sort of unit.</param>
		protected virtual void strifeLeft( float distance ) // in meters
		{
			// STOP COMMENTING THIS.
			// It is really hard to play when you strife left whilst facing down using your crappy method.
			// (You go right!)
            if ((base.Rotation >= Math.PI / 4 && base.Rotation <= 3 * Math.PI / 4) || (base.Rotation <= -Math.PI / 4 && base.Rotation >= -3 * Math.PI / 4)) {
                base.X -= distance * .5f;
                base.Y += distance * 0f;
            }
            else if (base.Rotation >= -3 * Math.PI / 4 && base.Rotation <= 3 * Math.PI / 4) {
                base.X += distance * 0f;
                base.Y -= distance * .5f;
            }
            else {
                base.X -= distance * 0f;
                base.Y += distance * .5f;
            }

			base.State = EntityState.MOVING;
		}

		protected virtual void strifeRight( float distance ) {
			strifeLeft(-distance);
		}

        protected virtual void Attack(GameTime time)
        {
			Program.Client.send(ServerCommandTypes.FIRE, "1");
            if (this.ActiveWeapon == null)
                //throw new Exception("No Active Weapon");
                ;
            //GameAndStuff.HTW.log("No active weapon."); <-- Not working
            else
                this.ActiveWeapon.Fire(this.Rotation, time);
        }
        protected virtual void rest()
        {
            base.State = EntityState.RESTING;
        }

        public abstract void obstacleCollide(Entity[] entities);

        protected virtual void turnTo(float degrees)
        {
            base.Rotation = degrees;
        }

        public override void destroy()
        {
            ParentWorld.kill(this);
            base.destroy();
        }
#endregion
    }

    public class EntityActionEventArgs : EventArgs
    {
        public enum ActionType { ATTACK, INTERACT, KILL, REVIVE }

        public ActionType Action { get; private set; }

        public DynamicEntity Actor { get; private set; }
        public DynamicEntity Initiator { get; private set; }

        public EntityActionEventArgs(ActionType type, DynamicEntity actor, DynamicEntity initiator)
        {
            Action = type;
            Actor = actor;
            Initiator = initiator;
        }
    }
}
