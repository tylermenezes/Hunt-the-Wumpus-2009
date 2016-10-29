using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.GraphicsAndStuff
{
    class Cursor
    {
        private Texture2D _image;
        public Vector2 Location { get; private set; }
        private Controls _input;

        public static Cursor Instance { get; private set; }

        static Cursor()
        {
            Instance = new Cursor();
        }

        private Cursor()
        {
            Location = Vector2.Zero;
            _input = Controls.Instance;
        }

        public void LoadCursor(Texture2D image)
        {
            _image = image;
        }

        public void SetupCursor(float x, float y)
        {
            _input.RotationChanged += new RotationEventHandler(_input_ViewChanged);
            Location = new Vector2(x, y);
        }

        void _input_ViewChanged(RotationEventArgs e)
        {
            Location = new Vector2(
                e.Location.X,
                e.Location.Y);
        }

        public void Draw(SpriteBatch sprieBatch)
        {
            sprieBatch.Draw(
                _image,
                Location,
                null,
                Color.White,
                0,
                new Vector2(_image.Width / 2, _image.Height / 2),
                1,
                SpriteEffects.None,
                1);
        }
    }
}
