using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Windows.UI.Xaml;

namespace MINI_ROYALE {
    class LoadingState : State {
        private List<Component> _components = new List<Component>();
        private const int LOADING_DISPLAY_TIME = 2;
        private DispatcherTimer timer = new DispatcherTimer();

        #region StateMethods
        public LoadingState(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
            CreateComponents();
            timer.Tick += Tick_Timer;
            timer.Interval = new TimeSpan(0, 0, LOADING_DISPLAY_TIME);
            timer.Start();
        }

        void Tick_Timer(object sender, object e) {
            timer.Stop();
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
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
            // Not used in this usecase.
        }

        public override void Update(GameTime gameTime) {
            foreach (Component component in _components) {
                component.Update(gameTime);
            }
        }
        #endregion
        #region LoadinStateMethods
        private void CreateComponents() {
            /*SpriteFont font = _content.Load<SpriteFont>(@"Fonts\ThirtheenPixels");

            _components.Add(new TextField(font, 400, 100) {
                position = new Vector2(800, _graphicsDevice.Viewport.Bounds.Center.X),
                text = "Loading...",
                penColor = Color.OrangeRed
            });*/
        }
        #endregion
    }
}
