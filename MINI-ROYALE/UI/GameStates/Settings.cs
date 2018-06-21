using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MINI_ROYALE {
    class Settings : State {
        private List<Component> _components = new List<Component>();

        #region StateMethods
        public Settings(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
            CreateComponents();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            // Draw the background screen.
            Texture2D background = _content.Load<Texture2D>("Controls/Menu_Background");
            spriteBatch.Draw(background, _graphicsDevice.Viewport.Bounds, Color.White);

            // Draw the components to the screen.
            foreach (Component component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gametime) {
            // Not needed for this case.
        }

        public override void Update(GameTime gameTime) {
            foreach (Component component in _components) {
                component.Update(gameTime);
            }
        }
        #endregion
        #region SettingStateMethods
        private void createComponents() {
            SpriteFont font = _content.Load<SpriteFont>(@"Fonts\ThirteenPixels");
            

            _components.Add(new TextField(font, 400, 100) {
                position = new Vector2(148, 250),
                text = "SETTINGS :D",
                penColor = Color.Black
            });
        }
        #endregion
    }
}
