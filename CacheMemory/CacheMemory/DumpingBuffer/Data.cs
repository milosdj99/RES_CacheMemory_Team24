using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Models
{
    class Data
    {
        
        string Code { get; set; }
        Value Value { get; set; }

        public Data(string code, Value value)
        {
            Code = code;
            Value = value;
        }
    }
}
