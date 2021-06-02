using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IHistoricalValue
    { 

        DateTime TimeStamp { get; set; }
        int IdGeografskogPodrucja { get; set; }
        int Potrosnja { get; set; }
    }
}

