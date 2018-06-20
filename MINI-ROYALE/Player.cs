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
        public Rectangle boundingBox;
        public byte currentItem;

        private byte hp;
        private Inventory inventory;
        private float orientation;

        private GameState state;


        public Player(GameState state) {
            this.state = state;
            this.pos = new Vector2(32,32);
            inventory = new Inventory(this);
            this.boundingBox = new Rectangle(32, 32, 16, 16);
        }

        public int collidesWithSurrounding() {
            var xCoord = (int)this.pos.X / 16;
            var yCoord = (int)this.pos.Y / 16;
            //System.Diagnostics.Debug.WriteLine("X = {0}. Y = {1}", xCoord, yCoord);
            //Half block check met modulo
            for (int y = yCoord - 1; y < yCoord + 1; y++)
            { 
                for (int x = xCoord - 1; x <= xCoord+1; x++)
                {
                   
                    if (TileMap.instance.mapLoaded == true)
                    {

                        bool tileHasCollision = TileMap.instance.getTileOnLoc(x, y).hasCollision;
                        if (tileHasCollision == true)
                        {
                            //System.Diagnostics.Debug.WriteLine("A tile was loaded");
                            if (boundingBox.Intersects(new Rectangle((int)TileMap.instance.getTileOnLoc(x, y).position.X, (int)TileMap.instance.getTileOnLoc(x, y).position.Y, 16,16)))
                            {
                                //System.Diagnostics.Debug.WriteLine("Collision detected: " + position);
                            }
                            System.Diagnostics.Debug.WriteLine("Collision detected ");

                        }
                    }
                }
            }
            return 1;
        }

        public void draw(SpriteBatch spriteBatch, Game game) {
            spriteBatch.Begin();
            Viewport viewport = game.GraphicsDevice.Viewport;

            Item item = getItemInSlot(currentItem - 1);
            string texture = "";
            if (item is Weapon)
            {
                texture = "player-weapon";
            }
            else
            {
                texture = "player-normal";
            }

            Texture2D playerImg = game.Content.Load<Texture2D>(texture);


            Vector2 playerOrigin = new Vector2(playerImg.Width / 2f, playerImg.Height / 2f);
            MouseState current_mouse = Mouse.GetState();
            Vector2 dPos = new Vector2(viewport.Width / 2f, viewport.Height / 2f) - current_mouse.Position.ToVector2();
            float rotation = (float)Math.Atan2(dPos.Y, dPos.X) + (float)Math.PI;
            // System.Diagnostics.Debug.WriteLine("{0} {1}", current_mouse.X, current_mouse.Y);
            //collidesWithSurrounding(); // debugging only
            this.orientation = rotation;
            spriteBatch.Draw(playerImg, new Vector2(viewport.Width / 2f, viewport.Height / 2f), null, Color.White, rotation, playerOrigin, .5f, SpriteEffects.None, 0f);
            spriteBatch.End();
            boundingBox.X = (int)pos.X;
            boundingBox.Y = (int)pos.Y;
        }

        public void Move(Vector2 vec) {
            this.pos.X += vec.X;
            pos.Y += vec.Y;
            this.boundingBox.X = (int)pos.X;
            this.boundingBox.Y = (int)pos.Y;
            //System.Diagnostics.Debug.WriteLine("{0} {1}", pos.x, pos.y);
        }
        

        public bool pickup(Item item) {
            if(inventory.GetSizeOfInv() == 0) { currentItem = 1; }
            return inventory.AddItemToInv(item);
        }

        public string getCurrentItemName() {
            return inventory.GetItemName(currentItem - 1);
        }

        // Voor het removen van een item heb de functie naar een bool veranderd van een Item. 
        // Als dit niet goed is graag aangeven
        public bool dropItem(Item item) {
            return inventory.RemoveItemFromInv(item);
        }

        public Item getItemInSlot(int slot) {
            return inventory.GetItemInSlot(slot);
        }

        public byte takeDamage(byte damage) {
            // TODO
            return 0;
        }

        public byte increaseHealth(byte amount) {
            this.hp += amount;
            if(hp > 100)
            {
                hp = 100;
            }
            return hp;
        }

        public byte increaseArmor(byte amount) {
            // TODO
            return 0;
        }

        public Throwable throwItem(int slot) {
            // TODO
            return null;
        }

        public void update() {
            // TODO
        }


        public void Shoot(float orientation)
        {
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            MouseState current_mouse = Mouse.GetState();
            Vector2 bulletTarget = new Vector2(viewport.Width / 2f, viewport.Height / 2f) - current_mouse.Position.ToVector2();
            Vector2 spawnPosition;

            spawnPosition.X = pos.X;
            spawnPosition.Y = pos.Y;
            
            Bullet bullet = new Bullet(spawnPosition, -bulletTarget, orientation);
            state.spawnedBullets.Add(bullet);
        }

        public void ThrowExplosive() {
            Vector2 spawnPosition;
            Vector2 bulletTarget;

            bulletTarget.X = Mouse.GetState().X;
            bulletTarget.Y = Mouse.GetState().Y;

            spawnPosition.X = pos.X;
            spawnPosition.Y = pos.Y;

            Bullet bullet = new Bullet(spawnPosition, bulletTarget, orientation);
            state.spawnedBullets.Add(bullet);
        }

        private bool checkCollision(Vector2 pos) {
            // TODO
            return true;
        }

        public float getOrientation()
        {
            return orientation;
        }
    }
}
