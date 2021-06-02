using CacheMemory.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Historical
{
    public class HistoricalValue : IHistoricalValue
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
