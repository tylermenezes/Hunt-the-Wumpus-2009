using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.GameAndStuff.TileMapEngine {
	public abstract class Map {
		protected int[][] map;

		protected uint TilesWide;
		protected uint TilesHigh {
			get {
				return (uint)map.Length;
			}
			set {
				map = new int[value][];
			}
		}

		protected abstract void init();

		public Map() {
			init();
		}

		// Remember, as long as the values for the enums are powers of two, they're unique,
		// even when combined. You can use the binary AND operator (&) to properly combine them.

		/// <summary>
		/// Adds a row of tiles to the map (which is a jagged array).
		/// </summary>
		/// <param name="row">What lives here? You could do this by hand, the easier way
		/// is to use the format EntityType.WAMPUS1 & EntityType.WEMPUS2</param>
		protected void add( int[] row ) {
			// WAT?
			if (row.Length > TilesWide)
				throw new Exception("Row can't be wider than the map width!");

			// Make it the right width.
			if (row.Length < TilesWide)
				row = concatArrays(row, new int[TilesWide - row.Length]);

			// Append it.
			map = concatArrays(map, new int[][]{row});
		}

		private static int[] concatArrays( int[] arrayOne, int[] arrayTwo ) {
			int[] toReturn = new int[arrayOne.Length + arrayTwo.Length];
			int i = 0;
			foreach (int o in arrayOne) {
				toReturn[i] = o;
				i++;
			}
			foreach (int o in arrayTwo) {
				toReturn[i] = o;
				i++;
			}
			return toReturn;
		}

		private static int[][] concatArrays( int[][] arrayOne, int[][] arrayTwo ) {
			int[][] toReturn = new int[arrayOne.Length + arrayTwo.Length][];
			int i = 0;
			foreach (int[] o in arrayOne) {
				toReturn[i] = o;
				i++;
			}
			foreach (int[] o in arrayTwo) {
				toReturn[i] = o;
				i++;
			}
			return toReturn;
		}


	}
}
