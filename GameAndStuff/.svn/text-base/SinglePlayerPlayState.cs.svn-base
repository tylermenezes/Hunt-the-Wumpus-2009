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
    public class SinglePlayerPlayState : IGameState
    {

        #region Properties
        public World CurrentWorld
        {
            get
            {
                return manager.CurrentWorld;
            }
        }
        public Profile PlayerProfile { get; private set; }
        public Player MainPlayer { get; private set; }
        public bool IsReady { get; private set; }
        #endregion


        #region Fields
        public SinglePlayerGameManager manager {get; private set;}
        Dictionary<Entity, EntityDrawer> _Livingdrawers;
        Dictionary<Entity, EntityDrawer> _Deaddrawers;
        Cursor _cursor;
        private bool once = true;
        private Camera _camera;
        private Controls _controls;
        public Overlay _overlay {get; private set;}
        #endregion

        #region Inititialize/Deinitialize
        public void Initialize()
        {
            //  Camera.Instance.Type = CameraType.CENTERED; 
            Camera.Instance.Type = CameraType.CENTERED; // <-- Uncomment this to change to a different style

            // Initialize GameManger
            manager = new SinglePlayerGameManager();

                manager.EntityAdded += new EntityChangeHandler(EntityAdded);
                manager.EntityRemoved += new EntityChangeHandler(EntityRemoved);
                manager.GameEnded += new GameEndedEventHandler(EndGame);
                  
            // Register Player
            var playerEntity =  manager.registerPlayer(PlayerProfile);
            MainPlayer.SetupEntity(playerEntity);

            playerEntity.PlayerGainedPointsEvent += new PlayerEntity.PlayerGainedPointsEventHandler(PlayerGainedPoints);
            // Listen for termination signals.
            _controls.KeyPressed += new KeyPressEventHandler(controls_KeyPressed);
            _cursor.SetupCursor
                (HTW.Instance.ScreenWidth / 2, HTW.Instance.ScreenHeight / 2);

            IsReady = true;

            //Start Game -- Crude
            NewGame();
        }

        void PlayerGainedPoints(int newAmount)
        {
            PlayerProfile.Points = newAmount;
        }

        public void LoadContent()
        {
            _cursor.LoadCursor(
                (Texture2D)CentralResourceRepository.Resources["Cursor"]);

            _overlay = new Overlay();
            _overlay.LoadContent();
            // BUG: If cursor is perfectly centered under wumpus when moving, Wumpus will disappear.

            //foreach (EntityDrawer drws in _drawers.Values)
            //    drws.SetupDrawer();
        }

        private void NewGame()
        {
            // needs to be fixed
            // Initialize World and Stuff
            manager.initializeGame();

            MainPlayer.SetupDrawer((PlayerDrawer)_Livingdrawers[MainPlayer.Entity]);
            MainPlayer.SetupControler();
            MainPlayer.Entity.PlayerMovedEventHandler += new PlayerEntity.PlayerMovedEvent(PlayerMovedEventHandler);



            Camera.Instance.LockOn(MainPlayer.Entity);

            manager.startGame();

            // init overlay
            _overlay.Initialize(MainPlayer.Entity, manager.CurrentWorld);
        }

        void EntityRemoved(HuntTheWumpus.WorldAndStuff.Entity entity)
        {
            _Livingdrawers.Remove(entity);
            _Deaddrawers.Remove(entity);
        }

        void EntityAdded(HuntTheWumpus.WorldAndStuff.Entity entity)
        {
            var newDrawer = EntityDrawer.CreateFromEntity(entity);
            newDrawer.SetupDrawer();
            _Livingdrawers.Add(entity, newDrawer);


            if (entity.Class == EntityClass.PLAYER || entity.Class == EntityClass.WUMPUS)
                ((DynamicEntity)entity).EntityDiedEvent += new DynamicEntity.EntityDiedEventHandler(SinglePlayerPlayState_EntityDiedEvent);

        }

        void SinglePlayerPlayState_EntityDiedEvent(DynamicEntity entity)
        {
			try {
				var drwr = _Livingdrawers[entity];
				_Livingdrawers.Remove(entity);
				_Deaddrawers.Add(entity, drwr);
			} catch {
			}
        }


        /// <summary>
        /// Handles game termination gracefully.
        /// </summary>
        public void EndGame(GameEndedEventArgs e)
        {
            // Give Game over Message etc. Here
            switch (e.Reason)
            {
                case GameEndedEventArgs.Circumstance.WON:
					HTW.Instance.CurrentGameState.manager.placeRandomEnemies();
					break;
                case GameEndedEventArgs.Circumstance.LOST:
                    PlayerProfile.Points /= 3;
                    _Livingdrawers.Clear();
                    MainPlayer.Cleanup();
                    IsReady = false;
                    
                    HuntTheWumpus.GameAndStuff.HTW.Instance.start = false;


					return;
                    
					break;
				case GameEndedEventArgs.Circumstance.QUIT:
					HTW.Instance.Exit();
					break;

            }      
        }

        public void Destroy()
        {
            _Livingdrawers.Clear();
            MainPlayer.Cleanup();

            // Switch State
          HTW.Instance.CurrentGameState = null;
        } 
        #endregion

        #region Logic
        public void Update(GameTime gameTime)
        {
            // Look for input and dispatch events if some is found.
            MainPlayer.Controler.handeInput(gameTime);

            // Update Game
            manager.Update(gameTime);
        }
        void PlayerMovedEventHandler(PlayerMovedEventArgs e)
        {
            Camera.Instance.Update(e.TimeElapsed);
        }

        void controls_KeyPressed(KeyPressEventArgs e)
        {
            switch (e.keyType)
            {
                case Controls.specialKeys.Exit:
                    this.EndGame(
                        new GameEndedEventArgs(GameEndedEventArgs.Circumstance.QUIT));
                    break;
                case Controls.specialKeys.CameraChange:
                    Camera.Instance.LockOn(MainPlayer.Entity);
                    break;
            }
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            var TimeElasped = gameTime.ElapsedGameTime.TotalSeconds;

            var deaddrwrs = _Deaddrawers.Values.ToArray();
            for (int i = 0; i < _Deaddrawers.Count; i++)
            {
                deaddrwrs[i].Draw(spriteBatch, TimeElasped);
            }

            var livedrwrs = _Livingdrawers.Values.ToArray();
            for (int i = 0; i < _Livingdrawers.Count; i++)
            {
				try {
					livedrwrs[i].Draw(spriteBatch, TimeElasped);
				} catch {
				}
            }

                _overlay.Draw(spriteBatch);
                _cursor.Draw(spriteBatch);

        } 
        #endregion

        public SinglePlayerPlayState(Profile profile)
        {
            PlayerProfile = profile;
            _Livingdrawers = new Dictionary<Entity, EntityDrawer>();
            _Deaddrawers = new Dictionary<Entity, EntityDrawer>();
            MainPlayer = new Player(this);
            _cursor = Cursor.Instance;

            _camera = Camera.Instance;
            _controls = Controls.Instance;
        }
    }
}
