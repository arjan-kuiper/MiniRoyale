using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MINI_ROYALE {
    class Healthbar : Component {
        
        #region Fields
        private Texture2D _texture_front, _texture_back, empty;
        private Rectangle healtbar;
        private Player _player;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }
        public Rectangle btnRectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture_front.Width, _texture_front.Height);
            }
        }
        #endregion

        #region Methods
        public Healthbar (Texture2D texture_front, Texture2D texture_back, GraphicsDevice graphics, Player player) {
            _texture_front = texture_front;
            _texture_back = texture_back;
            _player = player;

            empty = new Texture2D(graphics, 1, 1);

            Color[] colorData = {
                Color.Green,
            };

            // Set the texture data with our color information.  
            empty.SetData<Color>(colorData);
        }




        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // Draw Back layer
            spriteBatch.Begin();
            spriteBatch.Draw(_texture_back, btnRectangle, Color.White);
            // Draw actual health status

            spriteBatch.Draw(empty, GetHealthRectangle(), Color.White);
            // Draw front layer.
            spriteBatch.Draw(_texture_front, btnRectangle, Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) {
            // Do nothing.
        }

        private Rectangle GetHealthRectangle() {
            int x = (int)Position.X + 32;
            int y = (int) Position.Y + 188 / 2 - 80;

            byte hp = _player.hp;
            hp = 100;
            if (hp > 100)
                hp = 100;

            hp = 60;

            int width = 340 / 100 * hp;
            return new Rectangle(x, y, width, 80);
        }
        #endregion
    }
}
