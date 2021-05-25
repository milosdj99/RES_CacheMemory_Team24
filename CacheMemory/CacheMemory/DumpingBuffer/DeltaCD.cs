using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    class DeltaCD
    {

        public int TransactionId { get; set; }

        public CollectionDescription Add { get; set; }

        public CollectionDescription Update { get; set; }

        public CollectionDescription Remove { get; set; }

        public DeltaCD(int transactionId, CollectionDescription add, CollectionDescription update, CollectionDescription remove)
        {
            TransactionId = transactionId;
            Add = add;
            Update = update;
            Remove = remove;
        }
    }
}
