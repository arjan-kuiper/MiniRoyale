using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    class Armor : Item
    {
        private float armor { get; set; }
        private TimeSpan useTime { get; set; } // ook mogelijk om localtime te gebruiken, nog kijken welke het makkelijkst haalbaar is
    }
}
