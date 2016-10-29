using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuntTheWumpus.GraphicsAndStuff.Drawers;
using HuntTheWumpus.GraphicsAndStuff;
using HuntTheWumpus.WorldAndStuff;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.GameAndStuff
{
    /// <summary>
    /// A player in the game. You just lost the game, by the way.
    /// </summary>
    public class Player
    {
        /// <summary>The entity representing the player.</summary>
        public WorldAndStuff.PlayerEntity Entity { get; private set; }
        /// <summary>The Drawer for Entity.</summary>
        public GraphicsAndStuff.Drawers.PlayerDrawer Drawer { get; private set; }
        /// <summary>The Player's Controller</summary>
        public GraphicsAndStuff.PlayerControler Controler { get; set; }
        public SinglePlayerPlayState ParentState { get; private set; }

        /// <summary>
        /// Sets up the game for the player.
        /// </summary>
        /// <param name="game">The playstate of the game which is being setup.</param>
        public Player(SinglePlayerPlayState state)
        {
            ParentState = state;
            Entity = null;
            Controler = null;
            Drawer = null;
        }

        /// <summary>
        /// Sets up an entity.
        /// </summary>
        /// <param name="position">The position of the entity.</param>
        public void SetupEntity(PlayerEntity entity)
        {
            Entity = entity;
        }

        void Entity_EntityAttacked(HuntTheWumpus.WorldAndStuff.EntityActionEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts handling the player's controls.
        /// </summary>
        public void SetupControler()
        {
            if (Entity == null)
                throw new Exception("PlayerEntity Not Setup!");
            else
                Controler = new HuntTheWumpus.GraphicsAndStuff.PlayerControler(Entity);
        }

        /// <summary>
        /// Starts handling the drawing of the player.
        /// </summary>
        public void SetupDrawer(PlayerDrawer drawer)
        {
            if (Entity == null)
                throw new Exception("PlayerEntity Not Setup!");
            else
                Drawer = drawer;
        }

        public void Cleanup()
        {
            Entity = null;
            Controler = null;
            Drawer = null;
        }
    }
}
