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
        public Settings(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            // Draw the background screen.
            Texture2D background = _content.Load<Texture2D>("Controls/Menu_Background");
            spriteBatch.Draw(background, _graphicsDevice.Viewport.Bounds, Color.White);

            //TODO: Draw components here.
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gametime) {
            // Not needed for this case.
        }

        public override void Update(GameTime gameTime) {
            //TODO: Update components here.
        }
    }
}
