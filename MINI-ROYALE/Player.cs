using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MINI_ROYALE
{
    public class Player
    {
        public Vector2 pos;
        public Rectangle boundingBox;
        public byte currentItem;
        public bool alive;
        private byte hp;
        private Inventory inventory;
        private float orientation;

        private GameState state;

        public bool[] moveDirs = { true, true, true, true };

        public Player(GameState state)
        {
            this.pos = new Vector2(400,400);
            inventory = new Inventory(this);
            this.boundingBox = new Rectangle(32, 32, 16, 16);
            alive = true;
        }

        public void collidesWithSurrounding()
        {
            var xCoord = (int)Math.Floor((this.pos.X) / 16);
            var yCoord = (int)Math.Floor((this.pos.Y) / 16);
            //System.Diagnostics.Debug.WriteLine("X = {0}. Y = {1}", xCoord, yCoord);
            //Half block check met modulo
            if (TileMap.instance.mapLoaded)
            {
                System.Diagnostics.Debug.WriteLine("X: {0}, Y: {1}", xCoord, yCoord);
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

        public void Move(Vector2 vec) {
            this.pos.X += vec.X;
            pos.Y += vec.Y;
            boundingBox.X = (int)Math.Round(pos.X-16);
            boundingBox.Y = (int)Math.Round(pos.Y+16);
            collidesWithSurrounding();
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
            this.hp += damage;
            if (hp <= 0)
            {
                this.alive = false;
            }
            return hp;
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
            // Sound management variables
            List<Sounds> sounds = new List<Sounds> { Sounds.SHOT_PISTOL_0, Sounds.SHOT_PISTOL_1};
            Random rand = new Random();

            // Bullet variables.
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            MouseState current_mouse = Mouse.GetState();
            Vector2 bulletTarget = new Vector2(viewport.Width / 2f, viewport.Height / 2f) - current_mouse.Position.ToVector2();
            Vector2 spawnPosition;

            spawnPosition.X = pos.X;
            spawnPosition.Y = pos.Y;


            Bullet bullet = new Bullet(spawnPosition, -bulletTarget, orientation);
            state.spawnedBullets.Add(bullet);
            
            // Play the gunsound.
            int sound = rand.Next(0, sounds.Count());
            state.PlaySoundEffect(sounds[sound]);
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
