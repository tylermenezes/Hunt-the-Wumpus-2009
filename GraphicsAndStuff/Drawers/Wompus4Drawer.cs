﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using HuntTheWumpus.WorldAndStuff;
namespace HuntTheWumpus.GraphicsAndStuff.Drawers
{
    class Wompus4Drawer : EntityDrawer
    {
        public Wompus4Drawer(WorldAndStuff.Wompus4Entity wompus) :
            base(wompus)
        { 
            // Don't Put anything here!
            // The drawing code goes in the EntityDrawer class, I moved it there
        }

        public override void SetupDrawer()
        {
            base.Animations.Add(EntityState.RESTING,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["WumpusExample"], // add a animation or picture here 
                                                              // and to the content folder
                    1,  // Number of frames in animation goes here
                    0.1));  // Framerate goes here

        }
    }
}
