using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Models
{
    class HistoricalProperty
    {
        string Code { get; set; }
        HistoricalValue Value { get; set; }

        public HistoricalProperty(string code, HistoricalValue value)
        {
            Code = code;
            Value = value;
        }
    }
}
