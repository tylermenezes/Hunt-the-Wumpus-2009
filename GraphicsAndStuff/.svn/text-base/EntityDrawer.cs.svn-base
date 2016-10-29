using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using HuntTheWumpus.WorldAndStuff;
using HuntTheWumpus.WorldAndStuff.Weapons.Projectiles;

namespace HuntTheWumpus.GraphicsAndStuff
{
    public abstract class EntityDrawer : I3DObject
    {
        public Entity Entity { get; private set; }
        public Dictionary<EntityState, Animation> Animations { get; private set; }

        public EntityDrawer(Entity entity)
        {
            Entity = entity;
            Animations = new Dictionary<EntityState, Animation>();
            entity.ChangeStateEventHandler += new Entity.ChangeStateEvent(entity_changeState);
        }

        void entity_changeState(EntityState newState)
        {
            if (Animations.ContainsKey(Entity.State))
                Animations[Entity.State].Reset();
        }

        /// <summary>
        /// Creates an appropriate [[Drawer]] from the type of Entity.
        /// </summary>
        /// <param name="ent">The entity for which a Drawer is needed</param>
        /// <returns>New [[PlayerDrawer]] or null if none exists.</returns>
        public static EntityDrawer CreateFromEntity(Entity ent)
        {
		// Going to get Ugly - so would appreciate alternatives

            // when a new drawer/entity pair is created add them here
            switch (ent.Type)
            {
                case EntityType.PLAYER:
                    return new Drawers.PlayerDrawer((PlayerEntity)ent);

                    // Enemies
                case EntityType.WUMPUS5:
                    return new Drawers.Wumpus5Drawer((Wumpus5Entity)ent);
                case EntityType.WIMPUS3:
                    return new Drawers.Wimpus3Drawer((Wimpus3Entity)ent);
                case EntityType.WAMPUS1:
                    return new Drawers.Wampus1Drawer((Wampus1Entity)ent);

                    //Terrain Objects
                case EntityType.BLOCK:
                    return new Drawers.BlockDrawer((BlockEntity)ent);

                    //Weapons
                case EntityType.PISTOLBULLET:
                    return new Drawers.WeaponDrawers.PistolBulletDrawer((PistolBulletEntity)ent);
                case EntityType.SHOTGUNBULLET:
                    return new Drawers.WeaponDrawers.ShotgunBulletDrawer((ShotgunBulletEntity)ent);
                case EntityType.ROCKET:
                    return new Drawers.WeaponDrawers.RocketDrawer((RocketEntity)ent);



                default: throw new Exception("EntityDrawer not found for Entity given");
            }
        }
        public abstract void SetupDrawer(); // use (Texture2D)CentralResourceRepository.Resources["fielname"] instead

        protected float scale = 1;
		/// <summary>
		/// Handles drawing of a SpriteBatch.
		/// </summary>
		/// <param name="spritebatch">Sprites to draw.</param>
		/// <param name="gametime">The Game Time.</param>
        public void Draw(SpriteBatch spritebatch, Double gametime)
        {

            if (Animations.ContainsKey(Entity.State))
            {
                //gets the varribles based on the animations state
                Animation anim = Animations[Entity.State];

                //Gets the view of the current frame
                anim.Update(gametime);

                //Draws the sprite. Need to add where it gets rotation if we want that
                spritebatch.Draw(anim.Filmstrip,
                        new Vector2(
                            Entity.X + Camera.Instance.XOffset,
                            Entity.Y + Camera.Instance.YOffset),
                        anim.SourceRectangle,
                        Color.White,
                        -Entity.Rotation + (float)Math.PI / 2,
                        new Vector2(anim.SourceRectangle.Width / 2, anim.SourceRectangle.Height / 2),
                        scale,
                        SpriteEffects.None,
                        1);
            }
        }
            
    }
}
