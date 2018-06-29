using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MINI_ROYALE {
    class Button : Component {
        
        #region Fields
        private MouseState _currentMouse, _previousMouse;
        private bool _isHovering;
        private Texture2D _texture, _hoverTexture;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle btnRectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        #endregion

        #region Methods
        public Button(Texture2D texture, Texture2D hoverTexture=null) {
            _texture = texture;
            // If hovertexture is not null, set it, if null, set default to hovering aswell.
            _hoverTexture = hoverTexture != null ? hoverTexture : texture;
        }




        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            Texture2D buttonTexture = _texture;
            // Check whether the texture should be swapped.
            if (_isHovering)
                buttonTexture = _hoverTexture;

            // Draw the texture to the screen.
            spriteBatch.Draw(buttonTexture, btnRectangle, Color.White);


        }

        public override void Update(GameTime gameTime) {
            // (Re)Setting the variables for the new cycle
            _isHovering = false;
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            // Check if the user is hovering over the button Object.
            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            if (mouseRectangle.Intersects(btnRectangle)) {
                _isHovering = true;

                // Check whether the user has pressed and released the leftmouse button whilst hovering over the button object.
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed) {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
