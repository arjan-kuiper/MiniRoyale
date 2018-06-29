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
        public Vector2 pos;

        public Item(String itemName, Texture2D sprite, Vector2 pos)
        {
            this.itemName = itemName;
            this.sprite = sprite;
            this.pos = pos;
        }

        /// <summary>
        /// gets the name of the item
        /// </summary>
        /// <returns>itemName</returns>
        public string getName()
        {
            return this.itemName;
        }

        /// <summary>
        /// removes the item from the map
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>true because called</returns>
        public bool removeItemFromMap(Vector2 pos)
        {
            GameState state = (GameState)Game.instance.getState();
            state.RemoveItemFromMap(pos);
            return true;
        }

        /// <summary>
        /// adds item to the map
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="item"></param>
        /// <returns>true because called</returns>
        public bool addItemToMap(Vector2 pos, Item item)
        {
            // for Item use this
            GameState state = (GameState)Game.instance.getState();
            state.AddItemToMap(item);
            return true;
        }

        /// <summary>
        /// this fucntion is called in other classes
        /// </summary>
        /// <returns>returns true</returns>
        public virtual bool use()
        {
            return true;
        }

        /// <summary>
        /// this fucntion is called in other classes
        /// </summary>
        /// <returns></returns>
        public virtual float getHealingCount()
        {
            return 0;
        }

        /// <summary>
        /// draws the items on the map
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: TileMap.instance.cam.GetViewMatrix());
            
            // Need rectangle stuff so we can resize it
            spriteBatch.Draw(sprite, new Rectangle((int)pos.X, (int)pos.Y, 16, 16), new Rectangle(0, 0, sprite.Width, sprite.Height), Color.White);
            spriteBatch.End();
        }
    }
}
