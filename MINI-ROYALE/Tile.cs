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
        public Rectangle collisionbox { get; set; }
        public Vector2 position;
        public byte inZone = 0;
        public Tile(string file, int tileId, Vector2 pos)
        {
            this.position = pos;
            this.file = file;
            switch (tileId)
            {
                case 44: /*
                    
                    case 21:
                    case 1:
                    case 2:
                    case 23:
                    case 24:
                    Hier kunnen meer tiles met collision toegevoegd worden.
                     */
                case 1:
                    hasCollision = true;
                    break;
                default://loopbare tiles
                    hasCollision = false;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zones"></param>
        /// <returns></returns>
        public byte setZone(byte zones)
        {
            inZone = zones;
            return inZone;
        }
       
        /// <summary>
        /// returns the inside of the zone
        /// </summary>
        /// <returns>inZone</returns>
        public byte getZone()
        {
            return inZone;
        }
    }
}
