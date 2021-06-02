using CacheMemory.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IHistoricalProperty
    {
         string Code { get; set; }
         HistoricalValue Value { get; set; }
    }
}
