using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Weapon : Item, Throwable
    {
        private float damage { get; set; }
        private float shotCoolDown { get; set; }
        private enum FiringType { SEMI, FULL }
        private int bulletInClip { get; set; }
        private int maxBulletInClip { get; set; }
        private float spread { get; set; }
        private float recoil { get; set; }

        public Weapon(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {

        }
        private int Reload()
        {
            if (bulletInClip == 0)
            {
                bulletInClip = maxBulletInClip;
            }
            return bulletInClip;
        }

        private int Fire()
        {
            // something like this?
            // for loop for shots maybe 2 fast 
            for (float i = 0; i < 999; i++)
            {
                if (i == shotCoolDown)
                {
                    i = 0;
                }
                // something for full and semi?? override or??
                if (bulletInClip > 0)
                {
                    // SHOOT
                    // shoot bullet don't know yet how

                }
                else
                {
                    // reload and can't shoot
                    Reload();
                    i = 0;
                }
            }
            return 0;
        }

        public void ThrowItem()
        {
            // throw function from trhow class here.
        }

        public bool Use()
        {
            Fire();
            return true;
        }
    }
}
