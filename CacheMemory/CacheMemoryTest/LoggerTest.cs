using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.Logger;
using NUnit.Framework;
using Moq;
using System.IO;
using CacheMemory.Common;

namespace CacheMemoryTest
{
    [TestFixture]
    class LoggerTest
    {
        ILogger log;

        [Test]
        public void prazanKonstruktorLoger()
        {
            string curDir = Directory.GetCurrentDirectory();
            string name = "Log.txt";
            string putanja = curDir + "/" + name;

            Log loger = new Log();

            string c = loger.CurrentDirectory;
            string n = loger.FileName;
            string p = loger.FilePath;

            bool cc = false;
            bool nn = false;
            bool pp = false;

            if(c == curDir)
            {
                cc = true;
            }

            if(n == name)
            {
                nn = true;
            }

            if(p == putanja)
            {
                pp = true;
            }

            Assert.AreEqual(true, cc);
            Assert.AreEqual(true, nn);
            Assert.AreEqual(true, pp);
        }

    }
}
