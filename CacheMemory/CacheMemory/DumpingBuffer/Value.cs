using CacheMemory.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    public class Value : IValue
    {
       
        public DateTime TimeStamp { get; set; }
        public int IdGeografskogPodrucja { get; set; }
        public int Potrosnja { get; set; }

        public Value(DateTime timeStamp, int idGeografskogPodrucja, int potrosnja)
        {
            TimeStamp = timeStamp;
            IdGeografskogPodrucja = idGeografskogPodrucja;
            Potrosnja = potrosnja;
        }
    }
}
