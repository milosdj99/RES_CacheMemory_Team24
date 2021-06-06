using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.DumpingBuffer;
using NUnit.Framework;

namespace CacheMemoryTest
{
    [TestFixture]
    class DataTest
    {
        DateTime d = new DateTime(2020, 2, 2);
        Value v = null;
        Value v2 = null;

        [SetUp]
        public void Setup()
        {
            v = new Value(d, 2, 200);
        }

        [Test]
        public void prazanKonstr()
        {
            Data d = new Data();
            Assert.IsNotNull(d);
        }

        [Test]
        [TestCase("CODE_ANALOG")]
        public void konstruktorDobriParamData(string kod)
        {
            Data d = new Data(kod, v);

            Assert.AreEqual(d.Code, kod);
            Assert.AreEqual(d.Value, v);           
        }

        [Test]
        [TestCase(null)]
        public void konstruktorNULLParamData(string kod)
        {
            Data d = new Data(kod, v2);

            Assert.AreEqual(d.Value, null);
            Assert.AreEqual(d.Code, null);
        }

    }
}
