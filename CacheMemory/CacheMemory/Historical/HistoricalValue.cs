using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Historical
{
    class HistoricalValue
    {
        public DateTime TimeStamp { get; set; }
        public int IdGeografskogPodrucja { get; set; }
        public int Potrosnja { get; set; }

        public HistoricalValue(DateTime timeStamp, int idGeografskogPodrucja, int potrosnja)
        {
            TimeStamp = timeStamp;
            IdGeografskogPodrucja = idGeografskogPodrucja;
            Potrosnja = potrosnja;
        }
    }
}
