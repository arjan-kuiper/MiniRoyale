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

        // making the list Item with a size of 5
        public Inventory(Player p)
        {
            items = new List<Item>(5);
            player = p;
        }

        // function to add an item to the inventory
        public bool AddItemToInv(Item item)
        {
            // check to see if there is inventory space and if there is to much in the inventorys;
            if (GetSizeOfInv() > 5)
            {
                // inventory is overflowed full 
                return false;
            }
            else if (GetSizeOfInv() == 5)
            {
                // inventory is full 
                return false;
            }
            else if (GetSizeOfInv() >= 0 && GetSizeOfInv() < 5)
            {
                // add item to inv
                items.Add(item);

                item.RemoveItemFromMap(player.pos);
                return true;
            }
            else
            {
                // something went wrong
                return false;
            }
        }

        public bool RemoveItemFromInv(Item item)
        {
            items.Remove(item);
            return true;
        }

        // function to check the size of the list.
        // don't rlly know why...
        public int GetSizeOfInv()
        {
            return items.Count;
        }

        public Item GetItemInSlot(int slot)
        {
            //System.Diagnostics.Debug.WriteLine(slot);
            return items.ElementAtOrDefault(slot);
        }

        public string GetItemName(int slot)
        {
            return items.ElementAt(slot).getName();
        }
    }
}
