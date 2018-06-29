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
        public Rectangle HealthUiRectangle {
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
            spriteBatch.Begin();

            // Draw Back layer, covers the part where the healthbar is not visible.
            spriteBatch.Draw(_texture_back, HealthUiRectangle, Color.White);
            // Draw actual health bar rectangle.
            spriteBatch.Draw(empty, GetHealthRectangle(), Color.White);
            // Draw front layer.
            spriteBatch.Draw(_texture_front, HealthUiRectangle, Color.White);


            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) {
            // Do nothing.
        }

        /// <summary>
        /// Calculate position and size of the healtbar to be drawn.
        /// </summary>
        /// <returns>Rectangle to be drawn</returns>
        private Rectangle GetHealthRectangle() {
            // Calculate the position for the HP bar to be drawn.
            int x = (int)Position.X + 32;
            int y = (int) Position.Y + 188 / 2 - 80;

            // Precaution so that health will never be drawn over it's limit.
            byte hp = _player.hp;
            if (hp > 100)
                hp = 100;
            
            // Calculate the display length of the healthbar.
            int width = (int)(340f / 100f * hp);
            return new Rectangle(x, y, width, 80);
        }
        #endregion
    }
}
