using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class HealingItem : Item
    {
        private float healingCount { get; set; }
        private int useTime { get; set; }

        public HealingItem(String itemName, Texture2D sprite, Vector2 pos, float healingCount) : base(itemName, sprite, pos)
        {
            this.healingCount = healingCount;
            use();
        }

        /// <summary>
        /// calls the interrupt use so you can't heal and keep shooting
        /// </summary>
        /// <returns>true</returns>
        public override bool use()
        {
            interruptUse();
            return true;
        }

        /// <summary>
        /// timer to wait before the player is healed.
        /// </summary>
        public void interruptUse()
        {
            for(int i = 0; i == useTime; i++)
            {
                getHealingCount();
                i = 0;
            }
        }

        /// <summary>
        /// returns the healing count (the amount it can heal)
        /// </summary>
        /// <returns>healingCount</returns>
        public override float getHealingCount()
        {
            return healingCount;
        }
    }
}
