using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GraphicsAndStuff.Drawers;
using HuntTheWumpus.GraphicsAndStuff;
using HuntTheWumpus.WorldAndStuff;
using HuntTheWumpus.WorldAndStuff.Weapons;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HuntTheWumpus.GameAndStuff
{
    public interface IGameState
    {

		SinglePlayerGameManager manager {get; }
		Overlay _overlay {get; }
        bool IsReady { get; }
        void Initialize();
        void LoadContent();
        void Update(GameTime time);
        void Draw(SpriteBatch spriteBatch, GameTime time);
        void Destroy();
    }
}
