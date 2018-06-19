using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MINI_ROYALE
{
    // Example of usage:
    // to send create new object and call this class
    // json j  = new j();
    // j.GameStarted = false;
    // j.events = *insert a byte here which can be linked to enums*

    // To receive and deSerialize just call deSerializeJson(string)
    // return will be an object filled with the info


    class jsonParser
    {
        public string serializeJson(object o)
        {
            return serializeObject(o);
        }

        public object deSerializeJson(string s)
        {
            return deSerializeJson(s);
        }
    }
}
