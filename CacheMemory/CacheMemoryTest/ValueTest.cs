using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CacheMemory.DumpingBuffer;


namespace CacheMemoryTest
{
    [TestFixture]
    class ValueTest
    {
        DateTime d;
        [SetUp]
        public void Setup()
        {
            DateTime d = new DateTime(2021, 2, 2);
        }

        [Test]
        [TestCase(5, 125)]
        [TestCase(6, 102)]
        [TestCase(null, 102)]
        [TestCase(5, null)]
        public void konstruktorValue(int idGeografskogPodrucja, int potrosnja)
        {
            Value v = new Value(d, idGeografskogPodrucja, potrosnja);

            Assert.AreEqual(v.IdGeografskogPodrucja, idGeografskogPodrucja);
            Assert.AreEqual(v.Potrosnja, potrosnja);
            Assert.AreEqual(v.TimeStamp, d);
        }

        

    }
}
