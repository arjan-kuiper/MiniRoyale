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
        public Position pos;
        private bool alive;
        private byte hp;
        private byte armor;
        private Inventory inventory;
        private byte currentItem;
        private float orientation;
        private int team;


        public Player()
        {
            this.pos = new Position(0, 0);
            inventory = new Inventory();
        }

        public void Move(Vector2 vec)
        {
            this.pos.x += vec.X;
            pos.y += vec.Y;
            //System.Diagnostics.Debug.WriteLine("{0} {1}", pos.x, pos.y);
        }
        

        public int pickup(Item item)
        {
            inventory.addItem(item);
            return 0;
        }

        public Item dropItem(int slot)
        {
            // TODO
            
            return null;
        }

        public byte takeDamage(byte damage)
        {
            // TODO
            return 0;
        }

        public byte increaseHealth(byte amount)
        {
            // TODO
            return 0;
        }

        public byte increaseArmor(byte amount)
        {
            // TODO
            return 0;
        }

        public Throwable throwItem(byte slot)
        {
            // TODO
            return null;
        }

        public Item dropItem(byte slot)
        {
            // TODO
            return null;
        }

        public void update()
        {
            // TODO
        }


        private float shoot(Position pos, float orientation)
        {
            // TODO
            return 0;
        }

        private bool checkCollision(Position pos)
        {
            // TODO
            return true;
        }
    }
}
