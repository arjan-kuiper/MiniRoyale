using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Armor : Item
    {
        private float armor { get; set; }
        private TimeSpan useTime { get; set; } // ook mogelijk om localtime te gebruiken, nog kijken welke het makkelijkst haalbaar is

        public Armor(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {

        }
    }
}
