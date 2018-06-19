using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MINI_ROYALE
{
    class Bullet
    {
        private Vector2 origin;

        public Bullet(Vector2 position, Vector2 direction)
        { 
            this.position = position;
            this.direction = direction;
            this.boundingBox = new Rectangle();
        }

        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }

        private float speed = 15f;
        public float lifeTime = 60f; //2 sec
        public float scale;
        public static float radius = 4f;

        public Rectangle boundingBox;
        public Texture2D bulletSprite;

        public void Update()
        {
            direction.Normalize();
            position += direction * speed;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            if (lifeTime > 0) { lifeTime--; }
        }

        public void Draw(SpriteBatch spriteBatch, Game game)
        {
            spriteBatch.Begin();
            bulletSprite = game.Content.Load<Texture2D>("Bulletsprite");
            Vector2 origin = new Vector2(bulletSprite.Width / 2, bulletSprite.Height / 2);
            scale = radius * 2 / bulletSprite.Width;
            spriteBatch.Draw(bulletSprite, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 1);
            spriteBatch.End();
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            boundingBox.Width = bulletSprite.Width;
            boundingBox.Height = bulletSprite.Height;
        }
    }
}