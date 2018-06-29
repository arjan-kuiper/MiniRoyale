using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Windows.UI.Xaml;

namespace MINI_ROYALE
{
    public class Bot
    {
        public Vector2 pos;
        public bool isBot;
        public Rectangle boundingBox;
        public byte currentItem;
        public bool alive;
        private int hp;
        private float orientation;
        public int timer;

        public Bot(Vector2 pos)
        {
            this.isBot = true;
            this.pos = pos;
            this.boundingBox = new Rectangle(32, 32, 16, 16);
            alive = true;
            hp = 100;
            timer = 0;
        }

        public int collidesWithSurrounding()
        {
            var xCoord = (int)this.pos.X * 16;
            var yCoord = (int)this.pos.Y * 16;
            //System.Diagnostics.Debug.WriteLine("X = {0}. Y = {1}", xCoord, yCoord);
            //Half block check met modulo
            for (int y = yCoord - 1; y < yCoord + 1; y++)
            {
                for (int x = xCoord - 1; x <= xCoord + 1; x++)
                {
                    if (TileMap.instance.mapLoaded == true)
                    {
                        bool tileHasCollision = TileMap.instance.getTileOnLoc(x, y).hasCollision;
                        if (tileHasCollision == true)
                        {
                            if (boundingBox.Intersects(new Rectangle((int)TileMap.instance.getTileOnLoc(x, y).position.X, (int)TileMap.instance.getTileOnLoc(x, y).position.Y, 16, 16)))
                            {
                                // Collision
                            }
                        }
                    }
                }
            }
            return 1;
        }

        public void draw(SpriteBatch spriteBatch, Game game)
        {
            float rotation;
            spriteBatch.Begin(transformMatrix: TileMap.instance.cam.GetViewMatrix());
            GameState s = (GameState)game.getState();
            Player p = s.GetPlayer();

            if (getTarget(p))
            {
                rotation = (float)Math.Atan2(this.pos.Y - p.pos.Y, this.pos.X - p.pos.X) + (float)Math.PI;
                if (timer >= 40)
                {
                    shoot(p, s, this);
                    timer = 0;
                } 
                timer++;
            }
            else    
            {
                int r1 = 0;
                int r2 = 0;
                rotation = 0.5f;

                r1 = (p.pos.X > pos.X) ? 1 : -1;
                r2 = (p.pos.Y > pos.Y) ? 1 : -1;

                Move(new Vector2(r1, r2));
            }

            string texture = "player-weapon";
            Texture2D botImg = game.Content.Load<Texture2D>(texture);
            Vector2 playerOrigin = new Vector2(botImg.Width / 2f, botImg.Height / 2f);
            this.orientation = rotation;

            spriteBatch.Draw(botImg, pos, null, Color.White, rotation, playerOrigin, .1f, SpriteEffects.None, 0f);
            this.boundingBox.X = (int)Math.Round(pos.X - 16);
            this.boundingBox.Y = (int)Math.Round(pos.Y + 16);
            spriteBatch.End();
        }

        public void Move(Vector2 vec)
        {
            this.pos.X += vec.X;
            this.pos.Y += vec.Y;
            boundingBox.X = (int)pos.X;
            boundingBox.Y = (int)pos.Y;
        }

        public bool takeDamage(byte damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                
                alive = false;
            }
            return alive;
        }

        public void shoot(Player p, GameState state, Bot b)
        {
            //TODO: Fire at player pos not mousePos
            // Sound management variables
         
            List<sounds> soundsList = new List<sounds> { sounds.SHOT_PISTOL_0, sounds.SHOT_PISTOL_1 };
            Random rand = new Random();

            // Bullet variables.
            Vector2 bulletTarget = new Vector2(p.pos.X, p.pos.Y);
            Vector2 spawnPosition;

            //orientation = 0;

            spawnPosition.X = b.pos.X;
            spawnPosition.Y = b.pos.Y;

            System.Diagnostics.Debug.WriteLine("{0}, {1}", b.pos.X, b.pos.Y);
            System.Diagnostics.Debug.WriteLine("---");
            System.Diagnostics.Debug.WriteLine("{0}, {1}", p.pos.X, p.pos.Y);

            Bullet bullet = new Bullet(spawnPosition, -bulletTarget, orientation, 100, true);
            state.spawnedBullets.Add(bullet);

            // Play the gunsound.
            int sound = rand.Next(0, soundsList.Count());
            state.PlaySoundEffect(soundsList[sound]);
        }

        private bool checkCollision(Vector2 pos)
        {
            // TODO
            return true;
        }

        private bool getTarget(Player p)
        {    
            float pX = p.pos.X;
            float pY = p.pos.Y;
            
            if (((pos.X - 54) <= pX && (pos.X + 54) >= pX) || ((pos.Y - 64) <= pY && (pos.Y + 64) >= pY))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}

