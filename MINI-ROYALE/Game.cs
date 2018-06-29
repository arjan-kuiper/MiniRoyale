using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace MINI_ROYALE
{
    public enum Songs { NONE, AMBIENT, WIN, LOSS }
    public enum Sounds { NONE, SHOT_PISTOL_0, SHOT_PISTOL_1, SHOT_SHOTGUN_0, SHOT_SHOTGUN_1, HIT_0, HIT_1}
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        // === GameState Manager variables ===
        #region Fields
        #region GameStateVars
        private State currentState, nextState;
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        private MouseState _currentMouse, _previousMouse;

        private Dictionary<Songs, Song> songs = new Dictionary<Songs, Song>();
        private Songs currentSong, nextSong;
        #endregion

        public static Game instance;
        
        #endregion
        

        public State getState() {
            return currentState;
        }

        public void changeState(State state) {
            nextState = state;
        }
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            
            instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            nextSong = Songs.AMBIENT;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            // Load songs.
            songs.Add(Songs.AMBIENT, Content.Load<Song>(@"Sounds\Ambient"));
            songs.Add(Songs.WIN, Content.Load<Song>(@"Sounds\Win"));
            songs.Add(Songs.LOSS, Content.Load<Song>(@"Sounds\Loss"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Set the previous mouse actions.
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            // Check whether the user has pressed and released the leftmouse button whilst hovering over the button object.
            if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed) {
                Debug.WriteLine("X = {0} || Y= {1}", _currentMouse.X, _currentMouse.Y);
            }

            if (nextState != null) {
                currentState = nextState;
                nextState = null;
            }

            // Check whether the user has pressed and released the leftmouse button whilst hovering over the button object.
            if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed) {
                Debug.WriteLine("X = {0} || Y= {1}", _currentMouse.X, _currentMouse.Y);
            }

            // === Call the current state's update methods. === //
            currentState.Update(gameTime);
            currentState.PostUpdate(gameTime);
            SoundHandler(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This method is being called each update. to check whether another song should be played.
        /// </summary>
        /// <param name="gametime"></param>
        private void SoundHandler(GameTime gametime) {
            if (nextSong != Songs.NONE) {
                currentSong = nextSong;
                nextSong = Songs.NONE;

                // sets the volume of the song
                MediaPlayer.Volume = 0.1f;
                MediaPlayer.Play(songs[currentSong]);
            }
        }

        /// <summary>
        /// loops through all the songs that are in a list
        /// </summary>
        /// <param name="song"></param>
        public void changeSong(Songs song) {
            nextSong = song;
        }

        /// <summary>
        /// This method is called to write.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Draw base-background color incase something goes wrong. Followed by the currentState's draw methods.
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}