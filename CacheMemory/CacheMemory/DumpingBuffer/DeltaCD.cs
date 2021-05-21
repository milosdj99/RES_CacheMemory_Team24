using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    class DeltaCD
    {       

        int TransactionId { get; set; }

        CollectionDescription Add { get; set; }

        CollectionDescription Update { get; set; }

        CollectionDescription Remove { get; set; }

        public DeltaCD(int transactionId, CollectionDescription add, CollectionDescription update, CollectionDescription remove)
        {
            TransactionId = transactionId;
            Add = add;
            Update = update;
            Remove = remove;
        }
    }
}
