using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    // Onze eigen position class die eigenlijk niet meer nodig is. In de onderzoeksfase hebben we besloten om onze
    // eigen Position class te maken waarin we de X en Y coordinaten konden opslaan en ophalen, maar wanneer we eenmaal
    // bezig waren kwamen we erachter dat XNA/MonoGame een Vector2 class heeft.
    class Position
    {
        public float x;
        public float y;
        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// gets the position on the grid field
        /// </summary>
        /// <param name="tileSize"></param>
        /// <returns>position</returns>
        public float[] getGridPosition(float tileSize)
        {
            //Format X,Y in tiles.
            float[] position = { (float)x / tileSize, (float)y / tileSize };
            return position;
        }
    }
}