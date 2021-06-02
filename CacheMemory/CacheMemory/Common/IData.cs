using CacheMemory.DumpingBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IData
    {
         string Code { get; set; }
         Value Value { get; set; }
    }
}
