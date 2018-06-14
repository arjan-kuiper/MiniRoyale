using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MINI_ROYALE
{
    class Player
    {
        public Vector2 pos;
        private bool alive;
        private byte hp;
        private byte armor;
        private Inventory inventory;
        private byte currentItem;
        private float orientation;
        private int team;
        public Rectangle boundingBox;


        public Player()
        {
            this.pos = new Vector2(0, 0);
            inventory = new Inventory(this);
            this.boundingBox = new Rectangle(0, 0, 16, 16);
        }

        public int collidesWithSurrounding()
        {
            var xCoord = (int)this.pos.X / 16;
            var yCoord = (int)this.pos.Y / 16;
            //System.Diagnostics.Debug.WriteLine("X = {0}. Y = {1}", xCoord, yCoord);
            //Half block check met modulo
            for (int y = yCoord - 1; y < yCoord + 1; y++)
            {
                for (int x = xCoord - 1; x <= xCoord+1; x++)
                {
                    //if (new Rectangle(pos.X, pos.Y, 16, 16)
                    //If player left side > block to left right side && collision is true CONFLICT
                    if (boundingBox.Intersects(new Rectangle(32, 32, 8, 8)))
                    {
                        System.Diagnostics.Debug.WriteLine("COLLISION DETECTED");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("NO COLLISION DETECTED");
                    }
                }
            }
            return 1;
        }

        public void draw(SpriteBatch spriteBatch, Game game)
        {
            spriteBatch.Begin();
            Viewport viewport = game.GraphicsDevice.Viewport;
            Texture2D playerImg = game.Content.Load<Texture2D>("player");
            Vector2 playerOrigin = new Vector2(playerImg.Width / 2f, playerImg.Height / 2f);
            MouseState current_mouse = Mouse.GetState();
            Vector2 dPos = new Vector2(viewport.Width / 2f, viewport.Height / 2f) - current_mouse.Position.ToVector2();
            float rotation = (float)Math.Atan2(dPos.Y, dPos.X) + (float)Math.PI;
            // System.Diagnostics.Debug.WriteLine("{0} {1}", current_mouse.X, current_mouse.Y);
            collidesWithSurrounding(); // debugging only
            this.orientation = rotation;
            spriteBatch.Draw(playerImg, new Vector2(viewport.Width / 2f, viewport.Height / 2f), null, Color.White, rotation, playerOrigin, .5f, SpriteEffects.None, 0f);
            spriteBatch.End();
            boundingBox.X = (int)pos.X;
            boundingBox.Y = (int)pos.Y;
        }

        public void Move(Vector2 vec)
        {
            this.pos.X += vec.X;
            pos.Y += vec.Y;
            //System.Diagnostics.Debug.WriteLine("{0} {1}", pos.x, pos.y);
        }
        

        public int pickup(Item item)
        {
            inventory.AddItemToInv(item);
            return 0;
        }

        public Item dropItem(int slot)
        {
            // TODO
            
            return null;
        }

        public byte takeDamage(byte damage)
        {
            // TODO
            return 0;
        }

        public byte increaseHealth(byte amount)
        {
            // TODO
            return 0;
        }

        public byte increaseArmor(byte amount)
        {
            // TODO
            return 0;
        }

        public Throwable throwItem(byte slot)
        {
            // TODO
            return null;
        }

        public Item dropItem(byte slot)
        {
            // TODO
            return null;
        }

        public void update()
        {
            // TODO
        }


        public void shoot()
        {
            System.Diagnostics.Debug.WriteLine("Shoot" + orientation);
        }

        private bool checkCollision(Vector2 pos)
        {
            // TODO
            return true;
        }
    }
}
