using CacheMemory.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Historical
{
    public class HistoricalProperty : IHistoricalProperty
    {
        public string Code { get; set; }
        public IHistoricalValue Value { get; set; }

        public HistoricalProperty(string code, IHistoricalValue value)
        {
            Code = code;
            Value = value;
        }
    }
}
