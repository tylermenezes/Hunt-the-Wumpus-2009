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


namespace HuntTheWumpus.GraphicsAndStuff
{
    public delegate void AnimationChangeEvent();
	public class Animation 
	{
		public Texture2D Filmstrip {get; private set;}
		public double FrameRate { get; set; }
		public bool AnimationRunning { get; private set;}
		public bool IsLooping { get; private set;}
		public int FrameNumber { get; private set; }
		public int CurrentFrame {get; private set;}
        public Rectangle SourceRectangle { get { return _sourceRectangle; } }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                if (_isRunning)
                    AnimationStarted();
                else
                    AnimationStopped();
            }
        }

        public event AnimationChangeEvent AnimationStarted;
        public event AnimationChangeEvent AnimationStopped;

        private Rectangle _sourceRectangle;
        double animFrameElapsed;

		/// <summary>
		/// Represents a filmstrip-based animation.
		/// </summary>
		/// <param name="filmstrip">The Filmstrip to use for animations.</param>
		/// <param name="framenumber">Number of frames in the animation. Frame size is filmstrip width / this.</param>
		/// <param name="framerate">Frames per second.</param>
		public Animation
			( Texture2D filmstrip,
			int framenumber,
            double framerate)
		{
            
			Filmstrip = filmstrip;
			FrameNumber = framenumber;
			FrameRate = framerate;
            _sourceRectangle = new Rectangle(0, 0, filmstrip.Width / framenumber, filmstrip.Height);
            animFrameElapsed = 0;
			IsLooping = true;
            _isRunning = true;
		}
		/// <summary>
		/// Represents a filmstrip-based animation.
		/// </summary>
		/// <param name="filmstrip">The Filmstrip to use for animations.</param>
		/// <param name="framenumber">Number of frames in the animation. Frame size is filmstrip width / this.</param>
		/// <param name="framerate">Frames per second.</param>
		/// <param name="islooping">Should we loop the animation or play it once?</param>
		public Animation( Texture2D filmstrip,
			int framenumber,
            double framerate,
			bool islooping ) : this(filmstrip,framenumber,framerate) 
		{
			IsLooping = islooping;

		}

		/// <summary>
		/// Draws the frames given the current time.
		/// </summary>
		/// <param name="gametime">Current time in the game.</param>
		public void Update(Double gametime)  // Changed the name to Update because it doesn't really draw anything
		{
            if (IsRunning)
            {
                animFrameElapsed += gametime; //Says How Long the frame has bee going
                if (animFrameElapsed >= FrameRate)
                {
                    CurrentFrame = (CurrentFrame + 1) % FrameNumber;  //Changes the Frame
                    animFrameElapsed = 0;
                }
                //Creates the current view of the spritesheet based on the crrent frame.    
                _sourceRectangle.X = CurrentFrame * _sourceRectangle.Width;

                if (!IsLooping && _sourceRectangle.X ==
                    Filmstrip.Width - _sourceRectangle.Width)
                {
                    IsRunning = false;
                }
            }

		}

        public void Reset()
        {
            _sourceRectangle.X = 0;
            animFrameElapsed = 0;
        }
	}
}
