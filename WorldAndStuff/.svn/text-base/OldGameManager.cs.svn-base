//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using HuntTheWumpus.GraphicsAndStuff;
//using Microsoft.Xna.Framework.Graphics;


//namespace HuntTheWumpus.WorldAndStuff
//{
//    public class GameManager
//    {
//        // Manages Actually Game such as time, gameover etc.
//        // That is - if the game were multiplayer this would be shared

//        public bool GameRunning { get; private set; }
//        public World CurrentWorld { get; private set; }

//        public event EntityChangeHandler EntityAdded;
//        public event EntityChangeHandler EntityRemoved;

//        List<HTW> _registrees;

//        public GameManager() { _registrees = new List<HTW>(); }

//        // Will Merge Later -- Sorry
//        public void CreateRandomWorld(System.Drawing.Size worldSize)
//        {
//            CurrentWorld = new World();
//            CurrentWorld.EntityAdded   += EntityAdded;
//            CurrentWorld.EntityRemoved += EntityRemoved;

//            EntityRemoved += new EntityChangeHandler(GameManager_EntityRemoved);

//            // Randomly Place Wumpii, etc. on Map
//            var rand = new System.Random();
//            for (int i = rand.Next(worldSize.Width / 50, worldSize.Width / 25); i >= 0; i--)
//            {
//                new BlockEntity("blockTester"+i,
//                    new Vector3(rand.Next(worldSize.Width / 2), rand.Next(worldSize.Height / 2), 0),
//                    CurrentWorld);
//            }
//            // Create map limits
//            Vector2[] cityLimits = new Vector2[2];
//            Vector2 origin = new Vector2(Camera.Instance.XOffset + GraphicsAndStuff.HTW.Instance.ScreenWidth / 2, Camera.Instance.YOffset + GraphicsAndStuff.HTW.Instance.ScreenHeight / 2);
//            cityLimits[0] = new Vector2(origin.X - .5f * worldSize.Width, origin.Y - .5f * worldSize.Width);
//            cityLimits[1] = new Vector2(origin.X + .5f * worldSize.Width, origin.Y + .5f * worldSize.Width);

//            for (int i = 0; cityLimits[0].X + i < cityLimits[1].X; ) {

//                    i += (int)new BlockEntity("boundry1" + i,
//                        new Vector3(cityLimits[0].X + i, cityLimits[0].Y, 0),
//                        CurrentWorld).Size.X;

//                    new BlockEntity("boundry2" + i,
//                            new Vector3(cityLimits[0].X + i, cityLimits[1].Y, 0),
//                            CurrentWorld);
//            }

//            for (int i = 0; cityLimits[0].Y + i < cityLimits[1].Y; ) {

//                i += (int)new BlockEntity("boundry3" + i,
//                    new Vector3(cityLimits[0].X, cityLimits[0].Y + i, 0),
//                    CurrentWorld).Size.X;

//                new BlockEntity("boundry4" + i,
//                        new Vector3(cityLimits[1].X, cityLimits[0].Y + i, 0),
//                        CurrentWorld);
//            }

//            GameRunning = true;
            
//        }

//        void GameManager_EntityRemoved(Entity entity)
//        {
//            if (CurrentWorld.EntityCount() == 0)
//            {
//                this.EndGame();
//            }
//        }

//        public void Register(HTW registree)
//        {
//            _registrees.Add(registree);
         
//        }

//        public void Update(GameTime time)
//        {
//            CurrentWorld.Update(time);
//            if (CurrentWorld.EntityCount() == 0) EndGame();
//        }
//        public void EndGame()
//        {
//            GameRunning = false;
//        }
//    }
//}
