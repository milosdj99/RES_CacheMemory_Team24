using CacheMemory.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IDescription
    {
         int Tip { get; set; }
         int Id { get; set; }
         int DataSet { get; set; }
         List<HistoricalProperty> HistoricalProperties { get; set; }
    }
}
