using CacheMemory.DumpingBuffer;
using CacheMemory.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    interface IHistoricall
    {

        List<CollectionDescription> CheckData(List<CollectionDescription> list);

        void Recieve(DeltaCD delta);

        void Connect();

        void Disconnect();

        bool CheckDeadBand(int oldValue, int newValue);

        void WriteToBase();

        void Add(Description d);

        void Update(Description d);

        void Remove(Description d);

        List<HistoricalProperty> DataSetSearch(int dataset);

        List<HistoricalProperty> CodeSearch(string code);

        List<HistoricalProperty> DateSearch(DateTime min, DateTime max);
    }
}
