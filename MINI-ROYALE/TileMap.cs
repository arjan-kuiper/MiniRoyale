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
        Dictionary<int[], Tile> bitmap = new Dictionary<int[], Tile>();
        private Camera2D cam;
        Game game;
        
        
        public Camera2D Camera
        {
            get
            {
                return cam;
            }
        }
        public TileMap(GraphicsDevice gd)
        {
            cam = new Camera2D(gd);
            cam.Zoom = 5; // This seems about right
            loadMap();
        }
        public void setGameDevice(Game g)
        {
            this.game = g;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (!mapLoaded) return;
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            foreach (KeyValuePair<int[], Tile> tile in bitmap)
            {
                int[] coords = tile.Key;
                Tile currTile = tile.Value;
                Texture2D tileToUse = game.Content.Load<Texture2D>("environment/1");
                
                spriteBatch.Draw(tileToUse, new Vector2(coords[0], coords[1]), Color.White);
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
                    int[] c = new int[] { x * 16, y * 16};
                    bitmap.Add(c, new Tile("environment/" + coords[y, x], false));
                }
            }

            mapLoaded = true;
        }
    }
}
