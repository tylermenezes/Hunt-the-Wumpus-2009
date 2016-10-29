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

// Quidquid latine dictum sit, altum sonatur.

namespace HuntTheWumpus.GameAndStuff {

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class HTW : Microsoft.Xna.Framework.Game
    {
#region SingletonSetup
        private static HTW _instance;

        public static HTW Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HTW();
                }
                return _instance;
            }
        }
#endregion

#region Properties
        public float TotalElapsedMilliseconds { get; private set; }
		public static readonly bool debugging = false;
        public GraphicsDeviceManager Graphics { get; private set; }
        public Network.Server Server { get; private set; }
        public Network.Client Client { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public bool start;
        public bool pause;
        
        public MainMenu main = new MainMenu();
        public float ScreenHeight
        {
            get
            {
                return Graphics.PreferredBackBufferHeight;
            }
        }
        public float ScreenWidth
        {
            get
            {
                return Graphics.PreferredBackBufferWidth;
            }
        }

        private IGameState _currentState;
        public IGameState CurrentGameState
        {
            get
            {
                return _currentState;
            }
            set
            {
                if (value == null)
                    this.Exit();
                else
                    _currentState = value;
            }
        }
#endregion

#region Fields
        public double TimeElasped;
#endregion

#region Setup

        private HTW()
        {
			// Intilize the client for a loopback
			// TODO: Allow network configuration
            Server = Program.Server; //new Network.Server(new System.Net.IPEndPoint(16777343L, 9867));
            
			if(debugging)
				Server.clientSendData += new HuntTheWumpus.Network.GenericIncomingDataHandler(Server_clientSendData);

            Client = Program.Client; //new Network.Client(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 9867));
			Client.send(ServerCommandTypes.CONTROL, "REGISTER");

            Graphics = new GraphicsDeviceManager(this);

            Graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";   
        }

		void Server_clientSendData( byte[] data, System.Net.EndPoint client ) { // Testing
			Console.Write(client.ToString().PadRight(18));
			Console.Write(Network.Server.getCommandType(Encoding.ASCII.GetString(data)).ToString().PadRight(15));
			Console.WriteLine(Network.Server.getCommand(Encoding.ASCII.GetString(data)));
		}

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            start = false;
            pause = false;
            // Start by moving the cursor to the middle of the screen for control purposes.
            Mouse.SetPosition(Graphics.GraphicsDevice.Viewport.Width / 2, Graphics.GraphicsDevice.Viewport.Height / 2);

            // Temporary : untill other states are written
            
            CurrentGameState = new SinglePlayerPlayState(
                new Profile(0,
                    "player",
                    PlayerEmployer.COMPANY_ONE,
                    new List<InventoryItem>() 
                    { 
                        InventoryItem.PISTOL, 

                    }
                    ));

            // Finish it up.
            base.Initialize();
        }
        

        //protected override void OnExiting( object sender, EventArgs args ) {
        //    try {
        //        QuitGame();
        //    } catch {
        //    }
        //    base.OnExiting(sender, args);
        //}

        /// <summary>
        /// Will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            CentralResourceRepository.LoadResources(Content);
            main.Initialize();
            CurrentGameState.LoadContent(); // crude will be fixed
        }

#endregion

#region Logic
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            TotalElapsedMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
			// Send out network events. -- When Networking is ready
            //Server.dispatchMessages();
            //Client.dispatchMessages();
            HuntTheWumpus.GraphicsAndStuff.Controls.Instance.handlePause(gameTime);
            if (start == false)
            {
                main.Update(gameTime);
            }
                    if (!CurrentGameState.IsReady) CurrentGameState.Initialize();
                    if (start == true)
                    {
                        if (pause == false)
                        {

                    CurrentGameState.Update(gameTime);
                }
            }
            base.Update(gameTime); // Update the game time.
        }
#endregion

#region Drawing
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            
            SpriteBatch.Begin(); // Start Drawing
            if (start == false)
            {

                main.Draw();
            }
            if (start == true)
            {

                CurrentGameState.Draw(SpriteBatch, gameTime);
            }
            SpriteBatch.End(); // Stop Drawing

            base.Draw(gameTime);
        } 
#endregion
	}
}
