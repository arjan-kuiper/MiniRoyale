using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace MINI_ROYALE
{
    class TileMap
    {
        public bool mapLoaded = false;
        int[,] mapCoords;
        public Dictionary<Tuple<int,int>, Tile> bitmap = new Dictionary<Tuple<int, int>, Tile>();
        public Camera2D cam;
        Game game;
        public static TileMap instance;
        
        public Camera2D Camera
        {
            get
            {
                return cam;
            }
        }
        
        // Pak het tile object op de opgegeven X en Y
        public Tile getTileOnLoc(int x, int y)
        {
            return bitmap[new Tuple<int, int>(x*16, y*16)];

        }
        // TileMap is een singleton omdat we in verschillende classes bij de tilemap moeten kunnen
        public TileMap(GraphicsDevice gd)
        {
            cam = new Camera2D(gd);
            cam.Zoom = 5; // This seems about right
            loadMap();
            instance = this;
        }
        public void setGameDevice(Game g)
        {
            this.game = g;
        }

        // De draw method van de gehele map
        public void draw(SpriteBatch spriteBatch, Player p)
        {
            // We willen pas beginnen met het drawen van de map wanneer deze geladen is
            if (!mapLoaded) return;
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            Viewport viewport = game.GraphicsDevice.Viewport;

            // Loop door elke tile heen in de bitmap
            foreach (KeyValuePair<Tuple<int,int>, Tile> tile in bitmap)
            {
                // We pakken de coordinaten van de tile en gaan kijken of deze binnen de viewport van de player ligt.
                // We willen tenslotte geen overbodige tiles tekenen.
                Tuple<int,int> coords = tile.Key;
                if(coords.Item1 > p.pos.X - (viewport.Width / 2) && coords.Item1 < p.pos.X + (viewport.Width / 2))
                {
                    if (coords.Item2 > p.pos.Y - (viewport.Height / 2) && coords.Item2 < p.pos.Y + (viewport.Height / 2))
                    {
                        // Bepalen of de tile in de zone is. Zoja, welk stadium en pas hier de kleur op aan
                        Tile currTile = tile.Value;
                        Texture2D tileToUse = game.Content.Load<Texture2D>(currTile.file);
                        if(currTile.inZone == 3)
                        {
                            spriteBatch.Draw(tileToUse, new Vector2(coords.Item1, coords.Item2), Color.Purple);
                        }
                        else if (currTile.inZone == 2)
                        {
                            spriteBatch.Draw(tileToUse, new Vector2(coords.Item1, coords.Item2), Color.LightPink);
                        }
                        else if (currTile.inZone == 1)
                        {
                            spriteBatch.Draw(tileToUse, new Vector2(coords.Item1, coords.Item2), Color.Pink);
                        }
                        else if (currTile.inZone == 0)
                        {
                            spriteBatch.Draw(tileToUse, new Vector2(coords.Item1, coords.Item2), Color.White);
                        }
                    }
                }
            }
            spriteBatch.End();
        }

        // Een magische methode die een 1-dimensionale array van 160.000 getallen omzet naar een 400*400 2-dimensionale array
        // en daarbij ook een Tile object instantiate en meegeeft.
        // De loadMap() functie is async, omdat hij een file moet laden.
        private async void loadMap()
        {
            // Pak de map.txt file uit de assets folder en laad deze
            var localizationDirectory = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile sampleFile = await localizationDirectory.GetFileAsync("map.txt");
            string txt = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

            // Alle 'ruwe' coordinaten omzetten naar een int[] array
            int[] rawCoords = txt.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            int[,] coords = new int[401, 401];
            int currRow = 0, currColumn = 0;

            // Loopen door alle coordinaten om ze vervolgens om de 400 tiles naar een nieuwe row te duwen
            // Op deze manier maken we een 2D array
            for(int i = 0; i < rawCoords.Length; i++)
            {
                coords[currRow, currColumn] = rawCoords[i];
                if(i % 400 == 0)
                {
                    currRow++;
                    currColumn = 0;
                }
                currColumn++;
            }
            mapCoords = coords;
            
            // We loopen door de uiteindelijke array en kennen het Tile object toe
            for (int y = 1; y < 400; y++)
            {
                for(int x = 1; x < 400; x++)
                {
                    Tuple<int, int> c = new Tuple<int, int>(x*16,y*16);
                    
                    bitmap.Add(c, new Tile("environment/" + coords[y, x], coords[y, x], new Vector2(x,y)));
                }
            }

            // De map is nu geladen
            mapLoaded = true;
        }

        // Een willekeurige positie op de map genereren
        public Vector2 getRandomOnMapPosition()
        {
            var result = true;
            var vec2 = new Vector2();
            Random rnd = new Random();
            while(result == true)
            {
                var num1 = rnd.Next(1, 399);
                var num2 = rnd.Next(1, 399);
                var xCoord = (int)(num1/16);
                var yCoord = (int)(num2 /16);
                result = false; // this.getTileOnLoc(xCoord, yCoord).hasCollision;
                vec2 = new Vector2(num1, num1);
            }
            return vec2;

        }
    }
}
