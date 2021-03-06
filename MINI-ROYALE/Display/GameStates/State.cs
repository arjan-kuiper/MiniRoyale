﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE {
    /// <summary>
    /// Base class for each State. Defines the required methods and fields for an abstraction of this class.
    /// </summary>
    public abstract class State {
        #region Fields
        protected ContentManager _content;
        protected Game _game;
        protected GraphicsDevice _graphicsDevice;
        #endregion

        #region Methods
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate(GameTime gametime);

        public State(Game game, GraphicsDevice graphicsDevice, ContentManager content) {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }
        #endregion
    }
}
