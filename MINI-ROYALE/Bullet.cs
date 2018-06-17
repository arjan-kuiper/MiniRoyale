using System;

namespace MINI_ROYALE
{
    class Bullet
    {
        public Bullet(Texture2D sprite, Vector2 position, float direction)
        {
            this.texture = texture;
            this.position = position;
            this.direction = direction;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            scale = radius * 2 / texture.Width;
            hitbox = new Circle(radius, position);
        }

        public Texture2D texture { get; set; }

        public Vector2 position { get; set; }

        public Vector2 direction { get; set; }

        private Vector2 origin;
        private float speed = 15f;
        public float lifeTime = 60f; //2 sec
        public Circle hitbox;

        public void Update()
        {
            direction.Normalize();
            position += direction * speed;
            if (lifeTime > 0) { lifeTime--; }
            hitbox.position = position;
        }
    }
}

