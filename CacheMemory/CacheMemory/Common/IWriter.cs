using CacheMemory.DumpingBuffer;
using CacheMemory.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IWriter
    {       

        DateTime genTimeStamp();

        int genPotrosnja();

        string genCode();

        int genGeoID();

        Data WriteToDumpingBuffer(int brSlanja, ILogger log);

        Data ManualWriteToDumpingBuffer(ILogger log);


    }
}
