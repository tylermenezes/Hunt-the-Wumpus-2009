using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.GraphicsAndStuff; 
namespace HuntTheWumpus.WorldAndStuff.Weapons
{
    public interface IPlayerWeapon
    {
        Animation WeaponWalkAnimation { get; }
        Animation WeaponStandAnimation { get; }
    }
}
