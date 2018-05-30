using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Player
    {
        private Position pos;
        
        
        public Player()
        {
            this.pos = new Position(0, 0);
        }

        public void Move(Vector2 vec)
        {
            pos.x += vec.X;
            pos.y += vec.Y;
            System.Diagnostics.Debug.WriteLine("{0} {1}", pos.x, pos.y);
        }
    }
}
