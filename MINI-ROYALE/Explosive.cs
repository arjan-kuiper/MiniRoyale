using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Explosive : Item
    {
        private enum type { IRREGULAR, CONTROLLED, PREFORMED }
        private int explosionTime { get; set; }
        private int blastRadius { get; set; }

        public Explosive(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {

        }
    }
}
