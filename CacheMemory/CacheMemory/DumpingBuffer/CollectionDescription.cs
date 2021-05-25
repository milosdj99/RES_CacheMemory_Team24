using CacheMemory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    class CollectionDescription
    {
        

        public int Id { get; set; }

        public int DataSet { get; set; }

        public List<Data> DumpingPropertyCollection { get; set; }

        public CollectionDescription(int id, int dataSet, List<Data> dumpingPropertyCollection)
        {
            Id = id;
            DataSet = dataSet;
            DumpingPropertyCollection = dumpingPropertyCollection;
        }
    }
}
