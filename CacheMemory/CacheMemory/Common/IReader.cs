using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    interface IReader
    {

        void DataSetSearch(int dataset);

        void CodeSearch(string code);

        void DateSearch(DateTime dateMin, DateTime dateMax);
    }
}
