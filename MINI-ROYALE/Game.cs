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
        public static Game instance;
        GraphicsDeviceManager graphics;
        TileMap tm;
        private SpriteBatch spritebatch;
        Player p;
        InputHandler h;

        // voor items op de map (Busy)
        private Dictionary<Vector2, Item> items;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            // list voor items
            items = new Dictionary<Vector2, Item>();
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
            // TODO: Add your initialization logic here
            base.Initialize();
            spritebatch = new SpriteBatch(GraphicsDevice);
            tm = new TileMap(GraphicsDevice);

            //Initializes a player
            p = new Player();
            h = new InputHandler(p);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            items.Add(new Vector2(32, 32), new HealingItem("test", Content.Load<Texture2D>("items/medic"), new Vector2(32, 32)));
           

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
            h.mouseListener();
            tm.Camera.LookAt(new Vector2(p.pos.X, p.pos.Y));

        }
        protected bool CollisionCheck()
        {
            // TODO
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
            tm.draw(spritebatch, p);
            foreach(KeyValuePair<Vector2, Item> item in items)
            {
                item.Value.draw(spritebatch);
            }
            p.draw(spritebatch, this);

            base.Draw(gameTime);
        }


        public bool RemoveItemFromMap(Vector2 pos)
        {
            foreach(KeyValuePair<Vector2, Item> needle in items)
            {
                if (pos == needle.Key)
                {
                    items.Remove(needle.Key);
                    return true;
                }
            }
            return false;
        }

        public bool AddItemToMap(Vector2 pos, Item item)
        {

            items.Add(pos, item);
            return true;
        }

    }
}
