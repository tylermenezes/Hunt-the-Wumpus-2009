using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using HuntTheWumpus.WorldAndStuff;
using HuntTheWumpus.GraphicsAndStuff;

namespace HuntTheWumpus.GraphicsAndStuff
{
    public class MainMenu
    {
        Texture2D _background, newButton, quitButton, menuCursor;
        public int currentSelection = 0;
        Controls controls;
        private PlayerIndex player = PlayerIndex.One;
        public void Initialize()
        {
            // Load Images
            _background = (Texture2D)CentralResourceRepository.Resources["menuBackground"];
            newButton = (Texture2D)CentralResourceRepository.Resources["newGameButton"];
            quitButton = (Texture2D)CentralResourceRepository.Resources["quitGameButton"];
            menuCursor = (Texture2D)CentralResourceRepository.Resources["menuCursor"];
            KeyPressEventHandler h = new KeyPressEventHandler(keyPressed);
        }

        public void Update(GameTime time)
        {
            HuntTheWumpus.GraphicsAndStuff.Controls.Instance.handleMainMenu(time, currentSelection);
            Console.WriteLine(currentSelection);

        }
        void keyPressed(KeyPressEventArgs e)
        {
            if (Keyboard.GetState(player).IsKeyDown(Keys.Enter))
            {
				//if (currentSelection == 0)
				//    HuntTheWumpus.GameAndStuff.HTW.Instance.NewGame()				//else
				//    HuntTheWumpus.GameAndStuff.HTW.Instance.QuitGame();
            }
            if (Keyboard.GetState(player).IsKeyDown(Keys.Up))
                currentSelection = 0;
            if (Keyboard.GetState(player).IsKeyDown(Keys.Down))
                currentSelection = 1;
        }
        public void ChangeSelection(int cur)
        {
            if (cur == 0)
            {
                currentSelection = 0;
            }
            else { currentSelection = 1; }
        }
        public void Draw()
        {
            int width = (int)HuntTheWumpus.GameAndStuff.HTW.Instance.ScreenWidth;
            int height = (int)HuntTheWumpus.GameAndStuff.HTW.Instance.ScreenHeight;
            Rectangle menuRect = new Rectangle(0, 0, width, height);

            //Draw the images
            HuntTheWumpus.GameAndStuff.HTW.Instance.SpriteBatch.Draw(_background,
                menuRect,
                Color.White);
            HuntTheWumpus.GameAndStuff.HTW.Instance.SpriteBatch.Draw(newButton,
                new Rectangle(200, 200, 100, 50),
                Color.White);
            HuntTheWumpus.GameAndStuff.HTW.Instance.SpriteBatch.Draw(quitButton,
                new Rectangle(300, 300, 100, 50),
                Color.White);

            //Draw the cursor next to the selected button 50 pixels to the left of the selected button.
            if (currentSelection == 0)
                HuntTheWumpus.GameAndStuff.HTW.Instance.SpriteBatch.Draw(menuCursor,
                    new Rectangle(150, 200, 50, 50),
                    Color.White);
            else if (currentSelection == 1)
                HuntTheWumpus.GameAndStuff.HTW.Instance.SpriteBatch.Draw(menuCursor,
                    new Rectangle(250, 300, 50, 50),
                    Color.White);
        }
    }
}