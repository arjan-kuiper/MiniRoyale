using Microsoft.Xna.Framework;
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
            float speed = 10f;
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
            moveVel *= speed;
            p.Move(moveVel);
        }

    }
}
