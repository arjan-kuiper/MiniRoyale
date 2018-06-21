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
                if(p.moveDirs[0]!= false)
                {
                    moveVel += new Vector2(0, -1);
                }
                
            }
            if (k.IsKeyDown(Keys.Down) || k.IsKeyDown(Keys.S))
            {
                if (p.moveDirs[2] != false) {
                    moveVel += new Vector2(0, 1);
                }
                    
            }
            if (k.IsKeyDown(Keys.Left) || k.IsKeyDown(Keys.A))
            {
                if (p.moveDirs[3] != false) { moveVel += new Vector2(-1, 0); }
                    
            }
            if (k.IsKeyDown(Keys.Right) || k.IsKeyDown(Keys.D))
            {
                if (p.moveDirs[1] != false)
                {
                    moveVel += new Vector2(1, 0);
                }
                
                
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
                p.shoot();
                
            }
        }

    }
}
