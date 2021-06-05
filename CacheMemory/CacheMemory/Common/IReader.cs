using CacheMemory.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IReader
    {
        IHistoricall Historical { get; set; }

        void DataSetSearch(int dataset);

        void CodeSearch(string code);

        void DateSearch(DateTime dateMin, DateTime dateMax);
    }
}
