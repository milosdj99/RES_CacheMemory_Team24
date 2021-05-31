using CacheMemory.DumpingBuffer;
using CacheMemory.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    interface IWriter
    {

        DateTime genTimeStamp();

        int genPotrosnja();

        string genCode();

        int genGeoID();

        Data WriteToDumpingBuffer(int brSlanja, Log log);

        Data ManualWriteToDumpingBuffer(Log log);


    }
}
