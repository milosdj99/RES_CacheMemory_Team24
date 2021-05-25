using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Historical
{
    class HistoricalProperty
    {
        public string Code { get; set; }
        public HistoricalValue Value { get; set; }

        public HistoricalProperty(string code, HistoricalValue value)
        {
            Code = code;
            Value = value;
        }
    }
}
