using CacheMemory.DumpingBuffer;
using CacheMemory.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CacheMemory.Historical;
using CacheMemory.Common;

namespace CacheMemory.Writer
{
    class Write : IWriter
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

        public  Data WriteToDumpingBuffer(int brSlanja, Log log)
        {
                brSlanja++;

                DateTime ts = genTimeStamp();
                int p = genPotrosnja();
                int geoID = genGeoID();
                string code = genCode();

                Value v = new Value(ts, geoID, p);
                Data d = new Data(code, v);

                log.LogMsg("Poslati podaci DumpingBufferu. BrSlanja: "+ brSlanja);

            return d;
              
        }
        
        public  Data ManualWriteToDumpingBuffer(Log log)
        {
            DateTime ts ;
            int p ;
            int geoID ;
            string code ;

            Console.WriteLine("Unesite sledece podatke: ");
            Console.WriteLine("Unesite potrosnju: ");
            p = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite ID geografskog podrucja: ");
            geoID = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite jedan od sledecih kodova: ");
            Console.WriteLine("CODE_ANALOG, CODE_DIGITAL, CODE_LIMITSET, CODE_CUSTOM, CODE_SINGLENODE,");
            Console.WriteLine("CODE_MULTIPLENODE, CODE_CONSUMER, CODE_SOURCE, CODE_MOTION, CODE_SENSOR");
            code = Console.ReadLine();
            
            Console.WriteLine("Unesite datum i vreme (npr. 10/22/2017 02:30:52)");
            
            Console.WriteLine("Dan:");
            int dan = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Mesec:");
            int mesec = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Godina:");
            int godina = Int32.Parse(Console.ReadLine());

            ts = new DateTime(godina, mesec, dan);
            

            Value v = new Value(ts, geoID, p);
            Data d = new Data(code, v);

            log.LogMsg("Rucno uneti podaci za DumpingBuffer");

            return d;
      
        }
    }
}
