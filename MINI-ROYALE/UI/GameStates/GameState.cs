using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MINI_ROYALE {
    /// <summary>
    /// This is the 'GameState' test state. To Check whether the menu buttons actually push you to the correct gameState.
    /// </summary>
    public class GameState : State {
        public GameState(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            throw new NotImplementedException();
        }

        public override void PostUpdate(GameTime gametime) {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            throw new NotImplementedException();
        }
    }
}
