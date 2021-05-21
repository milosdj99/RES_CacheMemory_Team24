using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Models
{
    class Description
    {
        int Id { get; set; }
        int DataSet { get; set; }
        List<HistoricalProperty> HistoricalProperties { get; set; }

        public Description(int id, int dataSet, List<HistoricalProperty> historicalProperties)
        {
            Id = id;
            DataSet = dataSet;
            HistoricalProperties = historicalProperties;
        }
    }
}
