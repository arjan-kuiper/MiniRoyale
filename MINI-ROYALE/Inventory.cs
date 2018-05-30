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
        public byte addItem(String information)
        {
            // check to see if there is inventory space and if there is to much in the inventorys;
            int check = getSizeOfInv();
            if (check > 5)
            {
                // inventory is overflowed full 
                return 0;
            }
            else if (check == 5)
            {
                // inventory is full 
                return 0;
            }
            else if (check >= 0)
            {
                items.Add(new Item() { information });
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // function to check the size of the list.
        // don't rlly know why...
        public int getSizeOfInv()
        {
            int sizeInv = 0;
            // foreach loop trough the list of items
            foreach (Item needle in items)
            {
                sizeInv++;
            }
            return sizeInv;
        }
    }
}
