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
        private List<Component> _components = new List<Component>();
        private GraphicsDevice _graphicsDevice;

        public MenuState(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
            _graphicsDevice = graphicsDevice;
            createComponents();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            Texture2D background = _content.Load<Texture2D>("Controls/Menu_Background");
            spriteBatch.Draw(background, _graphicsDevice.Viewport.Bounds, Color.White);
            foreach (Component component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gametime) {
            // Remove sprites if they're not needed.
        }

        public override void Update(GameTime gameTime) {
            foreach(Component component in _components) {
                component.Update(gameTime);
            }
        }

        private void createComponents() {
            //TODO: Make this method dynamic with a while-loop.
            Texture2D btnTexture, btnHoverTexture;
            Button btn;

            // === Start define Play Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Play_Game");
            btnHoverTexture = _content.Load<Texture2D>("Controls/Play_Game_Selected");

            btn = new Button(btnTexture, btnHoverTexture) {
                Position = new Vector2(25, 450)
            };
            // Set click event listener.
            btn.Click += PlayGameButton_Click;
            _components.Add(btn);
            // === Stop define Play Button ===
            // === Start define Settings Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Settings");
            btnHoverTexture = _content.Load<Texture2D>("Controls/Settings_Selected");

            btn = new Button(btnTexture, btnHoverTexture) {
                Position = new Vector2(200, 590)
            };
            // Set click event listener.
            btn.Click += SettingsGameButton_Click;
            _components.Add(btn);
            // === Stop define Settings Button ===
            // === Start define Credits Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Credits");
            btnHoverTexture = _content.Load<Texture2D>("Controls/Credits_Selected");

            btn = new Button(btnTexture, btnHoverTexture) {
                Position = new Vector2(25, 730)
            };
            // Set click event listener.
            btn.Click += CreditsGameButton_Click;
            _components.Add(btn);
            // === Stop define Credits Button ===
            // === Start define Quit Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Quit");
            btnHoverTexture = _content.Load<Texture2D>("Controls/Quit_Selected");

            btn = new Button(btnTexture, btnHoverTexture) {
                Position = new Vector2(200, 870)
            };
            // Set click event listener.
            btn.Click += QuitGameButton_Click;
            _components.Add(btn);
            // === Stop define Quit Button ===
        }

        //TODO: Fill in the action upon buttonclick.
        #region ClickEvents
        private void QuitGameButton_Click(object sender, EventArgs e) {
            _game.Exit();
        }

        private void CreditsGameButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void PlayGameButton_Click(object sender, EventArgs e) {
            // Change state to screen where you can enter IP and Port for server.
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void SettingsGameButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
