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

        private float speed = 0.2f;
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
            this.rotation = rotation - 80;
            this.boundingBox = new Rectangle();
        }

        public void Update()
        {
            direction.Normalize();
            position += direction * speed;
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
            Vector2 origin = new Vector2(bulletSprite.Width / 2, bulletSprite.Height / 2);
            scale = radius * 2 / bulletSprite.Width;
            Viewport viewport = Game.instance.GraphicsDevice.Viewport;
            MouseState current_mouse = Mouse.GetState();

            spriteBatch.Draw(bulletSprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 1);
            spriteBatch.End();
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            boundingBox.Width = bulletSprite.Width;
            boundingBox.Height = bulletSprite.Height;
            System.Diagnostics.Debug.WriteLine("b update");
        }
    }
}