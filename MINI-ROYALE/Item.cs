using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    abstract public class Item
    {
        private string itemName { get; set; }
        private Texture2D sprite { get; set; }
        private Position position { get; set; }
        private Game game { get; }

        public bool RemoveItemFromMap(Vector2 pos)
        {
            // TODO
            game.RemoveItemFromMap(pos);
            return true;
        }

        public bool AddItemToMap(Vector2 pos, Item item)
        {
            // for Item use this
            // TODO
            game.AddItemToMap(pos, item);
            return true;
        }

        public bool Use()
        {
            // TODO
            if(this is Weapon)
            {
                //Reference to player.
            }
            if(this is Armor)
            {

            }
            if(this is HealingItem)
            {

            }
            if(this is Explosive)
            {

            }
            return true;
        }
    }
}
