using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Zone
    {
        private Texture2D sprite { get; set; }
        public Vector2 pos;
        private int min = 0;
        private int max = 6401;
        private int radius = 250 * 16;

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: TileMap.instance.cam.GetViewMatrix());

            // Need rectangle stuff so we can resize it
            //spriteBatch.Draw(sprite, new Rectangle((int)pos.X, (int)pos.Y, xcoord, ycoord), new Rectangle(4, 4, sprite.Width, sprite.Height), Color.Purple);
            update(GetRandomCoordsForZone(), GetRandomCoordsForZone());
            spriteBatch.End();
        }
        
        public int GetRandomCoordsForZone()
        {
            Random rnd = new Random();
            int Coords = rnd.Next(min, max);
            min = Coords - radius;
            max = Coords + radius;
            return Coords;
        }

        public void update(int x, int y)
        {
            // elke minuut

            foreach (KeyValuePair<Tuple<int, int>, Tile> tile in TileMap.instance.bitmap)
            {
                Vector2 tilePos = tile.Value.position;
                float worldX = tile.Key.Item1;
                float worldY = tile.Key.Item2;

                if ((worldX < x - radius || worldX > x + radius) || (worldY < y - radius || worldY > y + radius))
                {
                    // Tile is in zone
                    tile.Value.SetZone(true);
                    
                }
            }
            radius /= 2;
        }
    }
}
