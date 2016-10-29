using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace HuntTheWumpus.WorldAndStuff
{
    public class BlockEntity : Entity
    {
        public BlockEntity(string id) :
            base(EntityType.BLOCK, id, 
            new Vector2(32,30)) // set size here (depends on image but ~30x30)
        {

        }
        public override void recieveAttack(DynamicEntity sender, DynamicEntity.Weapon weapon)
        {
            //
        }

        public override void recieveInteraction(PlayerEntity sender)
        {
            // Do Nothing
        }
        
    }
}
