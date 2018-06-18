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
        public Bullet(Vector2 position, Vector2 direction)
        {
            this.texture = texture;
            this.position = position;
            this.direction = direction;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Texture2D texture { get; set; }

        public Vector2 position { get; set; }

        public Vector2 direction { get; set; }

        private Vector2 origin;
        private float speed = 15f;
        public float lifeTime = 60f; //2 sec

        public void Update()
        {
            direction.Normalize();
            position += direction * speed;
            if (lifeTime > 0) { lifeTime--; }
        }
    }
}
