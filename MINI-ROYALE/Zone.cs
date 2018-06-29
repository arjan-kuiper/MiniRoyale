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
        private int radius = 400 * 16;
        private int Coords;
        private int controlXCoords = -16;
        private int controlYCoords = -16;

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
            return Coords;
        }

        public void update()
        {
            // elke minuut

            if (controlYCoords < -1)
            {
                controlYCoords = GetRandomCoordsForZone();
            }
            if (controlXCoords < -1)
            {
                controlYCoords = GetRandomCoordsForZone();
            }

            foreach (KeyValuePair<Tuple<int, int>, Tile> tile in TileMap.instance.bitmap)
            {
                Vector2 tilePos = tile.Value.position;
                float worldX = tile.Key.Item1;
                float worldY = tile.Key.Item2;

                if ((worldX < controlXCoords - radius || worldX > controlXCoords + radius) || (worldY < controlYCoords - radius || worldY > controlYCoords + radius))
                {
                    // Tile is in zone

                    if (tile.Value.getZone() == 4)
                    {
                        tile.Value.SetZone(5);
                    }
                    else if (tile.Value.getZone() == 3)
                    {
                        tile.Value.SetZone(4);
                    }
                    else if (tile.Value.getZone() == 2)
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
            radius -= 2;
        }
    }
}
