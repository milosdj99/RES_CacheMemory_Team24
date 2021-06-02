using CacheMemory.DumpingBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface ICollectionDescription
    {
         int Id { get; set; }

         int DataSet { get; set; }

         List<Data> DumpingPropertyCollection { get; set; }
    }
}
