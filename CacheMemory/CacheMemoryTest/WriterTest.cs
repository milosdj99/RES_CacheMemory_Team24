using CacheMemory.Writer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.Common;
using CacheMemory.DumpingBuffer;
using Moq;

namespace CacheMemoryTest
{
    [TestFixture]
    class WriterTest
    {
        ILogger log = null;

        [SetUp]
        public void Setup()
        {

            var logerMock = new Mock<ILogger>();
            log = logerMock.Object;
        }

        [Test]
        public void genTimeStamp()
        {
            Write w = new Write();
            Assert.AreEqual(DateTime.Now, w.genTimeStamp());
        }

        [Test]
        public void genPotrosnja()
        {
            Write w = new Write();
            bool pripada = false;
            int vrednost;
            for (int i = 0; i < 10000; i++)
            {
                vrednost = w.genPotrosnja();
                if (vrednost < 1 || vrednost > 1000)
                {
                    pripada = false;
                    break;
                }

                pripada = true;
            }

            Assert.AreEqual(true, pripada);
        }

        [Test]
        public void genCode()
        {
            Write w = new Write();
            string[] kodovi = { "CODE_ANALOG", "CODE_DIGITAL", "CODE_LIMITSET", "CODE_CUSTOM", "CODE_SINGLENODE",
                               "CODE_MULTIPLENODE", "CODE_CONSUMER", "CODE_SOURCE", "CODE_MOTION", "CODE_SENSOR" };
            string kod = "";
            bool pripada = false;
            for (int i = 0; i < 100; i++)
            {
                kod = w.genCode();
                if (kod != kodovi[0] && kod != kodovi[1] && kod != kodovi[2] && kod != kodovi[3] && kod != kodovi[4] && kod != kodovi[5] && kod != kodovi[6] && kod != kodovi[7] && kod != kodovi[8] && kod != kodovi[9])
                {
                    pripada = false;
                    break;
                }
                pripada = true;
            }

            Assert.AreEqual(true, pripada);
        }

        [Test]
        public void genGeoID()
        {
            Write w = new Write();
            bool pripada = false;
            int vrednost;
            for (int i = 0; i < 100; i++)
            {
                vrednost = w.genGeoID();
                if (vrednost < 1 || vrednost > 11)
                {
                    pripada = false;
                    break;
                }

                pripada = true;
            }

            Assert.AreEqual(true, pripada);
        }

        [Test]
        [TestCase(3)]
        public void vrsiUpisUDumpingBuffer(int id)
        {
            Write w = new Write();
            Data d = w.WriteToDumpingBuffer(id, log);

            Assert.AreNotEqual(null, d.Value);
            Assert.AreNotEqual(null, d.Code);
        }


    }
}
