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
        private Vector2 pos { get; set; }

        public Item(String itemName, Texture2D sprite, Vector2 pos)
        {
            this.itemName = itemName;
            this.sprite = sprite;
            this.pos = pos;
        }

        public bool RemoveItemFromMap(Vector2 pos)
        {
            // TODO
            Game.instance.RemoveItemFromMap(pos);
            return true;
        }

        public bool AddItemToMap(Vector2 pos, Item item)
        {
            // for Item use this
            // TODO
            Game.instance.AddItemToMap(pos, item);
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

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: TileMap.instance.cam.GetViewMatrix());
            
            // Need rectangle stuff so we can resize it
            spriteBatch.Draw(sprite, new Rectangle((int)pos.X, (int)pos.Y, 16, 16), new Rectangle(0, 0, sprite.Width, sprite.Height), Color.White);
            spriteBatch.End();
        }
    }
}
