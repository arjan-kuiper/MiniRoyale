using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Explosive : Item
    {
        private enum type { IRREGULAR, CONTROLLED, PREFORMED }
        private int explosionTime { get; set; }
        private int blastRadius { get; set; }
    }
}
