using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{
    public class Randomizer
    {

        public Randomizer()
        {
            rnd = new Random();
            instance = this;
        }
        Random rnd;

        public static Randomizer instance;
        
        /// <summary>
        /// get a random number between given values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>random number</returns>
        public int randomNumber(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}
