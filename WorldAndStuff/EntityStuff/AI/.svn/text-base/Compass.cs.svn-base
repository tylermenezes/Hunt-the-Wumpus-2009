using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
namespace HuntTheWumpus.WorldAndStuff.AI
{
    public abstract partial class EnemyEntity : DynamicEntity
    {
        // Methods belong to EnemyEntity
        // They sould be called

        public enum WumpusTarget { ENEMY, FRIEND, WEAPON, NOTHING }

        private WumpusTarget Target = WumpusTarget.NOTHING;
        protected void playerEnter(PlayerEntity ent, float distance)
        {
            Rotation =
                MathHelper.directionFromPointToPoint(this.Location,
                    ent.Location);
            if (distance <= this.ActiveWeapon.AttackRadius)
                this.State = EntityState.ATTACKING;
            else
            {
                this.State = EntityState.MOVING;
                this.Target = WumpusTarget.ENEMY;
            }
        }

        protected void enemyEnter(EnemyEntity ent, float distance)
        {
            Rotation =
                MathHelper.directionFromPointToPoint(this.Location,
                    ent.Location);
            if (distance <= this.ActiveWeapon.AttackRadius)
                this.State = EntityState.ATTACKING;
            else
            {
                this.Target = WumpusTarget.ENEMY;
                this.State = EntityState.MOVING;
            }
        }
        

        protected void friendEnter(EnemyEntity ent, float distance)
        {
            if (this.Target == WumpusTarget.NOTHING)
            {
                // Must find better algo
                var direction = MathHelper.directionFromPointToPoint(
                    this.Location, ent.Location);
                if (ent.State == EntityState.MOVING)
                {
                    if (this.State == EntityState.MOVING)
                    {
                        if (ent.Rotation != this.Rotation)
                            ent.Rotation = Rotation = (ent.Rotation + this.Rotation + direction) / 3;
                    }
                    else
                        Rotation = (ent.Rotation + direction) / 2;
                }
                {
                    this.Target = WumpusTarget.FRIEND;
                    this.State = EntityState.MOVING;
                }
            }
        }

        private static class Compass
        {
			static int lonely = 0;

            public enum AlgorythmType { BOX, CIRCLE } // other types could be added

            public static void checkForCreatures(EnemyEntity caller, AlgorythmType algo)
            {
                var dynms = caller.ParentWorld.getAllEnemyEntities();

                Dictionary<EnemyEntity, float> friends = new Dictionary<EnemyEntity, float>();
                Dictionary<DynamicEntity, float> enemies = new Dictionary<DynamicEntity, float>();

				lonely++;
				if (lonely >= 10) {
					Console.WriteLine(caller.Type.ToString() + " is lonely! ");
					caller.SightRadius += 25;
				}

                for (int i = dynms.Count-1; i >= 0; i--)
                {
                    if (dynms[i] == caller) continue;
                    if (dynms[i].State == EntityState.DEAD) continue;

                    var dist = MathHelper.distanceFromPointToPoint(
                        caller.Location, dynms[i].Location);
                    if ( dist <= caller.SightRadius)
                    {
                        if (caller.Type == dynms[i].Type)
                            friends.Add((EnemyEntity)dynms[i], dist);
                        else
                            enemies.Add(dynms[i], dist);
                    }
                }

                if (enemies.Count > 0 )
                {
					lonely = 0;
                    var enemy = pickClosestOne(enemies);
                    if (enemy.Class == EntityClass.PLAYER)
                        caller.playerEnter((PlayerEntity)enemy, MathHelper.distanceFromPointToPoint(
                            caller.Location, enemy.Location));
                    else if (enemy.Class == EntityClass.WUMPUS)
                        caller.enemyEnter((EnemyEntity)enemy, MathHelper.distanceFromPointToPoint(
                            caller.Location, enemy.Location));
                }
                else if (friends.Count > 0)
                {
                    var friend = pickClosestOne(friends);
                    caller.friendEnter(friend,
                        MathHelper.distanceFromPointToPoint(
                        caller.Location, friend.Location));
                }
            }

            private static DynamicEntity pickClosestOne(Dictionary<DynamicEntity, float> enemies)
            {
                DynamicEntity closest = null;
                foreach (DynamicEntity ent in enemies.Keys)
                {
                    if (closest == null) closest = ent;
                    if (enemies[ent] < enemies[closest])
                        closest = ent;
                }
                return closest;
            }

            private static EnemyEntity pickClosestOne(Dictionary<EnemyEntity, float> friends)
            {
                EnemyEntity closest = null;
                foreach (EnemyEntity ent in friends.Keys)
                {
                    if (closest == null) closest = ent;
                    if (friends[ent] < friends[closest])
                        closest = ent;
                }
                return closest;
            }
        }
    }
}
