﻿using Microsoft.Xna.Framework;
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

        public static bool hasCollision;
        public string file { get; set; }
        public Tile(string file, int tileId)
        {
            this.file = file;
            switch (tileId)
            {
                case 1: case 2://Tiles met collision DEZE DOORLOPEN!!!
                    hasCollision = true;
                    break;
                default://loopbare tiles
                    hasCollision = false;
                    break;
            }
        }
       
    }
}
