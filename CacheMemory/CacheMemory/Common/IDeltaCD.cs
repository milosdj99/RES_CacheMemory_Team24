using CacheMemory.DumpingBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IDeltaCD
    {
         int TransactionId { get; set; }

         List<CollectionDescription> Add { get; set; }

         List<CollectionDescription> Update { get; set; }

         List<CollectionDescription> Remove { get; set; }
    }
}
