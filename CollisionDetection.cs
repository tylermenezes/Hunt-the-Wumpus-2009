using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GameAndStuff;
using HuntTheWumpus.GraphicsAndStuff;
using HuntTheWumpus.WorldAndStuff;

enum CollisionState {
	COLLISION_LEFT = 1,
	COLLISION_RIGHT = 2,
	COLLISION_TOP = 4,
	COLLISION_BOTTOM = 8,
	NO_COLLISIONS = 0
}

namespace HuntTheWumpus {
	static class CollisionDetection {
		/// <summary>
		/// Gets a list of all direction in which an entity is colliding.
		/// </summary>
		/// <param name="a">The entity to test</param>
		/// <returns>An array of collisions or a one-dimensional array of NO_COLLISIONS.</returns>
		public static CollisionState[] testForCollisions( DynamicEntity a ) {
			var collisions = new Queue<CollisionState>();
            var objectsInCollision = new Queue<Entity>();
			try {
				foreach (Entity b in ((SinglePlayerPlayState)HTW.Instance.CurrentGameState).CurrentWorld.getAllEntities().Values) {
					if (a.Equals(b))
						continue; // Don't test against yourself.
					if (b.State == EntityState.DEAD)
						continue; // Don't test for corpses

					// First test for a bounding collision to save compute cycles.
					var collision = testForBoundCollisions(a, b);
					if (collision != CollisionState.NO_COLLISIONS) {
						objectsInCollision.Enqueue(b);
						collisions.Enqueue(collision);
					}
				}
				if (collisions.Count <= 0)
					return new CollisionState[] { CollisionState.NO_COLLISIONS };

				a.obstacleCollide(objectsInCollision.ToArray());
				return collisions.ToArray();
			} catch {
			}
			return new CollisionState[] { CollisionState.NO_COLLISIONS };
		}

		/// <summary>
		/// Tests if an Entity is in collision in a certain direction (useful for movement)
		/// </summary>
		/// <param name="test">Direction to test</param>
		/// <param name="toTest">Entity to test</param>
		/// <returns>True if it's in a collision, false otherwise.</returns>
		public static bool isCollision( CollisionState test, DynamicEntity toTest ) {
			foreach (CollisionState col in testForCollisions(toTest)) {
				if (col == test)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Tests for a collision between "a" and "b" in only their bounding boxes.
		/// </summary>
		/// <param name="a">An entity to test</param>
		/// <param name="b">An entity to test</param>
		/// <returns>CollisionState representing where a collision occured.</returns>
		private static CollisionState testForBoundCollisions( Entity a, Entity b) {
            // Didn't Take Rotation into Account
            //Queue<CollisionState> collisions = new Queue<CollisionState>();
            if (a.Right >= b.Right && a.Left <= b.Right &&
                ((a.Top > b.Bottom && a.Bottom <= b.Top) ||
                (a.Top <= b.Top && a.Bottom >= b.Bottom) ||
                (a.Top >= b.Bottom && a.Bottom <= b.Bottom)))
            {
                return computeEntityDirection(a, b); 
               
            }
            else if (a.Right >= b.Left && a.Left <= b.Left &&
                ((a.Top >= b.Bottom && a.Bottom <= b.Top) ||
                (a.Top <= b.Top && a.Bottom >= b.Bottom) ||
                (a.Top >= b.Bottom && a.Bottom <= b.Bottom)))
            {
                return computeEntityDirection(a, b); 
            }
            else if (a.Bottom <= b.Bottom && a.Top >= b.Bottom &&
                ((a.Left <= b.Left && a.Right >= b.Left) ||
                (a.Left >= b.Left && a.Right <= b.Right) ||
                (a.Left >= b.Left && a.Left <= b.Right)))
            {
                return computeEntityDirection(a, b); 
                }
            else if (a.Top >= b.Top && a.Bottom <= b.Top &&
                ((a.Left <= b.Left && a.Right >= b.Left) ||
                (a.Left >= b.Left && a.Right <= b.Right) ||
                (a.Left >= b.Left && a.Left <= b.Right)))
            {
                return computeEntityDirection(a, b); 
            }

            return CollisionState.NO_COLLISIONS;
		}

        private static CollisionState computeEntityDirection(Entity a, Entity b)
        {
            float direction = 
                MathHelper.directionFromPointToPoint(a.Location, b.Location);

            float delta = //(a.Rotation >= 0) ? 
                MathHelper.normalizeAngle(a.Rotation - direction); //:direction - a.Rotation;

            if (delta < 0)
            {
                if (-delta < Math.PI / 4)
                    return CollisionState.COLLISION_TOP;
                else if (-delta < Math.PI * 3 / 4)
                    return CollisionState.COLLISION_RIGHT;
                else if (-delta < Math.PI)
                    return CollisionState.COLLISION_BOTTOM;
            }
            else
            {
                if (delta < Math.PI / 4)
                    return CollisionState.COLLISION_TOP;
                else if (delta < Math.PI * 3 / 4)
                    return CollisionState.COLLISION_RIGHT;
                else if (delta < Math.PI)
                    return CollisionState.COLLISION_BOTTOM;
            }

            var conversionFactor = 180 / Math.PI;
            Console.WriteLine(
                "Cal: " + a.Rotation * conversionFactor +
                "| dir: " + direction * conversionFactor +
                "| delta: " + delta * conversionFactor);

            return CollisionState.NO_COLLISIONS;
        }
		/// <summary>
		/// Tests for a collision between non-alpha pixels in two objects.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		private static CollisionState testForPixelCollisions( Entity a, Entity b ) {
			throw new NotImplementedException();
		}
	}
}
