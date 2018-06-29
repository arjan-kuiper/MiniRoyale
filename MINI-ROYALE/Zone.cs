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
        private int min = 0;
        private int max = 6401;
        private int radius = 400 * 16;
        private int coords;
        private int controlXCoords = -16;
        private int controlYCoords = -16;

        /// <summary>
        /// draw is a function that draws the tiles on the map on the right position.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: TileMap.instance.cam.GetViewMatrix());
            
            spriteBatch.End();
        }
        
        /// <summary>
        /// getRandomCoordsForZone() is a specific function to get random numbers between 0 and 6401
        /// </summary>
        /// <returns>number</returns>
        public int getRandomCoordsForZone()
        {
            // generate a random number inside the zone, this will be the middle point of the new zone.
            Random rnd = new Random();
            coords = rnd.Next(min, max);
            return coords;
        }

        /// <summary>
        /// The update makes sure the tile color changes over time to stay in sync with the 'zone.'
        /// </summary>
        public void update()
        {
            // control to see if the y and x coords already has a number between 0 and 6401 if not controlXCoords and controlYCoords will be given some random numbers.
            if (controlYCoords < -1)
            {
                controlYCoords = getRandomCoordsForZone();
            }
            if (controlXCoords < -1)
            {
                controlYCoords = getRandomCoordsForZone();
            }

            foreach (KeyValuePair<Tuple<int, int>, Tile> tile in TileMap.instance.bitmap)
            {
                // gets the x and y coords on the map
                float worldX = tile.Key.Item1;
                float worldY = tile.Key.Item2;
                // checks to see if x and y coords is on the map and between the controlXCoords and YCoords with and without the radius if so the zone will be set
                if ((worldX < controlXCoords - radius || worldX > controlXCoords + radius) || (worldY < controlYCoords - radius || worldY > controlYCoords + radius))
                {
                    // The tiles that are out of a specific area are set to the zone.
                    // with multiple stages of the zone it will be more playable
                    if (tile.Value.getZone() == 4)
                    {
                        tile.Value.setZone(5);
                    }
                    else if (tile.Value.getZone() == 3)
                    {
                        tile.Value.setZone(4);
                    }
                    else if (tile.Value.getZone() == 2)
                    {
                        tile.Value.setZone(3);
                    }
                    else if (tile.Value.getZone() == 1)
                    {
                        tile.Value.setZone(2);
                    }
                    else if (tile.Value.getZone() == 0)
                    {
                        tile.Value.setZone(1);
                    }
                    
                }
            }
            // radius is set smaller so the zone will go more smootly to the middle point
            radius -= 2;
        }
    }
}
