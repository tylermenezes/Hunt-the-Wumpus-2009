using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.WorldAndStuff;
using HuntTheWumpus.GameAndStuff;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.GraphicsAndStuff
{
    public class PlayerControler
    {
        // TODO: Camera and Controls integration here
        public PlayerEntity Player { get; private set; }

        private SinglePlayerPlayState _playState;
        private Controls _controls;

		/// <summary>
		/// Represents a player which can control the game.
		/// </summary>
		/// <param name="player">A player.</param>
        public PlayerControler(PlayerEntity player)
        {
            Player = player;
            _controls = Controls.Instance;

            _controls.KeyPressed += new KeyPressEventHandler(KeyPressed);
            _controls.MovementOccured += new MovementEventHandler(_controls_MovementOccured);
            _controls.RotationChanged += new RotationEventHandler(_controls_ViewChanged);
            _controls.ActionOccured += new ActionEventHandler(_controls_ActionOccured);

            _playState = (SinglePlayerPlayState)HTW.Instance.CurrentGameState;
        }

        void _controls_ActionOccured(ActionEventArgs e)
        {
            switch (e.Type)
            {
                case Controls.ActionType.FIRE:
                    Player.AttackEntity(e.TimeElapsed);
                    break;
                case Controls.ActionType.INTERACT:

                    var reciever = _playState.CurrentWorld.findEntity(
                        new Vector2(
                            e.RecieverLocation.X - Camera.Instance.XOffset,
                            e.RecieverLocation.Y - Camera.Instance.YOffset));

                    if (reciever.Key != null &&
                        reciever.Key != Player)
                        if (MathHelper.distanceFromPointToPoint(
                            Player.Location, reciever.Key.Location) <= 500)
                            reciever.Key.recieveInteraction(Player);
                    break;
                case Controls.ActionType.NextWeapon:
                    Player.switchWeapon(1);
                    break;
                case Controls.ActionType.PreviousWeapon:
                    Player.switchWeapon(-1);
                    break;
            }
        }

		/// <summary>
		/// What do do on a mouse movement.
		/// </summary>
		/// <param name="e">View changed how?</param>
        void _controls_ViewChanged(RotationEventArgs e)
        {
            if (Player.State != EntityState.DEAD)
            {
                Player.TurnTo(MathHelper.directionFromPointToPoint(
                    new Vector2(
                        Player.X + Camera.Instance.XOffset,
                        Player.Y + Camera.Instance.YOffset),
                    e.Location));
                if (Player.Rotation.ToString() == "NaN")
                    Player.TurnTo(90);
                Program.Client.send(ServerCommandTypes.ORIENTATION, (Player.Rotation / Math.PI * 180).ToString());
            }
        }

		/// <summary>
		/// What to do on a keyboard movement.
		/// </summary>
		/// <param name="e">What type of movement.</param>
		void _controls_MovementOccured( MovementEventArgs e ) {
            if (Player.State != EntityState.DEAD)
            {
                switch (e.Direction)
                {
                    case Controls.MovementDirection.UP:
                        Program.Client.send(ServerCommandTypes.MOVEMENT, "F");
                        Player.MoveForward(e.TimeElapsed);
                        break;
                    case Controls.MovementDirection.DOWN:
                        Program.Client.send(ServerCommandTypes.MOVEMENT, "B");
                        Player.MoveBackward(e.TimeElapsed);
                        break;
                    case Controls.MovementDirection.LEFT:
                        Program.Client.send(ServerCommandTypes.MOVEMENT, "L");
                        Player.MoveLeft(e.TimeElapsed);
                        break;
                    case Controls.MovementDirection.RIGHT:
                        Program.Client.send(ServerCommandTypes.MOVEMENT, "R");
                        Player.MoveRight(e.TimeElapsed);
                        break;
                    case Controls.MovementDirection.NONE:
                        Player.Rest();
                        break;
                }
            }
        }
		

		/// <summary>
		/// What to do on keypress events other than movement, such as fire buttons.
		/// </summary>
		/// <param name="e"></param>
        void KeyPressed(KeyPressEventArgs e)
        {
            //
        }

		/// <summary>
		/// Starts looking for input.
		/// </summary>
		/// <param name="time">The Game Time</param>
        public void handeInput(GameTime time)
        {
            _controls.handleInput(time);
        }
    }
}
