using CacheMemory.Common;
using CacheMemory.DumpingBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.DumpingBuffer
{
    public class CollectionDescription : ICollectionDescription
    {
        public int Id { get; set; }

        public int DataSet { get; set; }

        public List<Data> DumpingPropertyCollection { get; set; }
        public CollectionDescription() { }

        public CollectionDescription(int id, Data data)
        {
            DumpingPropertyCollection = new List<Data>();

            Id = id; //koristice se rbr slanja da bi bio unikatan
            string code = data.Code;

            if (code == "CODE_ANALOG" || code == "CODE_DIGITAL")
            {
                DataSet = 1;
            }
            else if (code == "CODE_CUSTOM" || code == "CODE_LIMITSET")
            {
                DataSet = 2;
            }
            else if (code == "CODE_SINGLENODE" || code == "CODE_MULTIPLENODE")
            {
                DataSet = 3;
            }
            else if (code == "CODE_CONSUMER" || code == "CODE_SOURCE")
            {
                DataSet = 4;
            }
            else
            {
                DataSet = 5;
            }

          
            DumpingPropertyCollection.Add(data);
        }


    }
}
