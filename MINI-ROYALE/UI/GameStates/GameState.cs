using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MINI_ROYALE {
    /// <summary>
    /// This is the 'GameState' test state. To Check whether the menu buttons actually push you to the correct gameState.
    /// </summary>
    public class GameState : State {
        #region StateVariables
        private SpriteFont font;
        private TileMap tileMap;
        private Player player;
        private InputHandler inputHandler;
        private Zone zone = new Zone();


        private int counter;
        private const int LIMIT = 60;
        private const float COUNT_DURATION = 1800f;
        private float currentTime = 0f;

        // Collections used in the GameState state.
        public List<Item> items = new List<Item>();
        public List<Bullet> spawnedBullets = new List<Bullet>();

        #endregion
        #region StateMethods
        public GameState(Game game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
            // Initialize variables.
            tileMap = new TileMap(graphicsDevice);
            player = new Player(this);
            inputHandler = new InputHandler(player);

            LoadContent();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // Draw game components.
            tileMap.setGameDevice(_game);
            tileMap.draw(spriteBatch, player);
            Draw_Items(spriteBatch);
            Draw_Bullets(spriteBatch);
            player.draw(spriteBatch, _game);
            Draw_Inventory(spriteBatch);
            ZoneContraction(gameTime, spriteBatch);
        }

        public override void PostUpdate(GameTime gametime) {
            // This has no purpose for this case.
        }

        public override void Update(GameTime gameTime) {
            inputHandler.walk();
            inputHandler.mouseListener();
            inputHandler.interaction();
            tileMap.Camera.LookAt(player.pos);
        }
        #endregion
        #region GameStateMethods
        /// <summary>
        /// Update's the bullet's location. If lifecycle is ending, destroy the bullet instance.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch that draws the objects.</param>
        private void Draw_Bullets(SpriteBatch spriteBatch) {
            List<Bullet> toRemove = new List<Bullet>();
            foreach (Bullet bullet in toRemove) { spawnedBullets.Remove(bullet); }
            // Check whether the bullets are at the end of their lifecycle.
            foreach (Bullet bullet in spawnedBullets) {
                if (bullet.isDead()) {
                    toRemove.Add(bullet);
                    continue;
                }
                // Re-draw bullet for next location.
                bullet.Update();
                bullet.Draw(spriteBatch);
            }
            
            // Remove the actual bullets.
            foreach (Bullet bullet in toRemove) { spawnedBullets.Remove(bullet); }
        }
        /// <summary>
        /// Draw the inventory with picked up items to the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch that draws the objects.</param>
        private void Draw_Inventory(SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            // Instantiate the inventory.
            String invItems = "Inventory Items:\n";
            if (player.getItemInSlot(0) != null) invItems += player.getItemInSlot(0).getName() + ((player.currentItem == 1) ? " (SELECTED)" : "") + "\n";
            if (player.getItemInSlot(1) != null) invItems += player.getItemInSlot(1).getName() + ((player.currentItem == 2) ? " (SELECTED)" : "") + "\n";
            if (player.getItemInSlot(2) != null) invItems += player.getItemInSlot(2).getName() + ((player.currentItem == 3) ? " (SELECTED)" : "") + "\n";
            if (player.getItemInSlot(3) != null) invItems += player.getItemInSlot(3).getName() + ((player.currentItem == 4) ? " (SELECTED)" : "") + "\n";
            if (player.getItemInSlot(4) != null) invItems += player.getItemInSlot(4).getName() + ((player.currentItem == 5) ? " (SELECTED)" : "") + "\n";

            // Actually draw it here.
            spriteBatch.DrawString(font, invItems, new Vector2(100, 100), Color.Black);
            spriteBatch.End();
        }

        /// <summary>
        /// Draw all items to the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch that draws the objects.</param>
        private void Draw_Items(SpriteBatch spriteBatch) {
            foreach (Item item in items) {
                item.draw(spriteBatch);
            }
        }

        /// <summary>
        /// A method in which required assets can be loaded into the GameState state.
        /// </summary>
        private void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            items.Add(new HealingItem("Medkit", _content.Load<Texture2D>("items/medic"), new Vector2(32, 32)));
            items.Add(new HealingItem("Bandage", _content.Load<Texture2D>("items/bandage"), new Vector2(32, 48)));
            items.Add(new HealingItem("Health Potion", _content.Load<Texture2D>("items/potion-health"), new Vector2(32, 64)));
            items.Add(new Weapon("Pistol", _content.Load<Texture2D>("items/pistol"), new Vector2(32, 80)));
            items.Add(new Weapon("Shotgun", _content.Load<Texture2D>("items/shotgun"), new Vector2(32, 96)));
            items.Add(new Weapon("Shotgun", _content.Load<Texture2D>("items/shotgun"), new Vector2(32, 112)));

            font = _content.Load<SpriteFont>("TempInv");
        }

        private void ZoneContraction(GameTime gameTime, SpriteBatch spriteBatch) {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

            if (currentTime >= COUNT_DURATION) {
                counter++;
                currentTime -= COUNT_DURATION;   // "use up" the time
                zone.draw(spriteBatch);         //any actions to perform
            }
            if (counter >= LIMIT) {
                //Reset the counter;
                counter = 0;
            }
            zone.draw(spriteBatch);
        }

        /// <summary>
        /// Removes the referenced item from the map.
        /// </summary>
        /// <param name="pos">The position of the item.</param>
        /// <returns></returns>
        public bool RemoveItemFromMap(Vector2 pos) {
            // Een traditionele loop, omdat die removal safe is
            for (int i = items.Count - 1; i >= 0; i--) {
                if (pos == items[i].pos) {
                    items.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Puts an Item object to the map on the players position.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddItemToMap(Item item) {
            items.Add(item);
            return true;
        }
        #endregion
    }
}
