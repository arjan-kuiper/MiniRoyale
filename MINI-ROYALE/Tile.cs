using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Tile
    {

        public bool hasCollision;
        public string file { get; set; }
        public Rectangle Collisionbox { get; set; }
        public Vector2 position;
        public byte inZone = 0;
        public Tile(string file, int tileId, Vector2 pos)
        {
            this.position = pos;
            this.file = file;
            switch (tileId)
            {
                case 44: /*case 21:
                case 1:
                case 2:
                case 23:
                case 24:*///Tiles met collision DEZE DOORLOPEN!!!
                    hasCollision = true;
                    
                    break;
                default://loopbare tiles
                    hasCollision = false;
                    break;
            }
        }

        public byte SetZone(byte zones)
        {
            inZone = zones;
            return inZone;
        }
       
        public byte getZone()
        {
            return inZone;
        }
    }
}
