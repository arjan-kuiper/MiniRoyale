using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MINI_ROYALE {
    class TextField : Component {
        
        #region Fields
        private SpriteFont _font;
        public int _width, _height;
        #endregion

        #region Properties
        public bool Clicked {  get; private set; }
        public Vector2 position { private get; set; }
        public String text { private get; set; }
        public Color penColor { private get; set; }
        public Rectangle btnRectangle {
            get {
                return new Rectangle((int)position.X, (int)position.Y, _width, _height);
            }
        }
        #endregion

        #region Methods
        public TextField(SpriteFont font, int width, int height) {
            _font = font;
            _width = width;
            _height = height;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // Calculate right positioning for the text based on the font dimensions and defined rectangle sizes.
            float x = (btnRectangle.X + (btnRectangle.Width / 2) - (_font.MeasureString(text).X / 2));
            float y = (btnRectangle.Y + (btnRectangle.Height / 2) - (_font.MeasureString(text).Y / 2));

            // Draw the text to the viewport.
            spriteBatch.DrawString(_font, text, new Vector2(x, y), penColor);
        }

        public override void Update(GameTime gameTime) {
            // Do nothing, just be pretty.
        }
        #endregion
    }
}
