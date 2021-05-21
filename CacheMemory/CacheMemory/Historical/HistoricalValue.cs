using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Models
{
    class HistoricalValue
    {
        DateTime TimeStamp { get; set; }
        int IdGeografskogPodrucja { get; set; }
        int Potrosnja { get; set; }

        public HistoricalValue(DateTime timeStamp, int idGeografskogPodrucja, int potrosnja)
        {
            TimeStamp = timeStamp;
            IdGeografskogPodrucja = idGeografskogPodrucja;
            Potrosnja = potrosnja;
        }
    }
}
