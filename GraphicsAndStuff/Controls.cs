using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HuntTheWumpus.GraphicsAndStuff
{
    public class Controls
    {
        #region Structures
        public enum specialKeys
        {
            Exit = 0,
            Menu = 1,
            NextWeapon = 10,
            PreviousWeapon = 11,
            Reload = 13,
			CameraChange = 20
        }
        public enum MovementDirection 
        { 
            UP, 
            DOWN, 
            LEFT, 
            RIGHT, 
            NONE
        }
        public enum ActionType
        {
            FIRE,
            INTERACT,
            NextWeapon,
            PreviousWeapon
        }
        public enum XboxKeyMapping
        {
            Back = specialKeys.Exit,
            Start = specialKeys.Menu,
            RightShoulder = specialKeys.NextWeapon,
            LeftShoulder = specialKeys.PreviousWeapon,
            A = specialKeys.Reload
        }
        public enum pcKeyMapping
        {
            Escape = specialKeys.Exit,
            M = specialKeys.Menu,
            Q = specialKeys.NextWeapon,
            E = specialKeys.PreviousWeapon,
            Shift = specialKeys.Reload,
			CapsLock = specialKeys.CameraChange
        }
        #endregion
        #region Variables
        private PlayerIndex player;
        public float movementSpeed = 1;
        private MouseState _previousState;
        private MouseState _currentState;
        #endregion
        #region Subscribable Events
        // Set up the handler delegate for events from the controller or keyboard.
        public event RotationEventHandler RotationChanged;
        public event MovementEventHandler MovementOccured;
        public event ActionEventHandler ActionOccured;
        public event KeyPressEventHandler KeyPressed;
        bool movement = false;
        
        #endregion

        #region Constructor
        // Controls needs to be updated by the world, but other classes might
        // want to subscribe to events, so we make it a singleton.
        private static Controls _instance;

        #warning "NOT THREAD SAFE"
        public static Controls Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Controls();
                }
                return _instance;
            }
        }
        private Controls()
        {
            //Set the current player.
            player = PlayerIndex.One;
        }
        #endregion

		/// <summary>
		/// 
		/// XNA requires us to handle input at each update(), so we'll
		/// do that here and then send out events later.
		/// </summary>
		/// <param name="gameTime">The Game Time</param>
        
        public void handlePause(GameTime gameTime)
        {
            if (isKeyPressed(Keys.P))
            {
                if (HuntTheWumpus.GameAndStuff.HTW.Instance.pause == false)
                {
                    HuntTheWumpus.GameAndStuff.HTW.Instance.pause = true;   
                }
            }
            if (isKeyPressed(Keys.O))
            {
                if (HuntTheWumpus.GameAndStuff.HTW.Instance.pause == true)
                {
                    HuntTheWumpus.GameAndStuff.HTW.Instance.pause = false;
                }
            }

        }
        public void handleMainMenu(GameTime gameTime, int currentSelection)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (isKeyPressed(Keys.Up))
            {
                HuntTheWumpus.GameAndStuff.HTW.Instance.main.currentSelection = 0;
            }
            if (isKeyPressed(Keys.Down))
            {
                HuntTheWumpus.GameAndStuff.HTW.Instance.main.currentSelection = 1;
            }
            if (isKeyPressed(Keys.Enter))
            {
                if (currentSelection == 0)
                {
                    HuntTheWumpus.GameAndStuff.HTW.Instance.start = true;
                }
                else
                {
                    HuntTheWumpus.GameAndStuff.HTW.Instance.Exit();
                }
            }


        }
        public void handleInput(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            #region XBOX
                // TODO: Deal with this.
            #endregion
            #region PC
                #region Control Keys
                // Start handling pressed keys.
                if (isKeyPressed(Keys.Escape))
                {
                    KeyPressed(new KeyPressEventArgs((specialKeys)pcKeyMapping.Escape));
                }

                if (isKeyPressed(Keys.Space))
                {
                    KeyPressed(new KeyPressEventArgs((specialKeys)pcKeyMapping.Q));
                }
                if (isKeyPressed(Keys.Enter))
                {
                    KeyPressed(new KeyPressEventArgs((specialKeys)pcKeyMapping.E));
                }
				if (isKeyPressed(Keys.CapsLock))
				{
					KeyPressed(new KeyPressEventArgs((specialKeys)pcKeyMapping.CapsLock));
				}
                if (isKeyPressed(Keys.LeftShift) || isKeyPressed(Keys.RightShift))
                {

					ActionOccured(
						new ActionEventArgs(ActionType.NextWeapon,
							new Vector2(), gameTime));
					// KeyPressed(new KeyPressEventArgs((specialKeys)pcKeyMapping.Shift));
                }
                #endregion
                #region Movement
                // Handle movement.
                if (isKeyPressed(Keys.Up) || isKeyPressed(Keys.W))
                {
                    MovementOccured(
                        new MovementEventArgs(
                            Controls.MovementDirection.UP, gameTime)); //new Vector2(0, time * movementSpeed))); // (0, 1)
                    movement = true;
                }
                else if (isKeyPressed(Keys.Down) || isKeyPressed(Keys.S))
                {
                    MovementOccured(
                        new MovementEventArgs(Controls.MovementDirection.DOWN,
                            gameTime));
                    movement = true;
                }
                // These are basically useless now because of free-rotation
					//Uncommented and added strafing <t>
                else if (isKeyPressed(Keys.Left) || isKeyPressed(Keys.A))
                {
					MovementOccured(
					   new MovementEventArgs(Controls.MovementDirection.LEFT,
						   gameTime));
					movement = true;
                }
                else if (isKeyPressed(Keys.Right) || isKeyPressed(Keys.D))
                {
					MovementOccured(
					   new MovementEventArgs(Controls.MovementDirection.RIGHT,
						   gameTime));
					movement = true;
                }
                else if (movement == false)
                {
                    if (MovementOccured != null)
                    {
                        MovementOccured(
                            new MovementEventArgs(Controls.MovementDirection.NONE,
                                gameTime)); //new Vector2(time * movementSpeed, 0)));// (1, 0)
                    }
                    else 
                    {
                        //
                    }
                }
                #endregion
            updateMouse();            
                #region Action
            if (checkLeftButtonClick())
                ActionOccured(
                    new ActionEventArgs(ActionType.FIRE,
                        new Vector2(_currentState.X, _currentState.Y),
                        gameTime));
            else if (checkRightButtonClick())
                ActionOccured(
                    new ActionEventArgs(ActionType.INTERACT,
                        new Vector2(_currentState.X, _currentState.Y),
                        gameTime));
            else
            {
                int tick = checkScrolChange();
                if (tick < 0)
                    ActionOccured(
                        new ActionEventArgs(ActionType.NextWeapon,
                            new Vector2(), gameTime));
                else if (tick > 0)
                    ActionOccured(
                        new ActionEventArgs(ActionType.PreviousWeapon,
                            new Vector2(), gameTime));
            }
                #endregion
                #region Rotation
                if (checkMouseMotion())
                    RotationChanged(
                        new RotationEventArgs(
                        new Vector2(
                            _currentState.X, // - HTW.Instance.Window.ClientBounds.X,
                            _currentState.Y),
                         gameTime));// - HTW.Instance.Window.ClientBounds.Y))); 
                #endregion
            #endregion
        }

        private bool checkLeftButtonClick()
        {
            return 
                (_previousState.LeftButton == ButtonState.Pressed &&
                _currentState.LeftButton == ButtonState.Released);                
        }
        private bool checkRightButtonClick()
        {
            return 
                (_previousState.RightButton == ButtonState.Pressed &&
                _currentState.RightButton == ButtonState.Released);     
        }
        private int checkScrolChange()
        {
            return
                _currentState.ScrollWheelValue - 
                _previousState.ScrollWheelValue;
        }
        private void updateMouse()
        {            
            _previousState = _currentState;
            _currentState = Mouse.GetState();
        }
        private bool checkMouseMotion()
        {
            if (_currentState.X != _previousState.X ||
                _currentState.Y != _previousState.Y)
                return true;
            movement = false;
            return false;
        }

        private bool isKeyPressed(Keys key)
        {
            if (Keyboard.GetState(player).IsKeyDown(key))
                return true;
            
            return false; 
        }
    }

    #region Event handlers
    public delegate void RotationEventHandler(RotationEventArgs e);
    public delegate void MovementEventHandler(MovementEventArgs e);
    public delegate void ActionEventHandler(ActionEventArgs e);
    public delegate void KeyPressEventHandler(KeyPressEventArgs e);
    public class RotationEventArgs : EventArgs
    {
        public Vector2 Location { get; private set; }
        public GameTime TimeElapsed { get; private set; }

        public RotationEventArgs(Vector2 location, GameTime time)
        {
            this.Location = location;
            this.TimeElapsed = time;
        }
    }
    public class MovementEventArgs : EventArgs
    {
        public Controls.MovementDirection Direction { get; private set; }
        public GameTime TimeElapsed { get; private set; }

        public MovementEventArgs(Controls.MovementDirection direction, GameTime time)
        {
            Direction = direction;
            TimeElapsed = time;
        }
    }
    public class ActionEventArgs : EventArgs
    {
        public Controls.ActionType Type { get; private set; }
        public Vector2 RecieverLocation;

        public GameTime TimeElapsed { get; private set; }

        public ActionEventArgs(Controls.ActionType type,
            Vector2 recieverLocation,
            GameTime time)
        {
            Type = type;
            RecieverLocation = recieverLocation;
            TimeElapsed = time;
        }
    }
    public class KeyPressEventArgs : EventArgs
    {
        public readonly Controls.specialKeys keyType;

        /// <summary>
        /// Represents a key press across platforms.
        /// </summary>
        /// <param name="key">The key which was pressed.</param>
        public KeyPressEventArgs(Controls.specialKeys key)
        {
            keyType = key;
        }
    }
    #endregion
}

