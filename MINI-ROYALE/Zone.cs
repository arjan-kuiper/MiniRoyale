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
        private int Coords;

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: TileMap.instance.cam.GetViewMatrix());

            // Need rectangle stuff so we can resize it
            //spriteBatch.Draw(sprite, new Rectangle((int)pos.X, (int)pos.Y, xcoord, ycoord), new Rectangle(4, 4, sprite.Width, sprite.Height), Color.Purple);
            //update(GetRandomCoordsForZone(), GetRandomCoordsForZone());
            spriteBatch.End();
        }
        
        // Genereer een willekeurig getal binnen de huidige zone wat het middelpunt gaat worden van de nieuwe zone
        public int GetRandomCoordsForZone()
        {
            Random rnd = new Random();
            Coords = rnd.Next(min, max);
            min = Coords - radius;
            max = Coords + radius;
            if (min < 0)
            {
                min = 0;
            }
            if (max > 6401)
            {
                max = 6401;
            }
            return Coords;
        }

        // Elke keer dat deze update functie wordt aangeroepen wordt de zone verkleint met als middelpunt
        // de meegegeven x en y coordinaten.
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

                    if (tile.Value.getZone() == 2)
                    {
                        tile.Value.SetZone(3);
                    }
                    else if (tile.Value.getZone() == 1)
                    {
                        tile.Value.SetZone(2);
                    }
                    else if (tile.Value.getZone() == 0)
                    {
                        tile.Value.SetZone(1);
                    }
                    
                }
            }
            radius /= 2;
        }
    }
}
