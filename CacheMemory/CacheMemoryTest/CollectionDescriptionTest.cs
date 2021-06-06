using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.DumpingBuffer;

namespace CacheMemoryTest
{
    [TestFixture]
    class CollectionDescriptionTest
    {
        Data d = null;
        Data d2 = null;

        [SetUp]
        public void Setup()
        {
            DateTime dt = new DateTime(2020, 2, 2);
            Value v = new Value(dt, 5, 500);
            d2 = new Data("CODE_MOTION", v);
            d = new Data("CODE_SOURCE", v);
        }


        [Test]
        [TestCase(5)]
        public void konstruktorDobriParamCD_1 (int id)
        {
            CollectionDescription cd = new CollectionDescription(id, d2);

            Assert.AreEqual(cd.Id, id);
            Assert.Contains(d2, cd.DumpingPropertyCollection);
            Assert.AreEqual(cd.DataSet, 5);
        }

        [Test]
        public void prazanKonstr()
        {
            CollectionDescription cd = new CollectionDescription();
            Assert.IsNotNull(cd);
        }

        [Test]
        [TestCase(5)]
        public void konstruktorDobriParamCD_2(int id)
        {
            CollectionDescription cd = new CollectionDescription(id, d);

            Assert.AreEqual(cd.Id, id);
            Assert.Contains(d, cd.DumpingPropertyCollection);
            Assert.AreEqual(cd.DataSet, 4);
        }

        [Test]
        [TestCase(null)]
        public void konstruktorNullparam(int id)
        {
            CollectionDescription cd = new CollectionDescription(id, d2);
            Assert.AreEqual(cd.Id, 0);
        }

    }
}
