using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MINI_ROYALE {
    /// <summary>
    /// This is the GameState class. This class handles all in-game related operations to be performed.
    /// </summary>
    public class GameState : State {
        #region StateVariables
        private SpriteFont font;
        private TileMap tileMap;
        private Player player;
        private InputHandler inputHandler;
        
        private Zone zone = new Zone();
        private Dictionary<Sounds, SoundEffect> sounds = new Dictionary<Sounds, SoundEffect>();

        private int counter;
        private const int LIMIT = 15;
        private const float COUNT_DURATION = 45f;

        // Collections used in the GameState state.
        public List<Item> items = new List<Item>();
        public List<Bullet> spawnedBullets = new List<Bullet>();
        public List<Bot> bots = new List<Bot>();
        public List<Component> components = new List<Component>();
        private float currentTime = 0f;

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
            Draw_Bots(spriteBatch);
            player.draw(spriteBatch, _game);
            Draw_Inventory(spriteBatch);
            ZoneContraction(gameTime, spriteBatch);

            foreach (Component component in components) {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void PostUpdate(GameTime gametime) {
            // This has no purpose for this case.
        }

        public override void Update(GameTime gameTime) {
            // Update the game time!
            inputHandler.walk();
            inputHandler.mouseListener();
            inputHandler.interaction();
            tileMap.Camera.LookAt(player.pos);
            List<Bot> toRemove = new List<Bot>();
            foreach(Bot b in bots)
            {
                if(b.alive == false)
                {
                    toRemove.Add(b);
                    
                }
            }
            foreach(Bot b in toRemove)
            {
                bots.Remove(b);
            }

            foreach (Component component in components) {
                component.Update(gameTime);
            }
        }
        #endregion
        #region GameStateMethods

        /// <summary>
        /// Returns the player object.
        /// </summary>
        /// <returns>The Player object.</returns>
        public Player GetPlayer() {
            return player;
        }

        /// <summary>
        /// This methods performs the drawing of each of the in-game bots.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch that draws the objects.</param>
        private void Draw_Bots(SpriteBatch spriteBatch) {
            foreach(Bot bot in bots) {
                bot.draw(spriteBatch, _game);
            }
        }
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
            for (var i =0; i<=100; i++)
            {
                items.Add(new Weapon("Pistol", _content.Load<Texture2D>("items/pistol"), tileMap.getRandomOnMapPosition(),100));
            }
            items.Add(new HealingItem("Medkit", _content.Load<Texture2D>("items/medic"), new Vector2(480, 480), 100));
            items.Add(new HealingItem("Bandage", _content.Load<Texture2D>("items/bandage"), new Vector2(416, 480), 25));
            items.Add(new HealingItem("Health Potion", _content.Load<Texture2D>("items/potion-health"), new Vector2(480, 416), 50));
            items.Add(new Weapon("Pistol", _content.Load<Texture2D>("items/pistol"), new Vector2(512, 480),100));
            items.Add(new Weapon("Shotgun", _content.Load<Texture2D>("items/shotgun"), new Vector2(512, 480),150));
            items.Add(new Weapon("Shotgun", _content.Load<Texture2D>("items/shotgun"), new Vector2(550, 400),150));

            font = _content.Load<SpriteFont>(@"Fonts\TempInv");

            // Load the sound effects for the game.
            sounds.Add(Sounds.SHOT_PISTOL_0, _content.Load<SoundEffect>(@"Sounds\Shot_Pistol_0"));
            sounds.Add(Sounds.SHOT_PISTOL_1, _content.Load<SoundEffect>(@"Sounds\Shot_Pistol_1"));
            sounds.Add(Sounds.SHOT_SHOTGUN_0, _content.Load<SoundEffect>(@"Sounds\Shot_Shotgun_0"));
            sounds.Add(Sounds.SHOT_SHOTGUN_1, _content.Load<SoundEffect>(@"Sounds\Shot_Shotgun_1"));
            sounds.Add(Sounds.HIT_0, _content.Load<SoundEffect>(@"Sounds\Hit_0"));
            bots.Add(new Bot(new Vector2(300, 300)));


            // === COMPONENT CREATION === 
            Texture2D front = _content.Load<Texture2D>(@"Controls\Healthbar_Front");
            Texture2D back = _content.Load<Texture2D>(@"Controls\Healthbar_Back");

            components.Add(new Healthbar(front, back, _graphicsDevice, player) {
                Position = new Vector2(25, 900)
            });
        }

        /// <summary>
        /// Contract the 'Rectangle' zone each duration time.
        /// </summary>
        /// <param name="gameTime">Time passed </param>
        /// <param name="spriteBatch">The SpriteBatch that draws the objects.</param>
        private void ZoneContraction(GameTime gameTime, SpriteBatch spriteBatch) {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= COUNT_DURATION) {
                counter++;
                currentTime -= COUNT_DURATION;                                                  // "use up" the time
                zone.update(zone.GetRandomCoordsForZone(), zone.GetRandomCoordsForZone());      //any actions to perform
            }
            if (counter >= LIMIT) {
                //Reset the counter;
                counter = 0;
            }
            zone.draw(spriteBatch);
        }

        public void PlaySoundEffect(Sounds sound) {
            SoundEffectInstance instance = sounds[sound].CreateInstance();
            instance.Play();
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
