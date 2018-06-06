using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace MINI_ROYALE
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        TileMap tm;
        private SpriteBatch spritebatch;
        Player p;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            
            
        }

       

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            spritebatch = new SpriteBatch(GraphicsDevice);
            tm = new TileMap(GraphicsDevice,100, 100, 5, 5);

            //Initializes a player
            p = new Player();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
           

            // TODO: use this.Content to load your game content here
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
            // TODO: Add your update logic here
            base.Update(gameTime);
            float speed = 3.5224f;
            Vector2 moveVel = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                moveVel += new Vector2(0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                moveVel += new Vector2(0, 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                moveVel += new Vector2(-1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                moveVel +=new Vector2(1, 0);
            }
            moveVel *= speed;
            p.Move(moveVel);
            
            tm.Camera.LookAt(new Vector2(p.pos.x, p.pos.y));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);
            tm.draw(spritebatch);

            spritebatch.Begin();
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            Texture2D playerImg = Content.Load<Texture2D>("player");
            Vector2 playerOrigin = new Vector2(playerImg.Width / 2f, playerImg.Height / 2f);
            MouseState current_mouse = Mouse.GetState();
            Vector2 mousePos = new Vector2(current_mouse.X, current_mouse.Y);
            Vector2 dPos = new Vector2(viewport.Width / 2f, viewport.Height / 2f) - mousePos;
            float rotation = (float)Math.Atan2(dPos.Y, dPos.X);
            System.Diagnostics.Debug.WriteLine("{0} {1}", current_mouse.X, current_mouse.Y);
                        
            spritebatch.Draw(playerImg, new Vector2(viewport.Width / 2f, viewport.Height / 2f), null, Color.White, rotation, playerOrigin, 1f, SpriteEffects.None, 0f);
            spritebatch.End();

            base.Draw(gameTime);
          
        }
    }
}
