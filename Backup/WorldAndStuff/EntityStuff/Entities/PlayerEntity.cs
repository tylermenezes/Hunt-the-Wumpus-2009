using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GameAndStuff;
namespace HuntTheWumpus.WorldAndStuff
{
    public enum PlayerEmployer { COMPANY_ONE, COMPANY_TWO }
    public class PlayerEntity : DynamicEntity
    {
        #region Fields
        private bool _blockedForward = false; // is colliding with something
        private bool _blockedBackward = false;
        private bool _blockedLeft = false;
        private bool _blockedRight = false;
        private int _weaponIndex;
        #endregion

        public enum MovementDirection { FORWARD, BACKWARD, LEFT, RIGHT, NONE }
        public MovementDirection Direction;

        #region Events

        public delegate void PlayerMovedEvent(PlayerMovedEventArgs e);
        public event PlayerMovedEvent PlayerMovedEventHandler;

        public delegate void PlayerGainedPointsEventHandler(int newAmount);
        public event PlayerGainedPointsEventHandler PlayerGainedPointsEvent;

        public delegate void PlayerChangedWeaponEventHandler();
        public event PlayerChangedWeaponEventHandler PlayerChangedWeaponEvent;

        #endregion

        #region Properties
        private int _points;
        public int Points
        {
            get { return _points; }
            private set
            {
                _points = value;
                PlayerGainedPointsEvent(_points);
            }
        }

		public int level = 0;

        public List<Weapon> Weapons { get; private set; }
        #endregion

        public PlayerEmployer Employer { get; private set; }

        public PlayerEntity(Profile profile) :
            base(EntityType.PLAYER, profile.ID,
            new Microsoft.Xna.Framework.Vector2(33), 
            200, 3f/16)
        {
            Employer = profile.Employer;
            _points = profile.Points;
            
            Weapons = new List<Weapon>();
			Weapons.Add(GameAndStuff.Profile.getWeaponFromInventoryItem(InventoryItem.PISTOL, this));
			//foreach (GameAndStuff.InventoryItem i in profile.Inventory)
			//    Weapons.Add(GameAndStuff.Profile.
			//        getWeaponFromInventoryItem(i, this));

            ActiveWeapon = Weapons[0];
            _weaponIndex = 0;

            Direction = MovementDirection.NONE;
            // TODO : add weapon
        }

        public override void think(Microsoft.Xna.Framework.GameTime time)
        {
            // Do Nothing -- thinking done by user
        }
		#region Movements
		public void MoveForward(
            Microsoft.Xna.Framework.GameTime time)
        {
            if (!_blockedForward)
            {
                var oldLocation = this.Location;
                base.MoveForward(Speed * time.ElapsedGameTime.Milliseconds);
                if (PlayerMovedEventHandler != null)
                    PlayerMovedEventHandler(
                        new PlayerMovedEventArgs(this, oldLocation, time));
                Direction = MovementDirection.FORWARD;
            }

            if (this.CollisionTop)
                _blockedForward = true;
            else
                _blockedForward = false;
        }

        public void MoveBackward(
            Microsoft.Xna.Framework.GameTime time)
        {
            if (!_blockedBackward)
            {
                var oldLocation = this.Location;
                base.moveBackward(Speed * time.ElapsedGameTime.Milliseconds);
                if (PlayerMovedEventHandler != null)
                    PlayerMovedEventHandler(
                        new PlayerMovedEventArgs(this, oldLocation, time));
                Direction = MovementDirection.BACKWARD;
            }
            if (this.CollisionBottom)
                _blockedBackward = true;
            else
                _blockedBackward = false;
        }

		public void MoveLeft(
			Microsoft.Xna.Framework.GameTime time ) {
			if (!_blockedLeft) {
				var oldLocation = this.Location;
				base.strifeLeft(Speed * time.ElapsedGameTime.Milliseconds);
				if (PlayerMovedEventHandler != null)
					PlayerMovedEventHandler(
                        new PlayerMovedEventArgs(this, oldLocation, time));
                this.Direction = MovementDirection.LEFT;
            }
            if (this.CollisionLeft)
                _blockedLeft = true;
            else
                _blockedLeft = false;
		}

