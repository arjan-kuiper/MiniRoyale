using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE {
    public class MenuState : State {
        private List<Component> _components;

        public MenuState(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
            createComponents();
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

        private void createComponents() {
            //TODO: Make this method dynamic with a while-loop.
            Texture2D btnTexture, btnHoverTexture;
            Button btn;

            // === Start define Play Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Play_Game");
            btnHoverTexture = _content.Load<Texture2D>("Controls/Play_Game_Selected");

            btn = new Button(btnTexture, btnHoverTexture) {
                Position = new Vector2(300, 200)
            };
            // Set click event listener.
            btn.Click += PlayGameButton_Click;
            _components.Add(btn);
            // === Stop define Play Button ===
            // === Start define Settings Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Settings");

            btn = new Button(btnTexture) {
                Position = new Vector2(350, 250)
            };
            // Set click event listener.
            btn.Click += SettingsGameButton_Click;
            _components.Add(btn);
            // === Stop define Settings Button ===
            // === Start define Credits Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Credits");

            btn = new Button(btnTexture) {
                Position = new Vector2(300, 300)
            };
            // Set click event listener.
            btn.Click += CreditsGameButton_Click;
            _components.Add(btn);
            // === Stop define Credits Button ===
            // === Start define Quit Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Quit");

            btn = new Button(btnTexture) {
                Position = new Vector2(350, 350)
            };
            // Set click event listener.
            btn.Click += QuitGameButton_Click;
            _components.Add(btn);
            // === Stop define Quit Button ===
        }

        //TODO: Fill in the action upon buttonclick.
        #region ClickEvents
        private void QuitGameButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void CreditsGameButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void PlayGameButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void SettingsGameButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
