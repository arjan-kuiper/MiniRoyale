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
        bool mapLoaded = false;
        int[,] mapCoords;
        public Dictionary<Tuple<int,int>, Tile> bitmap = new Dictionary<Tuple<int, int>, Tile>();
        private Camera2D cam;
        Game game;
        public static TileMap instance;
        
        public Camera2D Camera
        {
            get
            {
                return cam;
            }
        }
        

        public Tile getTileOnLoc(int x, int y)
        {
            return bitmap[new Tuple<int,int>(x,y)];

        }
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
        public void draw(SpriteBatch spriteBatch, Player p)
        {
            if (!mapLoaded) return;
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            Viewport viewport = game.GraphicsDevice.Viewport;

            foreach (KeyValuePair<Tuple<int,int>, Tile> tile in bitmap)
            {
                Tuple<int,int> coords = tile.Key;
                // Only draw visible tiles
                if(coords.Item1 > p.pos.X - (viewport.Width / 2) && coords.Item1 < p.pos.X + (viewport.Width / 2))
                {
                    if (coords.Item2 > p.pos.Y - (viewport.Height / 2) && coords.Item2 < p.pos.Y + (viewport.Height / 2))
                    {
                        Tile currTile = tile.Value;
                        Texture2D tileToUse = game.Content.Load<Texture2D>(currTile.file);

                        spriteBatch.Draw(tileToUse, new Vector2(coords.Item1, coords.Item2), Color.White);
                    }
                }
            }
            spriteBatch.End();
        }

        private async void loadMap()
        {
            var localizationDirectory = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile sampleFile = await localizationDirectory.GetFileAsync("map.txt");
            string txt = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

            int[] rawCoords = txt.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            int[,] coords = new int[401, 401];
            int currRow = 0, currColumn = 0;
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
            
            for (int y = 1; y < 400; y++)
            {
                for(int x = 1; x < 400; x++)
                {
                    Tuple<int, int> c = new Tuple<int, int>(x*16,y*16);
                    
                    bitmap.Add(c, new Tile("environment/" + coords[y, x], coords[y, x]));
                }
            }

            mapLoaded = true;
        }
    }
}
