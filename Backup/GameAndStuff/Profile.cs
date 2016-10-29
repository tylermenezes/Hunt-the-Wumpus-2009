using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.WorldAndStuff;
namespace HuntTheWumpus.GameAndStuff
{
    public enum InventoryItem { PISTOL, SHOTGUN, ROCKETLAUNCHER }
    public class Profile
    {
        public int Points { get; set; }
        public List<InventoryItem> Inventory { get; private set; }
        public string ID { get; private set; }
        public PlayerEmployer Employer { get; private set; }
		public int Level = 0;

        public Profile(
            int points,
            string id,
            PlayerEmployer employer,
            List<InventoryItem> inventory)
        {
            Points = points;
            ID = id;
            Employer = employer;
            Inventory = inventory;
        }

        public static WorldAndStuff.DynamicEntity.Weapon
            getWeaponFromInventoryItem(InventoryItem i, PlayerEntity player)
        {
            switch (i)
            {
                case InventoryItem.PISTOL:
                    return new WorldAndStuff.Weapons.PistolWeapon(player);
                case InventoryItem.SHOTGUN:
                    return new WorldAndStuff.Weapons.ShotgunWeapon(player);
                case InventoryItem.ROCKETLAUNCHER:
                    return new WorldAndStuff.Weapons.RocketWeapon(player);

            }
            throw new Exception("Weapon Not Found");
        }

		public void addToInventory( InventoryItem item ) {
			Inventory.Add(item);
		}

		public bool inInventory( InventoryItem item ) {
			foreach (InventoryItem iItem in Inventory) {
				if (item.Equals(iItem))
					return true;
			}
			return false;
		}
    }
}
