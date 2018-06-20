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
        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }

        public Vector2 velocity;

        private float speed = 20f;
        public float lifeTime = 60f; //2 sec
        public float rotation;
        public float scale;
        public static float radius = 4f;

        public Rectangle boundingBox;
        public Texture2D bulletSprite;

        public Bullet(Vector2 position, Vector2 direction, float rotation)
        {
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            this.position = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            this.direction = direction;
            this.rotation = rotation;
            this.boundingBox = new Rectangle();
        }

        public void Update()
        {
            velocity.Y = speed;
            velocity.X = speed;
            Vector2 direction = new Vector2((float)Math.Cos(rotation),
                                     (float)Math.Sin(rotation));
            direction.Normalize();
            position += direction * velocity;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
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

            spriteBatch.Draw(bulletSprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            spriteBatch.End();
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            boundingBox.Width = bulletSprite.Width;
            boundingBox.Height = bulletSprite.Height;
            System.Diagnostics.Debug.WriteLine("b update");
        }
    }
}