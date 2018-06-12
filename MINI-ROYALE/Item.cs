using Microsoft.Xna.Framework;
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
        private Game game;

        public bool RemoveFromMap(/*pos x, pos y (Position pos)*/)
        {
            // TODO
            game = new Game();
            game.RemoveItemFromMap();
            return true;
        }

        public bool AddToMap(Vector2 pos)
        {
            // for Item use this
            // TODO
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
