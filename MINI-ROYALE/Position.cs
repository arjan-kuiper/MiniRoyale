using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Position
    {
        public float x;
        public float y;
        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float[] getGridPosition(float tileSize)
        {
            //Format X,Y in tiles.
            float[] position = { (float)x / tileSize, (float)y / tileSize };
            return position;
        }
    }
}