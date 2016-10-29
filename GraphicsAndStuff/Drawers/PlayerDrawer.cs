using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuntTheWumpus.WorldAndStuff;
 using Microsoft.Xna.Framework;
 using Microsoft.Xna.Framework.Audio;
 using Microsoft.Xna.Framework.Content;
 using Microsoft.Xna.Framework.GamerServices;
 using Microsoft.Xna.Framework.Graphics;
 using Microsoft.Xna.Framework.Input;
 using Microsoft.Xna.Framework.Net;
 using Microsoft.Xna.Framework.Storage;

namespace HuntTheWumpus.GraphicsAndStuff.Drawers
{
    public class PlayerDrawer : EntityDrawer
    {
        public PlayerDrawer(WorldAndStuff.PlayerEntity player) :
            base(player)
        { 
            // Don't Put anything here!
            // The drawing code goes in the EntityDrawer class, I moved it there
            player.PlayerChangedWeaponEvent += new PlayerEntity.PlayerChangedWeaponEventHandler(player_PlayerChangedWeaponEvent);
        }

        void player_PlayerChangedWeaponEvent()
        {
            var weapon = (HuntTheWumpus.WorldAndStuff.Weapons.IPlayerWeapon)
                ((DynamicEntity)Entity).ActiveWeapon;

            base.Animations.Remove(EntityState.MOVING);
            base.Animations.Remove(EntityState.RESTING);

            base.Animations.Add(EntityState.RESTING,
                weapon.WeaponStandAnimation);
            base.Animations.Add(EntityState.MOVING,
                weapon.WeaponWalkAnimation);
        }

        public override void SetupDrawer()
        {
            var weapon = (HuntTheWumpus.WorldAndStuff.Weapons.IPlayerWeapon)
                ((DynamicEntity)Entity).ActiveWeapon;
            base.Animations.Add(EntityState.RESTING,
                weapon.WeaponStandAnimation);
            base.Animations.Add(EntityState.MOVING,
                weapon.WeaponWalkAnimation);
            base.Animations.Add(EntityState.DEAD,
                new Animation(
                    (Texture2D)CentralResourceRepository.Resources["deadplayer"],
                    1,
                    0.1));
        }
    }
}
