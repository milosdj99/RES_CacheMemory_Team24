using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Historical
{
    class Description
    {
        public int Tip { get; set; }
        public int Id { get; set; }
        public int DataSet { get; set; }
        public List<HistoricalProperty> HistoricalProperties { get; set; }

        public Description(int tip, int id, int dataSet, List<HistoricalProperty> historicalProperties)
        {
            Tip = tip;
            Id = id;
            DataSet = dataSet;
            HistoricalProperties = historicalProperties;
        }
    }
}
