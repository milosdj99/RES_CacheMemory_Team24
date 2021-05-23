using CacheMemory.Dumping_Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheMemory.Writer
{
    class Write
    {
        Random r = new Random();

        public Write() { }

        public DateTime genTimeStamp()
        {           
            return DateTime.Now;
        }
        public int genPotrosnja()
        {
            return r.Next(1, 1000);
        }

        public string genCode()
        {
            string[] kodovi = { "CODE_ANALOG", "CODE_DIGITAL", "CODE_LIMITSET", "CODE_CUSTOM", "CODE_SINGLENODE",
                               "CODE_MULTIPLENODE", "CODE_CONSUMER", "CODE_SOURCE", "CODE_MOTION", "CODE_SENSOR" };
         
            return kodovi[r.Next(10)];
        }

        public int genGeoID()
        {
            return r.Next(1, 11);
        }

        public  Data WriteToDumpingBuffer(int brSlanja)
        {
                brSlanja++;

                DateTime ts = genTimeStamp();
                int p = genPotrosnja();
                int geoID = genGeoID();
                string code = genCode();

                Value v = new Value(ts, geoID, p);
                Data d = new Data(code, v);

            return d;
              
        }
        
        public  void ManualWriteToDumpingBuffer()
        {
            DateTime ts ;
            int p ;
            int geoID ;
            string code ;

            Console.WriteLine("Unesite sledece podetke: ");
            Console.WriteLine("Unesite potrosnju: ");
            p = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite ID geografskog podrucja: ");
            geoID = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite jedan od sledecih kodova: ");
            Console.WriteLine("CODE_ANALOG, CODE_DIGITAL, CODE_LIMITSET, CODE_CUSTOM, CODE_SINGLENODE,");
            Console.WriteLine("CODE_MULTIPLENODE, CODE_CONSUMER, CODE_SOURCE, CODE_MOTION, CODE_SENSOR");
            code = Console.ReadLine();
            Console.WriteLine("Unesite datum i vreme (npr. 10/22/2017 02:30:52)");
            ts = DateTime.Parse(Console.ReadLine());

            Value v = new Value(ts, geoID, p);
            Data d = new Data(code, v);
        }
    }
}
