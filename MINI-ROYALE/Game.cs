using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace MINI_ROYALE
{
    public enum songs { NONE, AMBIENT, WIN, LOSS }
    public enum sounds { NONE, SHOT_PISTOL_0, SHOT_PISTOL_1, SHOT_SHOTGUN_0, SHOT_SHOTGUN_1, HIT_0, HIT_1}
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

        private Dictionary<songs, Song> songsList = new Dictionary<songs, Song>();
        private songs currentSong, nextSong;
        #endregion

        public static Game instance;
        
        #endregion
        
        /// <summary>
        /// gets the current state
        /// </summary>
        /// <returns>current state</returns>
        public State getState() {
            return currentState;
        }

        /// <summary>
        /// change the currentstate if a change happens
        /// </summary>
        /// <param name="state"></param>
        public void changeState(State state) {
            nextState = state;
        }

        /// <summary>
        /// Gets the graphicsdevicemanager
        /// </summary>
        public GraphicsDeviceManager getGraphics()
        {
            return graphics;
        }


        /// <summary>
        /// makes the game with the screen
        /// </summary>
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
            nextSong = songs.AMBIENT;
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
            songsList.Add(songs.AMBIENT, Content.Load<Song>(@"sounds\Ambient"));
            songsList.Add(songs.WIN, Content.Load<Song>(@"sounds\Win"));
            songsList.Add(songs.LOSS, Content.Load<Song>(@"sounds\Loss"));
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
            soundHandler(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This method is being called each update. to check whether another song should be played.
        /// </summary>
        /// <param name="gametime"></param>
        private void soundHandler(GameTime gametime) {
            if (nextSong != songs.NONE) {
                currentSong = nextSong;
                nextSong = songs.NONE;

                // sets the volume of the song
                MediaPlayer.Volume = 0.1f;
                MediaPlayer.Play(songsList[currentSong]);
            }
        }

        /// <summary>
        /// loops through all the songs that are in a list
        /// </summary>
        /// <param name="song"></param>
        public void changeSong(songs song) {
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