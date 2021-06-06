using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemory.Common
{
    public interface ILogger
    {
        string CurrentDirectory { get; set; }
        string FileName { get; set; }
        string FilePath { get; set; }
        void LogMsg(string msg);

    }
}
