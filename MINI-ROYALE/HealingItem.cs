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

        public HealingItem(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {

        }
    }
}
