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
        private SpriteFont font;
        GraphicsDeviceManager graphics;
        TileMap tm;
        private SpriteBatch spritebatch;
        Player p;
        InputHandler h;

        // voor items op de map (Busy)
        public List<Item> items = new List<Item>();

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);

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
            items.Add(new HealingItem("Medkit", Content.Load<Texture2D>("items/medic"), new Vector2(32, 32)));
            items.Add(new HealingItem("Bandage", Content.Load<Texture2D>("items/bandage"), new Vector2(32, 48)));
            items.Add(new HealingItem("Health Potion", Content.Load<Texture2D>("items/potion-health"), new Vector2(32, 64)));
            items.Add(new Weapon("Pistol", Content.Load<Texture2D>("items/pistol"), new Vector2(32, 80)));
            items.Add(new Weapon("Shotgun", Content.Load<Texture2D>("items/shotgun"), new Vector2(32, 96)));
            items.Add(new Weapon("Shotgun", Content.Load<Texture2D>("items/shotgun"), new Vector2(32, 112)));

            font = Content.Load<SpriteFont>("TempInv");
           

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
            h.interaction();
            tm.Camera.LookAt(p.pos);

            Viewport viewport = GraphicsDevice.Viewport;
            //System.Diagnostics.Debug.WriteLine(new Vector2(viewport.Width, viewport.Height));
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
            foreach(Item item in items)
            {
                item.draw(spritebatch);
            }
            p.draw(spritebatch, this);

            spritebatch.Begin();
            String invItems = "Inventory Items:\n";
            if (p.getItemInSlot(0) != null) invItems += p.getItemInSlot(0).getName() + ((p.currentItem == 1) ? " (SELECTED)" : "") + "\n";
            if (p.getItemInSlot(1) != null) invItems += p.getItemInSlot(1).getName() + ((p.currentItem == 2) ? " (SELECTED)" : "") + "\n";
            if (p.getItemInSlot(2) != null) invItems += p.getItemInSlot(2).getName() + ((p.currentItem == 3) ? " (SELECTED)" : "") + "\n";
            if (p.getItemInSlot(3) != null) invItems += p.getItemInSlot(3).getName() + ((p.currentItem == 4) ? " (SELECTED)" : "") + "\n";
            if (p.getItemInSlot(4) != null) invItems += p.getItemInSlot(4).getName() + ((p.currentItem == 5) ? " (SELECTED)" : "") + "\n";

            spritebatch.DrawString(font, invItems, new Vector2(100, 100), Color.Black);
            spritebatch.End();
            

            base.Draw(gameTime);
        }


        public bool RemoveItemFromMap(Vector2 pos)
        {
            // Een traditionele loop, omdat die removal safe is
            for(int i = items.Count - 1; i >= 0; i--)
            {
                if (pos == items[i].pos)
                {
                    items.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool AddItemToMap(Item item)
        {
            items.Add(item);
            return true;
        }

    }
}
