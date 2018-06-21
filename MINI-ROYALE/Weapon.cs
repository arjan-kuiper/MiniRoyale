﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Weapon : Item, Throwable
    {
        private float damage { get; set; }
        private float shotCoolDown { get; set; }
        private enum FiringType { SEMI, FULL }
        private int bulletInClip { get; set; }
        private int maxBulletInClip { get; set; }
        private float spread { get; set; }
        private float recoil { get; set; }
        private Player play { get; }

        public Weapon(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {
            Use();
        }
        private int Reload()
        {
            for(int i = 0; i == 500; i++)
            {
                bulletInClip = maxBulletInClip;
            }
            return bulletInClip;
        }

        private int Fire()
        {
            // something like this?
            // for loop for shots maybe 2 fast 
            for (float i = 0; i < 999; i++)
            {
                if (i == shotCoolDown)
                {
                    i = 0;
                }
                // something for full and semi?? override or??
                if (bulletInClip > 0)
                {
                    // SHOOT
                    // mogelijke schiet functie gaat volgensmij de verkeerde kant op moet vanuit weapon naar player en niet player naar weapon
                    play.Shoot(play.getOrientation());
                }
                else
                {
                    // reload and can't shoot
                    Reload();
                }
            }
            return 0;
        }

        public void ThrowItem()
        {
            // throw function from throw class here.
        }

        public new bool Use()
        {
            Fire();
            return true;
        }
    }
}
