using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Weapon : Item
    {
        private float shotCoolDown { get; set; }
        private int bulletInClip { get; set; }
        private int maxBulletInClip { get; set; }
        public int spread;
        private Player play { get; }

        /// <summary>
        /// makes an weapon
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="sprite"></param>
        /// <param name="pos"></param>
        /// <param name="spread"></param>
        public Weapon(String itemName, Texture2D sprite, Vector2 pos, int spread) : base(itemName, sprite, pos)
        {

            use();
            this.spread = spread;
        }

        /// <summary>
        /// reloads the bullets in the clip with a cooldown
        /// </summary>
        /// <returns>bullets after reload</returns>
        private int reload()
        {
            // reset the bullets with a reload cooldown
            for(int i = 0; i == 500; i++)
            {
                bulletInClip = maxBulletInClip;
            }
            return bulletInClip;
        }

        /// <summary>
        /// shoots the bullet 
        /// </summary>
        /// <returns></returns>
        private int fire()
        {
            for (float i = 0; i < 999; i++)
            {
                if (i == shotCoolDown)
                {
                    i = 0;
                }
                if (bulletInClip > 0)
                {
                    //play.shoot(play.getOrientation());
                }
                else
                {
                    reload();
                }
            }
            return 0;
        }

        /// <summary>
        /// calls the fire function
        /// </summary>
        /// <returns>true when called</returns>
        public new bool use()
        {
            fire();
            return true;
        }
    }
}
