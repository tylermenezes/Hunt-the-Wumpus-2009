using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using HuntTheWumpus.WorldAndStuff;

namespace HuntTheWumpus.GraphicsAndStuff.Drawers
{
    class BlockDrawer : EntityDrawer
    {
		string[] randomTextures = new string[]{
			"bush",
			"rock"
		};
        public BlockDrawer(WorldAndStuff.Entity STATIC) :
            base(STATIC)
        {
            // Don't Put anything here!
            // The drawing code goes in the EntityDrawer class, I moved it there
        }

        public override void SetupDrawer()
        {
			string randomTexture = randomTextures[Random.fish.Next(randomTextures.Length)];
            base.Animations.Add(EntityState.STATIC,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources[randomTexture],   // set Image here
                    1,      
                    0.1));

        }
    }
}
