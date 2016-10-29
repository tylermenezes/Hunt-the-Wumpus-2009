using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.WorldAndStuff
{
	interface IWorldObject
	{
        string ID { get; }

        EntityType Type { get; }

        EntityState State { get; }
        void recieveInteraction(PlayerEntity sender);
        void recieveAttack(DynamicEntity sender, DynamicEntity.Weapon weapon);

        void destroy();
	}
}
