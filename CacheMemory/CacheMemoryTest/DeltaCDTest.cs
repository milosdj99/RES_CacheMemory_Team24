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
    class DeltaCDTest
    {
        List<CollectionDescription> add = new List<CollectionDescription>();
        List<CollectionDescription> update = new List<CollectionDescription>();
        List<CollectionDescription> remove = new List<CollectionDescription>();
        List<CollectionDescription> add1 = null;
        List<CollectionDescription> update1 = null;
        List<CollectionDescription> remove1 = null;

        [SetUp]
        public void Setup()
        {
            DateTime dt = new DateTime(2020, 2, 2);
            DateTime dt2 = new DateTime(2021, 12, 10);
            DateTime dt3 = new DateTime(2021, 8, 20);
            DateTime dt4 = new DateTime(2021, 1, 31);
            DateTime dt5 = new DateTime(2021, 6, 25);
            DateTime dt6 = new DateTime(2021, 7, 14);

            Value v = new Value(dt, 5, 500);
            Value v2 = new Value(dt2, 8, 935);
            Value v3 = new Value(dt3, 3, 821);
            Value v4 = new Value(dt4, 2, 445);
            Value v5 = new Value(dt5, 4, 216);
            Value v6 = new Value(dt6, 1, 777);

            Data d = new Data("CODE_SOURCE", v);
            Data d2 = new Data("CODE_SINGLENODE", v2);
            Data d3 = new Data("CODE_MULTIPLENODE", v3);
            Data d4 = new Data("CODE_CUSTOM", v4);
            Data d5 = new Data("CODE_DIGITAL", v5);
            Data d6 = new Data("CODE_MOTION", v6);

            CollectionDescription c = new CollectionDescription(1, d);
            CollectionDescription c2 = new CollectionDescription(2, d2);
            CollectionDescription c3 = new CollectionDescription(3, d3);
            CollectionDescription c4 = new CollectionDescription(4, d4);
            CollectionDescription c5 = new CollectionDescription(5, d5);
            CollectionDescription c6 = new CollectionDescription(6, d6);

            add.Add(c);
            add.Add(c3);
            add.Add(c2);
            update.Add(c4);
            update.Add(c5);
            remove.Add(c6);        
        }

        [Test]
        [TestCase(2)]
        public void konstruktorDCDdobriParam(int i)
        {
            DeltaCD dcd = new DeltaCD(i, add, update, remove);

            int brAdd = add.Count; 
            int brUpd = update.Count;
            int brRem = remove.Count;

            Assert.AreEqual(dcd.TransactionId, i);
            Assert.AreEqual(brAdd, 3);
            Assert.AreEqual(brUpd, 2);
            Assert.AreEqual(brRem, 1);
        }

        [Test]
        [TestCase(null)]
        public void konstruktorDCDnullParam(int i)
        {
            DeltaCD dcd = new DeltaCD(i, add1, update1, remove1);


            Assert.AreEqual(dcd.TransactionId, 0);
            Assert.AreEqual(dcd.Add, null);
            Assert.AreEqual(dcd.Remove, null);
            Assert.AreEqual(dcd.Update, null);
        }

    }
}
