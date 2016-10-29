using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GameAndStuff;
using HuntTheWumpus.WorldAndStuff;

namespace HuntTheWumpus.GraphicsAndStuff
{
    public enum CameraType { CENTERED, FREE }
    class Camera
    {
        public float XOffset { get; private set; }
        public float YOffset { get; private set; }

        private CameraType _type;
        public CameraType Type 
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    if (LockedOn != null &&
                        _type == CameraType.CENTERED)
                        setCameraCenter(LockedOn.X, LockedOn.Y);
                }
            }
        }
        public PlayerEntity LockedOn { get; private set; }
        public static Camera Instance { get; private set; }
        public float BorderWidth { get; set; }

        static Camera()
        {
            Instance = new Camera();
            Instance.SetCameraCorner(0, 0);
            Instance.Type = CameraType.CENTERED;
            Instance.LockedOn = null;
			Instance.BorderWidth = HTW.Instance.ScreenWidth * .25f;
        }

        private Camera()
        {
            XOffset = 0;
            YOffset = 0;
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            if (LockedOn == null)
                throw new Exception("Camera not Locked on Anything");

            if (Type == CameraType.CENTERED)
                setCameraCenter(LockedOn.X, LockedOn.Y);
            else
            {
                if (HTW.debugging)
                {
                    if (isEntityAtBorder() != BorderType.NONE)
                        Program.Client.send(ServerCommandTypes.DEBUG, isEntityAtBorder().ToString());
                }
                switch (isEntityAtBorder())
                {
                    case BorderType.TOP:
                    case BorderType.BOTTOM:
                        MoveCamera(
                                LockedOn.Speed * time.ElapsedGameTime.Milliseconds,
                                LockedOn.Rotation);
                        break;
                    case BorderType.LEFT:
                    case BorderType.RIGHT:
                        MoveCamera(
                                LockedOn.Speed * time.ElapsedGameTime.Milliseconds,
                                LockedOn.Rotation);
                        break;
                }

            }
                    
        }

        public void LockOn(PlayerEntity ent)
        {
            LockedOn = ent;
            setCameraCenter(ent.X, ent.Y);
        }

        private enum BorderType { TOP, BOTTOM, LEFT, RIGHT, NONE };
        private BorderType isEntityAtBorder()
        {
            if (LockedOn.X + XOffset < BorderWidth)
                return BorderType.LEFT;
            if (LockedOn.Y + YOffset < BorderWidth)
                return BorderType.TOP;
            if (LockedOn.X + XOffset > HTW.Instance.ScreenWidth - BorderWidth)
                return BorderType.RIGHT;
            if (LockedOn.Y + YOffset > HTW.Instance.ScreenHeight - BorderWidth)
                return BorderType.BOTTOM;
            return BorderType.NONE;
        }
        private void SetCameraCorner(float x, float y)
        {
            XOffset = -x;
            YOffset = -y;
        }
        private void setCameraCenter(float x, float y)
        {
            XOffset = HTW.Instance.ScreenWidth / 2 - x;
            YOffset = HTW.Instance.ScreenHeight / 2 - y;
        }
        private void MoveCamera(float distance, float direction)
        {
            switch (LockedOn.Direction)
            {
                case PlayerEntity.MovementDirection.FORWARD:
                    XOffset -= distance * (float)Math.Cos(direction);
                    YOffset += distance * (float)Math.Sin(direction);
                    break;
                case PlayerEntity.MovementDirection.BACKWARD:
                    XOffset -= distance * (float)Math.Cos(direction + Math.PI);
                    YOffset += distance * (float)Math.Sin(direction + Math.PI);
                    break;
                case PlayerEntity.MovementDirection.LEFT:
                    XOffset -= distance * (float)Math.Cos(direction + Math.PI / 2);
                    YOffset += distance * (float)Math.Sin(direction + Math.PI / 2);
                    break;
                case PlayerEntity.MovementDirection.RIGHT:
                    XOffset -= distance * (float)Math.Cos(direction - Math.PI / 2);
                    YOffset += distance * (float)Math.Sin(direction - Math.PI / 2);
                    break;
            }

        }
    }
}
