using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
    public delegate void EntityChangeHandler(Entity entity);
    
    // changes will be made after ai and physics stuff is in place
    public class World
    {
        private Dictionary<String, Entity> _entities; // should be augmented with physics engine
        private List<DynamicEntity> _dynamicEntities;

		public int LiveWumpii {
			get {
				int n = 0;
				foreach (DynamicEntity ent in getAllEnemyEntities()) {
					if (ent.Class == EntityClass.WUMPUS && ent.State != EntityState.DEAD)
						n++;
				}
				return n;
			}
		}

        public event EntityChangeHandler EntityAdded;
        public event EntityChangeHandler EntityRemoved;

        public void addEntity(Entity entity)
        {
            _entities.Add(entity.ID, entity);
            if (EntityAdded != null)
                EntityAdded(entity);
        } 
        public void removeEntity(Entity entity)
        {
            _entities.Remove(entity.ID);
            if(EntityRemoved != null)
                EntityRemoved(entity);
        }

        public void addEntity(Entity entity, bool fireEvent)
        {
            _entities.Add(entity.ID, entity);
            if (fireEvent) EntityAdded(entity);
        }
        public void removeEntity(Entity entity, bool fireEvent)
        {
            _entities.Remove(entity.ID);
            if (fireEvent) EntityRemoved(entity);
        }

        public void spawn(DynamicEntity entity)
        {
            _dynamicEntities.Add(entity);
        }
        public void kill(DynamicEntity entity)
        {
            _dynamicEntities.Remove(entity);
        }
        public Entity getEntity(string id)
        {
            return _entities[id];
        }
        public Dictionary<String, Entity> getAllEntities()
        {
            return _entities;
        }

        public List<DynamicEntity> getAllEnemyEntities()
        {
            return _dynamicEntities;
        }

        public int EntityCount() { return _entities.Count; }
        
        public void Update(GameTime time)
        {
            // Player stuff should go in Game class?
            for (int i = 0; i < _dynamicEntities.Count; i++)
            {
                var dyn = _dynamicEntities[i];
                if (dyn.State != EntityState.DEAD)
                    dyn.think(time);

            }
          
            // Physics integration here
        }

        public Vector2 WorldSize { get; private set; }
        public World(Vector2 size)
        {
            _dynamicEntities = new List<DynamicEntity>();
            _entities = new Dictionary<string, Entity>(); // replace with physics engine initialization
            WorldSize = size;
        }

        public KeyValuePair<Entity, float> findEntity(Vector2 location)
        {
            var entities = findEntities(location);
            if (entities.Count == 0) return new KeyValuePair<Entity,float>();
            else
            {
                Entity closest = null;
                foreach (Entity ent in entities.Keys)
                {
                    if (closest == null) closest = ent;
                    if (entities[ent] < entities[closest])
                        closest = ent;
                }
                return new KeyValuePair<Entity,float>(closest, entities[closest]);
            }
        }

        public Dictionary<Entity, float> findEntities(Vector2 location)
        {
            var entities = new Dictionary<Entity, float>();
            foreach (Entity entity in _entities.Values)
            {
                var xDistance = Math.Abs(location.X - entity.Location.X);
                var yDistance = Math.Abs(location.Y - entity.Location.Y);
                if (xDistance < entity.Size.X &&
                    yDistance < entity.Size.Y)
                    entities.Add(entity,
                        MathHelper.distanceFromPointToPoint(
                        location, entity.Location));
            }
            return entities;
        }
    }
}
