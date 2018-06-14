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

        public new void Use()
        {
            InterruptUse();
        }

        public void InterruptUse()
        {
            // check so you have to wait 1 time and not more than ones
            int check = 0;
            // armor default test has te boe replaced with player.armor
            int armor = 0;
            if(check == 0)
            {
                // waiting time
                for (int i = 0; i == 500; i++)
                {
                    // check to 1
                    check = 1;
                    i = 0;
                }
            }
            else if (check == 1)
            {
                // armor increased
                armor = 100;
                // set check to 2
                check = 2;
            }
            
        }
    }
}
