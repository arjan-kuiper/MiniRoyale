using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    abstract class Item
    {
        private string itemName;
        private Texture2D sprite;
        private Position position;

        public bool removeFromMap()
        {
            // TODO
            return true;
        }

        public bool addToMap(Item item, Position pos)
        {
            // TODO
            return true;
        }

        public bool use()
        {
            // TODO
            return true;
        }
    }
}
