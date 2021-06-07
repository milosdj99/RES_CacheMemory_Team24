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
using System.Diagnostics.CodeAnalysis;


namespace CacheMemory.Writer
{
    public class Write : IWriter
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

        public Data WriteToDumpingBuffer(int brSlanja, ILogger log)
        {
            brSlanja++;

            DateTime ts = genTimeStamp();
            int p = genPotrosnja();
            int geoID = genGeoID();
            string code = genCode();

            Value v = new Value(ts, geoID, p);
            Data d = new Data(code, v);

            log.LogMsg("Poslati podaci DumpingBufferu. BrSlanja: " + brSlanja);

            return d;

        }

        [ExcludeFromCodeCoverage]
        public Data ManualWriteToDumpingBuffer(ILogger log)
        {
            DateTime ts;
            int p;
            int geoID;
            string code;

            Console.WriteLine("Unesite sledece podatke: ");
            Console.WriteLine("Unesite potrosnju: ");
            p = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite ID geografskog podrucja: ");
            geoID = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite jedan od sledecih kodova: ");
            Console.WriteLine("CODE_ANALOG, CODE_DIGITAL, CODE_LIMITSET, CODE_CUSTOM, CODE_SINGLENODE,");
            Console.WriteLine("CODE_MULTIPLENODE, CODE_CONSUMER, CODE_SOURCE, CODE_MOTION, CODE_SENSOR");
            code = Console.ReadLine();


            bool danUspesno, mesecUspesno, godinaUspesno;
            int dan, mesec, godina;

            Console.WriteLine("Unesite datum:");

            Console.WriteLine("Dan:");
            danUspesno = Int32.TryParse(Console.ReadLine(), out dan);
            Console.WriteLine("Mesec:");
            mesecUspesno = Int32.TryParse(Console.ReadLine(), out mesec);
            Console.WriteLine("Godina:");
            godinaUspesno = Int32.TryParse(Console.ReadLine(), out godina);

            if (danUspesno == false || mesecUspesno == false || godinaUspesno == false)
            {
                throw new Exception();
            }

            if (dan <1 || dan > 31)
            {
                throw new Exception();
            }

            if (mesec < 1 || mesec > 12)
            {
                throw new Exception();
            }

            if (godina < 1950 || mesec > 2021)
            {
                throw new Exception();
            }


            ts = new DateTime(godina, mesec, dan);
            
            Value v = new Value(ts, geoID, p);
            Data d = new Data(code, v);

            log.LogMsg("Rucno uneti podaci za DumpingBuffer");

            return d;
      
        }
    }
}
