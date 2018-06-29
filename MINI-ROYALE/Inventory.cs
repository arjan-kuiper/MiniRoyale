using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Inventory
    {
        // list of Item as items
        private List<Item> items { get; }
        private Player player { get; }
        private Bot bot { get; }

        /// <summary>
        /// making the list Item with a size of 5
        /// </summary>
        /// <param name="p"></param>
        public Inventory(Player p)
        {
            items = new List<Item>(5);
            player = p;
        }

        /// <summary>
        ///  function to add an item to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if added else false</returns>
        public bool addItemToInv(Item item)
        {
            // check to see if there is inventory space and if there is to much in the inventorys;
            if (getSizeOfInv() > 5)
            {
                // inventory is overflowed full 
                return false;
            }
            else if (getSizeOfInv() == 5)
            {
                // inventory is full 
                return false;
            }
            else if (getSizeOfInv() >= 0 && getSizeOfInv() < 5)
            {
                // add item to inv
                items.Add(item);

                item.removeItemFromMap(player.pos);
                return true;
            }
            else
            {
                // something went wrong
                return false;
            }
        }

        /// <summary>
        /// removes the item in inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>return true for removing</returns>
        public bool removeItemFromInv(Item item)
        {
            item.addItemToMap(player.pos, item);
            items.Remove(item);
            return true;
        }

        /// <summary>
        ///  function to check the size of the list.
        /// </summary>
        /// <returns>list count</returns>
        public int getSizeOfInv()
        {
            return items.Count;
        }

        /// <summary>
        /// gets the item in the list
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>item in the slot</returns>
        public Item getItemInSlot(int slot)
        {
            return items.ElementAtOrDefault(slot);
        }

        /// <summary>
        /// gets the item name in the list
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>item names in the list</returns>
        public string getItemName(int slot)
        {
            return items.ElementAt(slot).getName();
        }
    }
}
