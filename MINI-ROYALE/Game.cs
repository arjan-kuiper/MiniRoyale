using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
        inputHandler h;

        // voor items op de map (Busy)
        private List<Item/*, Position pos*/> items;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            // list voor items
            items = new List<Item/*, Position*/>();
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
            tm = new TileMap(GraphicsDevice,100, 100, 1, 1);

            //Initializes a player
            p = new Player();
            h = new inputHandler(p);
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

            h.walk();
            tm.Camera.LookAt(new Vector2(p.pos.x, p.pos.y));

        }
        protected bool collisionCheck()
        {
            p.pos.getGridPosition(tm.getTileSize());
            //Check for colission
            if (true)
            {
                //dome something with the colission
                return true;
            }
            return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);
            tm.setGameDevice(this);
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


        public bool removeItem(/*pos x, pos y (Position pos)*/)
        {
            // kut shit hier moet weg!!!!
            int x = 0;
            int count = 0;
            foreach(Item needle in items)
            {
                count++;
                if (x == 0/*needle.pos = pos*/)
                {
                    items.RemoveAt(count);
                    return true;
                }
            }
            return false;
        }

        public bool addItem(/*Item item, pos x, pos y (Position pos)*/)
        {
            return true;
        }

    }
}
