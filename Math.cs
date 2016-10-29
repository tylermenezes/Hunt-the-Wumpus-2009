using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace HuntTheWumpus
{
    public static class MathHelper
    {
        public static float directionFromPointToPoint(Vector2 point1, Vector2 point2)
        {
            var slope = (point1.Y - point2.Y) / (point2.X - point1.X);
            var angle = (float)
                ((point2.X < point1.X) ? 
                Math.PI + Math.Atan(slope) : 
                Math.Atan(slope));

            // Console.Write("Slope: " + slope + "|| Angle: " + angle/Math.PI*180);
            return normalizeAngle(angle);
        }

        public static float distanceFromPointToPoint(Vector2 point1, Vector2 point2)
        {
            return
                (float)Math.Pow(
                   Math.Pow(point1.X - point2.X, 2) +
                   Math.Pow(point1.Y - point2.Y, 2),
                   .5);
        }

        public static float normalizeAngle(float angle)
        {
            // bring within -2p to 2pi range
            if (angle > Math.PI * 2)
                angle -= (float)Math.PI * 2;
            else if (angle < -Math.PI * 2)
                angle += (float)Math.PI * 2;

            // convert to negative angle if > 180
            if (angle > Math.PI)
                angle -= (float)Math.PI * 2;

            // convert to a positive angle if < -180
            if (angle < -Math.PI)
                angle += (float)Math.PI * 2;

            return (float)angle;
        }
                    
    }
}
