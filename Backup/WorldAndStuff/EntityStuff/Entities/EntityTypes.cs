using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.WorldAndStuff
{

    public enum EntityType // Powers of 2 for TileMapEngine.
    { 
        PLAYER			= 1,
 
        // Enemies
        WAMPUS1			= 2, 
        WEMPUS2			= 3, 
        WIMPUS3			= 4, 
        WOMPUS4			= 16, 
        WUMPUS5			= 32, 
        WYMPUS6			= 54, 

        // Weapons
        PISTOLBULLET	= 128,
        SHOTGUNBULLET	= 256,
        MISSLE			= 512,
		ACID			= 1024,

		// Bullets
		ROCKET			= 2048,

        // Terrain Objects
        BLOCK			= 4096 
    }

    public enum EntityClass
    {
        PLAYER,
        WUMPUS,
        WEAPON,
        TERRAIN
    }


}
