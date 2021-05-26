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

        public List <CollectionDescription> Add { get; set; }

        public List <CollectionDescription> Update { get; set; }

        public List<CollectionDescription> Remove { get; set; }

        public DeltaCD() { }

        public DeltaCD(int transactionId, List<CollectionDescription> add, List<CollectionDescription> update, List<CollectionDescription> remove)
        {
            TransactionId = transactionId;
            Add = add;
            Update = update;
            Remove = remove;
        }
    }
}
