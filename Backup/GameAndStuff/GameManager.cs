using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GraphicsAndStuff;
using HuntTheWumpus.WorldAndStuff;

namespace HuntTheWumpus.GameAndStuff
{
    abstract public class GameManager
    {
        public World CurrentWorld { get; protected set; }

        public event GameStartedEvent GameStarted;
        public event GameEndedEventHandler GameEnded; 

        public abstract PlayerEntity registerPlayer(Profile player);        
        public abstract void initializeGame();

		

        public bool GameRunning { get; private set; }

        public virtual void startGame()
        {
            GameRunning = true;
            if (GameStarted != null)
                GameStarted(
                    new GameStartedEventArgs());
        }
        public virtual void Update(Microsoft.Xna.Framework.GameTime time)
        {
            CurrentWorld.Update(time);
        }
        public virtual void endGame(GameEndedEventArgs.Circumstance reason)
        {
            if (GameEnded != null)
                GameEnded(
                    new GameEndedEventArgs(reason));
        }

        public GameManager()
        {
            GameRunning = false;
        }
    }

    public delegate void GameStartedEvent(GameStartedEventArgs e);
    public class GameStartedEventArgs : EventArgs { }

    public delegate void GameEndedEventHandler(GameEndedEventArgs e);
    public class GameEndedEventArgs : EventArgs
    {
        public enum Circumstance { WON, LOST, QUIT }
        public Circumstance Reason { get; private set; }

        public GameEndedEventArgs(Circumstance reason)
        {
            Reason = reason;
        }
    }
}