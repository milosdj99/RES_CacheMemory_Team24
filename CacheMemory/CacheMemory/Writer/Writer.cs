using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.Historical;

namespace CacheMemory.Writer
{
    class Writer
    {
        public Historical.Historical Historical { get; set; }        

        public Writer(Historical.Historical historical)
        {
            Historical = historical;
        }

        public void DataSetSearch(int dataset)
        {
            List<HistoricalProperty> returnList = Historical.DataSetSearch(dataset);

            Console.WriteLine("Code: ||| Id geografskog podrucja: ||| Timestamp: ||| Vrednost: \n");
            foreach (HistoricalProperty hs in returnList)
            {
                Console.WriteLine($"{hs.Code} ||| {hs.Value.IdGeografskogPodrucja} ||| {hs.Value.TimeStamp}: ||| {hs.Value.Potrosnja} ");
            }
        }

        public void CodeSearch(string code)
        {
            List<HistoricalProperty> returnList = Historical.CodeSearch(code);

            Console.WriteLine("Code: ||| Id geografskog podrucja: ||| Timestamp: ||| Vrednost: \n");
            foreach(HistoricalProperty hs in returnList)
            {
                Console.WriteLine($"{hs.Code} ||| {hs.Value.IdGeografskogPodrucja} ||| {hs.Value.TimeStamp}: ||| {hs.Value.Potrosnja} ");
            }
        }

        public void DateSearch(DateTime dateMin, DateTime dateMax)
        {
            List<HistoricalProperty> returnList = Historical.DateSearch(dateMin, dateMax);

            Console.WriteLine("Code: ||| Id geografskog podrucja: ||| Timestamp: ||| Vrednost: \n");
            foreach (HistoricalProperty hs in returnList)
            {
                Console.WriteLine($"{hs.Code} ||| {hs.Value.IdGeografskogPodrucja} ||| {hs.Value.TimeStamp}: ||| {hs.Value.Potrosnja} ");
            }
        }
    }
}
