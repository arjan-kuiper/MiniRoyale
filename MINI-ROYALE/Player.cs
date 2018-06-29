using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace MINI_ROYALE
{
    public class Player
    {
        public Vector2 pos;
        public Rectangle boundingBox;
        public byte currentItem;
        public bool alive;
        public byte hp { get; private set; } = 100;
        private byte armor;
        private Inventory inventory;
        private float orientation;
        public Randomizer rnd;
        private GameState state;

        public bool[] moveDirs = { true, true, true, true };

        public Player(GameState state)
        {
            this.pos = new Vector2(400,400);
            inventory = new Inventory(this);
            this.boundingBox = new Rectangle(32, 32, 16, 16);
            alive = true;
            this.state = state;
            if(Randomizer.instance == null)
            {
                rnd = new Randomizer();
            }
        }

        /// <summary>
        /// collision check with surrounding not with other bots or players.
        /// </summary>
        public void collidesWithSurrounding()
        {
            var xCoord = (int)Math.Floor((this.pos.X) / 16);
            var yCoord = (int)Math.Floor((this.pos.Y) / 16);
            //System.Diagnostics.Debug.WriteLine("X = {0}. Y = {1}", xCoord, yCoord);
            //Half block check met modulo
            if (TileMap.instance.mapLoaded)
            {
                //System.Diagnostics.Debug.WriteLine("X: {0}, Y: {1}", xCoord, yCoord);
                if( TileMap.instance.getTileOnLoc(xCoord, (yCoord - 1)).hasCollision)
                {
                    moveDirs[0] = false;

                }
                else
                {
                    moveDirs[0] = true;
                }
                if( TileMap.instance.getTileOnLoc(xCoord , (yCoord + 1)).hasCollision)
                {
                    moveDirs[2] = false;
                }
                else
                {
                    moveDirs[2] = true;
                }
                if( TileMap.instance.getTileOnLoc((xCoord - 1), yCoord).hasCollision)
                {
                    moveDirs[3] = false;
                }
                else
                {
                    moveDirs[3] = true;
                }
                if (TileMap.instance.getTileOnLoc((xCoord + 1), yCoord).hasCollision)
                {
                    moveDirs[1] = false;
                }
                else
                {
                    moveDirs[1] = true;
                }
            }
            
        }

        /// <summary>
        /// draws the player on the middle of the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="game"></param>
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
            boundingBox.X = (int)Math.Round(pos.X);
            boundingBox.Y = (int)Math.Round(pos.Y+16);
        }

        /// <summary>
        /// makes the player meve over the map
        /// </summary>
        /// <param name="vec"></param>
        public void move(Vector2 vec) {
            this.pos.X += vec.X;
            pos.Y += vec.Y;
            boundingBox.X = (int)Math.Round(pos.X-16);
            boundingBox.Y = (int)Math.Round(pos.Y+16);
            collidesWithSurrounding();
            //System.Diagnostics.Debug.WriteLine("{0} {1}", pos.x, pos.y);
        }
        
        /// <summary>
        /// picks a specific item from the map and adds it to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the added item in the inventory</returns>
        public bool pickup(Item item) {
            if(inventory.getSizeOfInv() == 0) { currentItem = 1; }
            return inventory.addItemToInv(item);
        }

        /// <summary>
        /// gets the selected item name of the inventory
        /// </summary>
        /// <returns>the selected itemName</returns>
        public string getCurrentItemName() {
            return inventory.getItemName(currentItem - 1);
        }

        /// <summary>
        /// Drops the item in the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the item that removed is</returns>
        public bool dropItem(Item item) {
            return inventory.removeItemFromInv(item);
        }

        /// <summary>
        /// gets the item in the given slot of the list
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>specific item in slot</returns>
        public Item getItemInSlot(int slot) {
            return inventory.getItemInSlot(slot);
        }

        /// <summary>
        /// makes sure the player gets dmg on hit
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>hp</returns>
        public byte takeDamage(byte damage) {
            hp -= damage;
            if (hp <= 0)
            {
                this.alive = false;
            }
            return hp;
        }

        /// <summary>
        /// increases health with healing item
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>hp</returns>
        public byte increaseHealth(byte amount) {
            hp += amount;
            if(hp > 100)
            {
                hp = 100;
            }
            return hp;
        }

        /// <summary>
        /// increases armor with armor item
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public byte increaseArmor(byte amount) {
            armor += amount;
            if(armor > 100)
            {
                armor = 100;
            }
            return armor;
        }


        public void update() {
            // TODO
        }

        /// <summary>
        /// shoot a bullet from the player
        /// </summary>
        /// <param name="orientation"></param>
        public void shoot(float orientation)
        {
            // Sound management variables
            List<sounds> soundsList = new List<sounds> { sounds.SHOT_PISTOL_0, sounds.SHOT_PISTOL_1};
            Random rand = new Random();

            // Bullet variables.
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            MouseState current_mouse = Mouse.GetState();
            Vector2 bulletTarget = pos - current_mouse.Position.ToVector2();
            Vector2 spawnPosition;

            spawnPosition.X = pos.X;
            spawnPosition.Y = pos.Y;
            Weapon w = (Weapon)inventory.getItemInSlot(currentItem - 1);
            Bullet bullet = new Bullet(spawnPosition, -bulletTarget, orientation, w.spread);
            if(w.getName().Equals("Shotgun"))
            {
                System.Diagnostics.Debug.WriteLine("SHOT");
                state.spawnedBullets.Add(new Bullet(boundingBox.Center.ToVector2(), -bulletTarget, orientation, w.spread));
                state.spawnedBullets.Add(new Bullet(boundingBox.Center.ToVector2(), -bulletTarget, orientation, w.spread));
                state.spawnedBullets.Add(new Bullet(boundingBox.Center.ToVector2(), -bulletTarget, orientation, w.spread));
                state.spawnedBullets.Add(new Bullet(boundingBox.Center.ToVector2(), -bulletTarget, orientation, w.spread));
            }
            state.spawnedBullets.Add(bullet);
            
            // Play the gunsound.
            int sound = rand.Next(0, soundsList.Count());
            state.PlaySoundEffect(soundsList[sound]);
        }

        /// <summary>
        /// throws an explosive away
        /// </summary>
        public void ThrowExplosive() {
            Vector2 spawnPosition;
            Vector2 bulletTarget;

            bulletTarget.X = Mouse.GetState().X;
            bulletTarget.Y = Mouse.GetState().Y;

            spawnPosition.X = pos.X;
            spawnPosition.Y = pos.Y;

            Bullet bullet = new Bullet(spawnPosition, bulletTarget, orientation,100);
            state.spawnedBullets.Add(bullet);
        }

        private bool checkCollision(Vector2 pos) {
            // TODO
            return true;
        }

        /// <summary>
        /// returns the orientation of the player
        /// </summary>
        /// <returns></returns>
        public float getOrientation()
        {
            return orientation;
        }
    }
}
