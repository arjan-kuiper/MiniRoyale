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
        private List<Item> items;

        // making the list Item with a size of 5
        public Inventory()
        {
            items = new List<Item>(5);
        }

        // function to add an item to the inventory
        public bool addItem(Item item)
        {
            // check to see if there is inventory space and if there is to much in the inventorys;
            int itemCount = items.Count;
            if (itemCount > 5)
            {
                // inventory is overflowed full 
                return false;
            }
            else if (itemCount == 5)
            {
                // inventory is full 
                return false;
            }
            else if (itemCount >= 0)
            {
                //items.Add(new Item() { information });
                return true;
            }
            else
            {
                return false;
            }
        }

        // function to check the size of the list.
        // don't rlly know why...
        public int getSizeOfInv()
        {
            return items.Count;
        }
    }
}
