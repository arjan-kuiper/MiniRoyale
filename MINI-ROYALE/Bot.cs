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
        public Rectangle boundingBox;
        public byte currentItem;
        public bool alive;
        private int hp;
        private float orientation;

        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 20;

        public Bot(Vector2 pos)
        {
            this.pos = pos;
            this.boundingBox = new Rectangle(32, 32, 16, 16);
            alive = true;
            hp = 100;
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
                rotation = 0;
                //Shoot(p, s);
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
            rotation = (float)Math.Atan2(this.pos.Y - p.pos.Y, this.pos.X - p.pos.X) + (float)Math.PI;
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

        public void Shoot(Player p, GameState state)
        {
            //TODO: Fire at player pos not mousePos
            // Sound management variables
         
            List<sounds> soundsList = new List<sounds> { sounds.SHOT_PISTOL_0, sounds.SHOT_PISTOL_1 };
            Random rand = new Random();

            // Bullet variables.
            Vector2 bulletTarget = new Vector2(p.pos.X, p.pos.Y);
            Vector2 spawnPosition;

            orientation = 0;

            spawnPosition.X = this.pos.X;
            spawnPosition.Y = this.pos.Y;

            Bullet bullet = new Bullet(spawnPosition, -bulletTarget, orientation, 100);
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

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //IsEnabled defaults to false
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            timesTicked++;
            if (timesTicked > timesToTick)
            {
                stopTime = time;
                dispatcherTimer.Stop();
                //IsEnabled should now be false after calling stop
                span = stopTime - startTime;
            }
        }
    }
}

