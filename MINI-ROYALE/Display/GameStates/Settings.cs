﻿using System;
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
        private TextField volumeShower;

        #region StateMethods
        public Settings(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
            createComponents();
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
        /// <summary>
        /// Constructs the components necessary to run the MenuState screen.
        /// </summary>
        private void createComponents() {
            SpriteFont font = _content.Load<SpriteFont>(@"Fonts\ThirteenPixels");
            Texture2D btnTexture, btnHoverTexture, arrow_up, arrow_down;
            Button btn;

            _components.Add(new TextField(font, 400, 100) {
                position = new Vector2(148, 250),
                text = "SETTINGS :D",
                penColor = Color.Black
            });

            // === Start define Play Button ===
            btnTexture = _content.Load<Texture2D>("Controls/Quit");
            btnHoverTexture = _content.Load<Texture2D>("Controls/Quit_Selected");

            btn = new Button(btnTexture, btnHoverTexture) {
                Position = new Vector2(25, 870)
            };
            // Set click event listener.
            btn.Click += GoBack_Click;
            _components.Add(btn);
            // === Stop define Play Button ===

            // === Start define volume control ===
            arrow_up = _content.Load<Texture2D>("Controls/Arrow_Up");
            arrow_down = _content.Load<Texture2D>("Controls/Arrow_Down");

            btn = new Button(arrow_up) {
                Position = new Vector2(600, 400)
            };
            // Set click event listener.
            //TODO: Set ClickEvent Listener
            // Due to lack of time this functionality has not been implemented.
            //btn.Click += ;
            _components.Add(btn);

            btn = new Button(arrow_down) {
                Position = new Vector2(600, 510)
            };
            // Set click event listener.
            //TODO: Set ClickEvent Listener
            // Due to lack of time this functionality has not been implemented.
            //btn.Click += ;
            _components.Add(btn);

            _components.Add(new TextField(font, 400, 100) {
                position = new Vector2(100, 425),
                text = "Music Volume",
                penColor = Color.Black
            });

            volumeShower = new TextField(font, 400, 100) {
                position = new Vector2(450, 425),
                text = "100%",
                penColor = Color.Red
            };

            _components.Add(volumeShower);
            // === Stop define volume control ===
        }

        private void GoBack_Click(object sender, EventArgs e) {
            // Change state to screen where you can enter IP and Port for server.
            _game.changeState(new MenuState(_game, _graphicsDevice, _content));
        }
        #endregion
    }
}
