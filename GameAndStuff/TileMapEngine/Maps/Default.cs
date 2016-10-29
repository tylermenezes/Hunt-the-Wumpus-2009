using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuntTheWumpus.WorldAndStuff;

namespace HuntTheWumpus.GameAndStuff.TileMapEngine.Maps {
	class Default : Map {
		protected override void init() {
			TilesHigh = 2;
			TilesWide = 2;
			add(new int[] { (int)EntityType.WAMPUS1 & (int)EntityType.WEMPUS2,	(int)EntityType.WAMPUS1 });
			add(new int[] { (int)EntityType.WEMPUS2,							(int)EntityType.WAMPUS1 & (int)EntityType.WEMPUS2 });
		}
	}
}
