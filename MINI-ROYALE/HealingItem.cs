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
        }

        public override bool Use()
        {
            return true;
        }

        public void InterruptUse()
        {
            int healthPlayer = 30;
            for(int i = 0; i == healingCount; i++)
            {
                healthPlayer += 50;
                i = 0;
            }
        }

        public override float getHealingCount()
        {
            return healingCount;
        }
    }
}
