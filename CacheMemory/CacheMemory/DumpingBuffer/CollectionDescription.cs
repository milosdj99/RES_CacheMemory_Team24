﻿using CacheMemory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    class CollectionDescription
    {
        

        int Id { get; set; }

        int DataSet { get; set; }

        List<Data> DumpingPropertyCollection { get; set; }

        public CollectionDescription(int id, int dataSet, List<Data> dumpingPropertyCollection)
        {
            Id = id;
            DataSet = dataSet;
            DumpingPropertyCollection = dumpingPropertyCollection;
        }
    }
}
