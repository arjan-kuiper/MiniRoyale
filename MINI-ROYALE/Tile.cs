using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Tile
    {
        Color c;
        public Tile(Color c)
        {
            this.c = c;
        }
        public Color getColor()
        {
            return c;
        }
    }
}
