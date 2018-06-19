using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class InputHandler
    {
        Player p;
        double interactionRange = .6; // In tiles

        public InputHandler(Player pl)
        {
            p = pl;
        }

        public void walk()
        {
            float speed = 1f;
            bool running = false;
            Vector2 moveVel = Vector2.Zero;
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.Up) || k.IsKeyDown(Keys.W))
            {
                moveVel += new Vector2(0, -1);
            }
            if (k.IsKeyDown(Keys.Down) || k.IsKeyDown(Keys.S))
            {
                moveVel += new Vector2(0, 1);
            }
            if (k.IsKeyDown(Keys.Left) || k.IsKeyDown(Keys.A))
            {
                moveVel += new Vector2(-1, 0);
            }
            if (k.IsKeyDown(Keys.Right) || k.IsKeyDown(Keys.D))
            {
                moveVel += new Vector2(1, 0);
            }
            if (k.IsKeyDown(Keys.LeftShift))
            {
                running = true;
            }

            if (running) speed *= 2;
            moveVel *= speed;
            p.Move(moveVel);
        }

        public void mouseListener()
        {
            MouseState ms = Mouse.GetState();
            if(ms.LeftButton == ButtonState.Pressed)
            {
                p.Shoot();
                
            }
        }

        public void interaction()
        {
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.E))
            {
                foreach(Item item in Game.instance.items)
                {
                    if(Vector2.Distance(p.pos, new Vector2(item.pos.X + 8, item.pos.Y + 8)) < interactionRange * 16)
                    {
                        System.Diagnostics.Debug.WriteLine(item.ToString());
                        if (p.pickup(item))
                        {
                            Game.instance.RemoveItemFromMap(item.pos);
                        }
                        break;
                    }
                }
            }
        }

    }
}
