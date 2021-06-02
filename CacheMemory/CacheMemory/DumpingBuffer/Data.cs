using CacheMemory.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    public class Data : IData
    {
        
        public string Code { get; set; }
        public Value Value { get; set; }

        public Data() { }
        public Data(string code, Value value)
        {
            Code = code;
            Value = value;
        }
    }
}
