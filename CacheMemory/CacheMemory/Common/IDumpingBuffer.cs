using CacheMemory.DumpingBuffer;
using CacheMemory.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    interface IDumpingBuffer
    {
        bool ProveriCode(CollectionDescription cd);

        bool ProveriDataSet(CollectionDescription cd);

        int UpisiListCD(CollectionDescription cd, Log log);

        DeltaCD PakujDCD(int brtr, List<CollectionDescription> listCD, Log log);

    }
}
