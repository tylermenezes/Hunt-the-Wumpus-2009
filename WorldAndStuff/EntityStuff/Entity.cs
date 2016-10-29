using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

public enum Directions {
	UP,
	DOWN,
	LEFT,
	RIGHT
}
namespace HuntTheWumpus.WorldAndStuff
{
	/// <summary>
	/// Represents an entity in the World.
	/// </summary>
    public abstract class Entity : IWorldObject
    {
        public string ID { get; protected set; }
        public EntityType Type { get; private set; }
        public EntityClass Class { get; private set; }
        public World ParentWorld { get; private set;}
        
        private EntityState _state;
        public EntityState State
        {
            get
            {
                return _state;
            }
            protected set
            {
                if (_state != value)
                {
                    if (ChangeStateEventHandler != null)
                        ChangeStateEventHandler(value);
                    _state = value;
                }
            }
        }
        private Vector3 _position;
        public Vector3 Position { get { return _position; } }
        public float X
        {
            get { return _position.X; }
            protected set { _position.X = value; }
        }
        public float Y
        {
            get { return _position.Y; }
            protected set { _position.Y = value; }
        }
		public float Top {
			get {
				return _position.Y;
			}
		}
		public float Bottom {
			get {
				return _position.Y - Size.Y;
			}
		}
		public float Left {
			get {
				return _position.X;
			}
		}
		public float Right {
			get {
				return _position.X + Size.X;
			}
		}
        public Vector2 Location
        {
            get
            {
                return new Vector2(X, Y);
            }
        }
		public Directions Facing {
			get {
				if (Rotation >= Math.PI / 4 && Rotation <= 3 * Math.PI / 4)
					return Directions.UP;
				else if (Rotation <= -Math.PI / 4 && Rotation >= -3 * Math.PI / 4)
					return Directions.DOWN;
				else if (Rotation >= -3 * Math.PI / 4 && Rotation <= 3 * Math.PI / 4)
					return Directions.RIGHT;
				else
					return Directions.LEFT;
			}
		}
        public float Rotation
        {
            get { return _position.Z; }
            protected set 
            {
                _position.Z = MathHelper.normalizeAngle(value);
            }
        }

        public Vector2 Size { get; private set; }

        public Entity( EntityType type, string id, Vector2 size)
        {
            Type = type;
            Class = getClassofEntityType(type);
            ID = id;
            Size = size;
        }

        private EntityClass getClassofEntityType(EntityType type)
        {
            switch (type)
            {
                case EntityType.BLOCK:
                    return EntityClass.TERRAIN; break;

                case EntityType.PISTOLBULLET:
                case EntityType.SHOTGUNBULLET:
                case EntityType.ROCKET:
                    return EntityClass.WEAPON;

                case EntityType.WAMPUS1:
                case EntityType.WEMPUS2:
                case EntityType.WIMPUS3:
                case EntityType.WOMPUS4:
                case EntityType.WUMPUS5:
                case EntityType.WYMPUS6:
                    return EntityClass.WUMPUS;

                case EntityType.PLAYER:
                    return EntityClass.PLAYER;
            }
            throw new Exception("Class of EntityType Unknown");
        }

        public abstract void recieveInteraction(PlayerEntity sender);
        public abstract void recieveAttack(DynamicEntity sender, DynamicEntity.Weapon weapon);

        public virtual void initialize(World world, Vector3 position)
        {
            ParentWorld = world;
            _position = position;
            world.addEntity(this);
        }
        public virtual void destroy()
        {
            ParentWorld.removeEntity(this);
        }

        public delegate void ChangeStateEvent(EntityState newState);
        public event ChangeStateEvent ChangeStateEventHandler;


        public override string ToString()
        {
            return "" + this.Type + ": " + _position.ToString(); 
        }
    }
}
