using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MINI_ROYALE {
    class VolumeEditor : Component {

        #region Fields
        private Button _arrow_up, _arrow_down;
        private SpriteFont font;
        #endregion

        #region Properties
        public Vector2 VolumePosition { get; set; }
        public Rectangle VolumeEditorRectangle {
            get {
                return new Rectangle((int)VolumePosition.X, (int)VolumePosition.Y, 400, 120);
            }
        }
        #endregion

        #region Methods
        public VolumeEditor(Texture2D arrow_up, Texture2D arrow_down, SpriteFont font) {
            _arrow_up = new Button(arrow_up) {
                Position = new Vector2(VolumePosition.X, VolumePosition.Y)
            };
            System.Diagnostics.Debug.WriteLine("" + VolumePosition.X);
            _arrow_down = new Button(arrow_down) {
                Position = new Vector2(VolumePosition.X, VolumePosition.Y + VolumeEditorRectangle.Height / 2)
            };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            _arrow_up.Draw(gameTime, spriteBatch);
            _arrow_down.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime) {
            _arrow_up.Update(gameTime);
            _arrow_down.Update(gameTime);
        }
        #endregion
    }
}
