using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CacheMemory.Common;

namespace CacheMemory.Logger
{
    public class Log : ILogger
    {
        public string CurrentDirectory { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public Log()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "Log.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;
        }

        public void LogMsg(string msg)
        {
            using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
            {
                w.Write("\n Log entry:");
                w.WriteLine("{0} , {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine(msg);
                w.WriteLine("--------------------------------------------------------");
            }
        }
    }
}
