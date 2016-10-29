using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using HuntTheWumpus.WorldAndStuff;
namespace HuntTheWumpus.GraphicsAndStuff.Drawers
{
    class Wimpus3Drawer : EntityDrawer
    {
        public Wimpus3Drawer(WorldAndStuff.Wimpus3Entity wimpus) :
            base(wimpus)
        { 
            // Don't Put anything here!
            // The drawing code goes in the EntityDrawer class, I moved it there
        }

        public override void SetupDrawer()
        {
            base.Animations.Add(EntityState.RESTING,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["restingwimpus"], // add a animation or picture here 
                                                              // and to the content folder
                    1,  // Number of frames in animation goes here
                    0.1));  // Framerate goes here
            base.Animations.Add(EntityState.MOVING,
                new Animation(
                    (Texture2D)CentralResourceRepository
                        .Resources["walkingwimpus"], 
                    4,
                    0.1));
            base.Animations.Add(EntityState.ATTACKING,
                new Animation(
                    (Texture2D)CentralResourceRepository
                        .Resources["attackingwimpus"],
                    3,
                    0.1));
            base.Animations.Add(EntityState.DEAD,
                new Animation(
                    (Texture2D)CentralResourceRepository
                        .Resources["deadwimpus"],
                    1,
                    0.1));
            scale = 2f;
        }
    }
}
