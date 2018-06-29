using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{

    public enum type { IRREGULAR, CONTROLLED, PREFORMED }

    class Explosive : Item
    {
        private int explosionTime { get; set; }
        private int blastRadius { get; set; }
        private Player play { get; }

        private type bomb;

        public Explosive(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {
            use();
        }


        /// <summary>
        /// calls the armExplosive funtion to drop a bomb
        /// </summary>
        /// <returns>true</returns>
        public override bool use()
        {
            armExplosive();
            return true;
        }
        
        /// <summary>
        /// gives and sets the dmg and radius of the bomb
        /// and calls the function to drop it and make it go off.
        /// </summary>
        public void armExplosive()
        {
            for (int i = 0; i == 500; i++)
            {
                for (int x = 0; x == getExplosionTime(); x++)
                {
                    if (bomb == type.CONTROLLED)
                    {
                        getBlastRadius();
                        play.ThrowExplosive();
                    }
                    else if(bomb == type.IRREGULAR)
                    {
                        getBlastRadius();
                        play.ThrowExplosive();
                    }
                    else if(bomb == type.PREFORMED)
                    {
                        getBlastRadius();
                        play.ThrowExplosive();
                    }
                    x = 0;
                }
                i = 0;
            }
        }

        /// <summary>
        /// return the blast radius
        /// </summary>
        /// <returns> blast radius</returns>
        public int getBlastRadius()
        {
            return blastRadius;
        }

        /// <summary>
        /// return the explosion tim
        /// </summary>
        /// <returns>explosion time</returns>
        public int getExplosionTime()
        {
            return explosionTime;
        }
    }
}
