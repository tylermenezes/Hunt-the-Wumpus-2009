using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus {
	class Random {
		public static System.Random fish = new System.Random();
		private static string[] randomLoadingStrings = new string[]{
			"Multiplying some numbers",
			"Cracking MD5 hashes",
			"Breaking RSA",
			"Calculating the answer to life",
			"Doing something important",
			"Hunting the Wumpus",
			"Surfing Reddit",
			"Composing music",
			"Playing with the light switch",
			"Accellerating to 88mph",
			"Factoring large primes",
			"Adding lies to the cake",
			"Restraining myself",
			"Powering up my \"Laser\"",
			"Writing the code",
			"Downloading weapons... lots of weapons",
			"Locking boxes",
			"Pretending to load",
			"Applying patch to Space-Time",
			"Charging batteriesw speeding up turbines",
			"Feeding the Wumpus",
			"Tightening Graphics on Level 3",
			"Adding more cowbell",
			"Burning the world",
			"Loading for teh win",
			"Boarding fail boat",
			"Venting radioactive gas",
			"Lovin' it",
			"Boiling Wympuses",
			"Spacing out",
			"Fixing an obscure bug",
			"Getting offended",
			"Going the distance",
			"Clicking Allow on UAC prompt",
			"Reinstalling Windows ME",
			"Laughing at the other games",
			"Kicking puppies",
			"Looking for teh kitteh",
			"Searching for whales",
			"Replenishing witty load message pool",
			"Attempting to start car",
			"Booting Ubuntu",
			"Pushing the button",
			"Picking random fish",
			"Getting ready",
			"Infesting cities",
			"Killing Zombies",
			"Rebuffering Z-Floor",
			"Trolling 4chan",
			"Negotiating world peace",
			"Adjusting radiation levels to current weather conditions",
			"Establishing mood lighting",
			"Preparing fatal error queue",
			"Reading C# tutorials",
			"Pirating music",
			"Using GOTOs",
			"Solving for x",
			"Dividing by 0",
			"Getting things done",
			"Testing math",
			"Readying final countdown"
		};
		/// <summary>
		/// Gets a "random" "action" for the "loading" "screen".
		/// </summary>
		public static string loadingString {
			get {
				return randomLoadingStrings[fish.Next(randomLoadingStrings.Length)];
			}
			set {
			}
		}

		private static string[] randomWeaponStarters = new string[]{
			"Icy",
			"Flaming",
			"Powerful",
			"Weak",
			"Awesome",
			"Kick-ass",
			"Winning",
			"Cool"
		};

		private static string[] randomWeaponAdjectives = new string[]{
			"Minor",
			"Mediocre",
			"Average",
			"Major",
			"Epic"
		};

		private static string[] randomWeaponEnd = new string[]{
			"Ice",
			"Heat",
			"Flames",
			"Fire",
			"Leetness",
			"Water",
			"Win",
			"Pwnage",
			"Proportions"
		};

		/// <summary>
		/// Returns a random weapon name in the form of Flaming [Armor] of Minor Ice
		/// </summary>
		/// <param name="baseName">The [*] part, i.e. Armor, Sword, Gun, etc</param>
		/// <returns></returns>
		public static string RandomWeaponName( string baseName ) {
			return randomWeaponStarters[fish.Next(randomWeaponStarters.Length)] + " "
					+ baseName + " of " + randomWeaponAdjectives[fish.Next(randomWeaponAdjectives.Length)]
					+ " " + randomWeaponEnd[fish.Next(randomWeaponEnd.Length)];
		}

		// TODO: Random map location generator
	}
}
