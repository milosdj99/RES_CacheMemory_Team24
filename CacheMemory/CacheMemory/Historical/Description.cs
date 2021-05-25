using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Historical
{
    class Description
    {
        public int Id { get; set; }
        public int DataSet { get; set; }
        public List<HistoricalProperty> HistoricalProperties { get; set; }

        public Description(int id, int dataSet, List<HistoricalProperty> historicalProperties)
        {
            Id = id;
            DataSet = dataSet;
            HistoricalProperties = historicalProperties;
        }
    }
}
