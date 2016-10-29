using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using HuntTheWumpus.GraphicsAndStuff;
using HuntTheWumpus.WorldAndStuff;

using Microsoft.Xna.Framework;

namespace HuntTheWumpus.GameAndStuff
{

    public class SinglePlayerGameManager : GameManager
    {
        #region Properties
        public Profile CurrentPlayerProfile { get; private set; } 
        #endregion

        #region Event
        public event EntityChangeHandler EntityAdded;
        public event EntityChangeHandler EntityRemoved;
        #endregion

        #region Fields
        private PlayerEntity _playerEntity;
		private Timer timeToNextLevel = new Timer();
		private bool levelLockout = false;

		private int _unique = 0;
		private int Unique {
			get {
				return ++_unique;
			}
		}
        #endregion

        #region Initialize
        public override PlayerEntity registerPlayer(Profile player)
        {
            CurrentPlayerProfile = player;
            return
                _playerEntity = new PlayerEntity(player);
        }
        public override void initializeGame()
        {
            CurrentWorld = new World(
                new Vector2(2000, 2000));
            CurrentWorld.EntityAdded += EntityAdded;
            CurrentWorld.EntityRemoved += new EntityChangeHandler(GameManager_EntityRemoved);

            _playerEntity.initialize(CurrentWorld,
                new Vector3(1000, 1000, 0));
            _playerEntity.EntityDiedEvent += new DynamicEntity.EntityDiedEventHandler(_playerEntity_PlayerDiedEvent);

             // Randomly Place Wumpuses etc. (Wumpi?) on Map
            CreateRandomWorld();
        }
        void _playerEntity_PlayerDiedEvent(DynamicEntity player)
        {
            endGame(GameEndedEventArgs.Circumstance.LOST);
        }
        #endregion

        #region Logic
        
        void GameManager_EntityRemoved(Entity entity)
        {
            if (EntityRemoved != null) EntityRemoved(entity);
            if (CurrentWorld.LiveWumpii <= 0 && !levelLockout)
            {
				levelLockout = true; // Prevents races.
				timeToNextLevel.Interval = 10000;
				timeToNextLevel.AutoReset = false;
				timeToNextLevel.Elapsed += new ElapsedEventHandler(timeToNextLevel_Elapsed);
				timeToNextLevel.Start();
				CurrentPlayerProfile.Level++;
				HTW.Instance.CurrentGameState._overlay.ShowMessage(150, "You reached level " + CurrentPlayerProfile.Level);
				_playerEntity.Health += 50 * (int)(CurrentPlayerProfile.Level * (100 / (_playerEntity.Health + 1)));
            }
        }

		void timeToNextLevel_Elapsed( object sender, ElapsedEventArgs e ) {
			timeToNextLevel.Enabled = false;
			levelLockout = false;
			timeToNextLevel.Stop();

			this.endGame(GameEndedEventArgs.Circumstance.WON);
		}

        #endregion
        
        #region Private Subroutines

        private bool doCoincide(Vector4 a, Vector4 b)
        {
            var xDis = Math.Abs(a.X - b.X);
            var yDis = Math.Abs(a.X - b.X);

            return (xDis < ((a.Z > b.Z) ? a.Z : b.Z) &&
                yDis < ((a.W > b.W) ? a.W : b.W));
        }

		private Vector3 hiddenVector() {
			Vector2 worldSize = CurrentWorld.WorldSize;
			System.Random rand = Random.fish;
			Vector3 vector = new Vector3(rand.Next((int)worldSize.X / 2),
										rand.Next((int)worldSize.Y / 2),
										(float)(rand.NextDouble() * Math.PI * 2));
			while (!isHidden(vector.X, vector.Y)) {
				vector = new Vector3(rand.Next((int)worldSize.X / 2),
										rand.Next((int)worldSize.Y / 2),
										(float)(rand.NextDouble() * Math.PI * 2));
			}
			return vector;
		}

		private bool isHidden( float x, float y ) {
			if (x > Camera.Instance.XOffset && x < Camera.Instance.XOffset + HTW.Instance.ScreenWidth) {
				return false;
			}
			if (y < Camera.Instance.YOffset && y > Camera.Instance.YOffset + HTW.Instance.ScreenHeight) {
				return false;
			}
			return true;
		}