		public void MoveRight(Microsoft.Xna.Framework.GameTime time ) {
			if (!_blockedRight) {
				var oldLocation = this.Location;
				base.strifeRight(Speed * time.ElapsedGameTime.Milliseconds);
				if (PlayerMovedEventHandler != null)
					PlayerMovedEventHandler(
						new PlayerMovedEventArgs(this, oldLocation, time));
                this.Direction = MovementDirection.RIGHT;
			}

            if (this.CollisionRight)
                _blockedRight = true;
            else
                _blockedRight = false;
		}

		#endregion

		public void ChangeWeapon(Weapon newWeapon)
        {
            if (Weapons.Contains(newWeapon))
            {
                ActiveWeapon = newWeapon;
                if (PlayerChangedWeaponEvent != null)
                    PlayerChangedWeaponEvent();
            }
        }

        public void switchWeapon(int direction)
        {
            _weaponIndex += (direction > 0) ? 1 : -1;
            if (_weaponIndex < 0)
                _weaponIndex = Weapons.Count-1;
            if (_weaponIndex >= Weapons.Count)
                _weaponIndex = 0;
            ActiveWeapon = Weapons[_weaponIndex];
            if (PlayerChangedWeaponEvent != null)
                PlayerChangedWeaponEvent();
        }

        public override void obstacleCollide(Entity[] entities)
        {
			this.Rest();
            // Do nothing here (but perhaps a different entity may act differently)
        }

        public void TurnTo(float degrees)
        {
            turnTo(degrees);
        }

        public void Rest()
        {
            Direction = MovementDirection.NONE;
            rest();
        }
        public override void recieveInteraction(PlayerEntity sender)
        {
            throw new NotImplementedException();
        }

        internal void raisePoints(int p)
        {
            Points += p;
			if (Points > 10 && !hasWeapon(GameAndStuff.Profile.getWeaponFromInventoryItem(InventoryItem.SHOTGUN, this))) {
				HTW.Instance.CurrentGameState._overlay.ShowMessage(300, "You found the " + Random.RandomWeaponName("Shotgun"));
				Weapons.Add(GameAndStuff.Profile.getWeaponFromInventoryItem(InventoryItem.SHOTGUN, this));
			}
			if (Points > 100 && !hasWeapon(GameAndStuff.Profile.getWeaponFromInventoryItem(InventoryItem.ROCKETLAUNCHER, this))) {
				HTW.Instance.CurrentGameState._overlay.ShowMessage(300, "You found the " + Random.RandomWeaponName("Shotgun"));
				Weapons.Add(GameAndStuff.Profile.getWeaponFromInventoryItem(InventoryItem.ROCKETLAUNCHER, this));
			}
            Console.WriteLine("Points: " + Points);
        }

        public override void die()
        {
            base.die();
        }

        public void AttackEntity(Microsoft.Xna.Framework.GameTime time)
        {
            Attack(time);
        }

		public bool hasWeapon( Weapon weapon ) {
			foreach (Weapon iWeapon in Weapons) {
				if (weapon.GetType() == iWeapon.GetType() || weapon == iWeapon)
					return true;
			}
			return false;
		}
    }

    public class PlayerMovedEventArgs : EventArgs
    {
        public Microsoft.Xna.Framework.GameTime TimeElapsed { get; private set; }
        public PlayerEntity Player { get; private set; }
        public Microsoft.Xna.Framework.Vector2 OldLocation { get; private set; }
        public float Distance { get; private set; }

        public PlayerMovedEventArgs(PlayerEntity player,
            Microsoft.Xna.Framework.Vector2 oldLocation, 
            Microsoft.Xna.Framework.GameTime time)
        {
            Player = player;
            OldLocation = oldLocation;
            TimeElapsed = time;
            Distance = MathHelper.distanceFromPointToPoint(OldLocation, player.Location);
        }
    }
}
