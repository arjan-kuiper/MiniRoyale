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

        public bool removeFromMap(/*pos x, pos y (Position pos)*/)
        {
            // TODO
            game = new Game();
            game.removeItem();
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