		public void placeRandomEnemies() {
			int pointMult = (int)CurrentPlayerProfile.Level;
			Console.WriteLine(CurrentPlayerProfile.Level);

			// Randomly Place Wumpii, etc. on Map
			var worldSize = CurrentWorld.WorldSize;
			var rand = Random.fish;

			var playerRect =
	new Vector4(
		_playerEntity.Location,
		_playerEntity.Size.X, _playerEntity.Size.Y);

			// Add Wumpi 
			for (int i = 0 + pointMult; i >= 0; i--) {
				var wumpus =
					new Wumpus5Entity("wumpus" + Unique);

				var rect =
					new Vector4(wumpus.Location,
						wumpus.Size.X, wumpus.Size.Y);
				if (!doCoincide(rect, playerRect))
					wumpus.initialize(CurrentWorld,
						   new Vector3(
								rand.Next((int)(worldSize.X / 2)),
								rand.Next((int)(worldSize.Y / 2)),
								(float)(rand.NextDouble() * Math.PI * 2)));
			}

			// Add Wampi
			for (int i = rand.Next(5, 7 + pointMult); i >= 0; i--) {
				var wampus =
					new Wampus1Entity("wampus" + Unique);

				var rect =
					new Vector4(wampus.Location,
						wampus.Size.X, wampus.Size.Y);
				if (!doCoincide(rect, playerRect))
					wampus.initialize(CurrentWorld,
						   new Vector3(
								rand.Next((int)(worldSize.X / 2)),
								rand.Next((int)(worldSize.Y / 2)),
								(float)(rand.NextDouble() * Math.PI * 2)));
			}

			// Add Random Wimpi
			for (int i = rand.Next(10, 15 + pointMult); i >= 0; i--) {
				var wimpus =
					new Wimpus3Entity("wimpus" + Unique);

				var rect =
					new Vector4(wimpus.Location,
						wimpus.Size.X, wimpus.Size.Y);
				if (!doCoincide(rect, playerRect))
					wimpus.initialize(CurrentWorld,
						   new Vector3(
								rand.Next((int)(worldSize.X / 2)),
								rand.Next((int)(worldSize.Y / 2)),
								(float)(rand.NextDouble() * Math.PI * 2)));
			}

		}

        private void CreateRandomWorld()
        {
            // Randomly Place Wumpii, etc. on Map
            var worldSize = CurrentWorld.WorldSize;
            var rand = Random.fish;

            // Create map limits
            Vector2[] cityLimits = new Vector2[2];
            Vector2 origin = new Vector2(Camera.Instance.XOffset + GameAndStuff.HTW.Instance.ScreenWidth / 2, Camera.Instance.YOffset + GameAndStuff.HTW.Instance.ScreenHeight / 2);
            cityLimits[0] = new Vector2(origin.X - .5f * worldSize.X, origin.Y - .5f * worldSize.Y);
            cityLimits[1] = new Vector2(origin.X + .5f * worldSize.X, origin.Y + .5f * worldSize.Y);
            Vector2[] sector = new Vector2[9];
			
			// ???
            for (int i = 1; i >= 10; i++)
            {
                int sec = i % 3;
                if (sec == 0) 
                {
                    sec = 3;
                }
                  
                sector[i] = new Vector2( worldSize.X / 3 * sec, worldSize.Y / 3);
                Console.WriteLine(sector[i]);
            }

			placeRandomEnemies();
             //Add Random blocks
            for (int i = rand.Next((int)(worldSize.X / 10), (int)(worldSize.X / 5)); i >= 0; i--)
            {

                new BlockEntity("rblock1" + i)
                    .initialize(
                       CurrentWorld,
                       new Vector3(
								rand.Next(-(int)worldSize.X / 2, (int)worldSize.X),
								rand.Next(-(int)worldSize.Y / 2, (int)worldSize.Y),
                                (float)(rand.NextDouble() * Math.PI * 2)));
            }

            // Add forest bounds.
            int sizex = (int)new BlockEntity("boundrytx").Size.X;
            int sizey = (int)new BlockEntity("boundryty").Size.Y;
            for (int i = 0; cityLimits[0].X + i + sizex * .05 <= cityLimits[1].X; i += sizex)
            {
                new BlockEntity("boundry1" + i)
                    .initialize(
                       CurrentWorld,
                       new Vector3(cityLimits[0].X + i, cityLimits[0].Y, (float)(rand.NextDouble() * Math.PI * 2)));

                new BlockEntity("boundry2" + i)
                    .initialize(
                        CurrentWorld,
                        new Vector3(cityLimits[0].X + i, cityLimits[1].Y, (float)(rand.NextDouble() * Math.PI * 2)));
            }

            for (int i = 0; cityLimits[0].Y + i + sizey * .05 <= cityLimits[1].Y; i += sizey)
            {
                new BlockEntity("boundry3" + i)
                    .initialize(
                        CurrentWorld,
                        new Vector3(cityLimits[0].X, cityLimits[0].Y + i, 0));

                new BlockEntity("boundry4" + i)
                    .initialize(
                        CurrentWorld,
                        new Vector3(cityLimits[1].X, cityLimits[0].Y + i, 0));
            }

        }
        #endregion
    }
}
