using CacheMemory.DumpingBuffer;
using CacheMemory.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface IDumpingBuffer
    {

        List<CollectionDescription> listCD { get; set; }

        bool ProveriCode(CollectionDescription cd);

        bool ProveriDataSet(CollectionDescription cd);

        int UpisiListCD(CollectionDescription cd, ILogger log);

        DeltaCD PakujDCD(int brtr, List<CollectionDescription> listCD, ILogger log);

    }
}
