using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MINI_ROYALE
{
    public class Bullet
    {
        private Vector2 position { get; set; }
        private Vector2 direction { get; set; }

        private Vector2 velocity;

        private float speed = 20f;
        private float lifeTime = 60f; //2 sec
        private float rotation;
        private float scale;
        private static float radius = 4f;
        double shotoffset;
        private Rectangle boundingBox;
        private Texture2D bulletSprite;

        public Bullet(Vector2 position, Vector2 direction, float rotation, int randomnessfactor)
        {
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            this.position = position;
            this.direction = direction;
            this.rotation = rotation;
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y,3,3);
 
            var result = Randomizer.instance.randomNumber(-randomnessfactor, randomnessfactor);
            System.Diagnostics.Debug.WriteLine(result);
            shotoffset = rotation + 0.001 * result;
        }

        public void update()
        {
            velocity.Y = speed;
            velocity.X = speed;
            Vector2 direction = new Vector2((float)Math.Cos(shotoffset),
                                     (float)Math.Sin(shotoffset));
            direction.Normalize();
            position += direction * velocity;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            
            GameState state = (GameState)Game.instance.getState();
            foreach(Bot b in state.bots)
            {
                if (b.boundingBox.Intersects(this.boundingBox))
                {
                    b.takeDamage(30);
                    state.PlaySoundEffect(Sounds.HIT_0);
                    lifeTime = 0;
                }
            }
            if (lifeTime > 0) { lifeTime--; }
        }

        public bool isDead()
        {
            return lifeTime <= 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            bulletSprite = Game.instance.Content.Load<Texture2D>("Bulletsprite");
            Vector2 origin = new Vector2(bulletSprite.Width, bulletSprite.Height);
            scale = radius * 2 / bulletSprite.Width;
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            MouseState current_mouse = Mouse.GetState();

            spriteBatch.Draw(bulletSprite, position, null, Color.White, (rotation - 80), origin, scale, SpriteEffects.None, 0);
            spriteBatch.End();
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            boundingBox.Width = bulletSprite.Width;
            boundingBox.Height = bulletSprite.Height;
        }
    }
}