using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HuntTheWumpus.WorldAndStuff;
namespace HuntTheWumpus.GraphicsAndStuff
{
    /// <summary>
    /// Draws Points Health and Number of Remaining Wumpi
    /// </summary>
    public class Overlay
    {
        public SpriteFont _font;
        private PlayerEntity _player;
		private World w;
		public List<KeyValuePair<int, string>> messagesToShow = new List<KeyValuePair<int,string>>();

        public void Initialize(PlayerEntity player, World world)
        {
			w = world;
            _player = player;
            _player.PlayerGainedPointsEvent += new PlayerEntity.PlayerGainedPointsEventHandler(_player_PlayerGainedPointsEvent);
        }

        void _player_PlayerGainedPointsEvent(int newAmount)
        {
            
        }

        public void LoadContent()
        {
            _font = (SpriteFont)CentralResourceRepository.Resources["font"];
        }

		public void ShowMessage(int showFor, string message) {
			messagesToShow.Add(new KeyValuePair<int, string>(showFor, message));
		}

        public void Draw(SpriteBatch spriteBatch)
        {
            var healthStr = "Health: " + _player.Health;
            var pointsStr = "Points: " + _player.Points;
			string numStr;
			numStr = "That's the last of 'em... for now.\nGather some points for upgrades while you have a chance.";

            spriteBatch.DrawString(_font, healthStr, Vector2.Zero, Color.Red);
            spriteBatch.DrawString(_font, pointsStr,
                new Vector2(
                    0, _font.MeasureString(healthStr).Y + 5), Color.Red);
			if(w.LiveWumpii <= 0)
            spriteBatch.DrawString(_font,numStr,
                new Vector2(
                    GameAndStuff.HTW.Instance.ScreenWidth -
                    _font.MeasureString(numStr).X, 0), Color.Red);

			int i = 1;
			var tmpMessages = new List<KeyValuePair<int, string>>();

			foreach (KeyValuePair<int, string> message in messagesToShow) {
				tmpMessages.Add(new KeyValuePair<int,string>(message.Key - 1, message.Value));
				if(message.Key - 1 <= 0) continue;

				Vector2 fontSize = _font.MeasureString(message.Value);
				spriteBatch.DrawString(_font, message.Value,
				new Vector2(
					GameAndStuff.HTW.Instance.ScreenWidth -
					fontSize.X, GameAndStuff.HTW.Instance.ScreenHeight - fontSize.Y * i), Color.Red);
			}

			messagesToShow = tmpMessages;
        }
    }
}
