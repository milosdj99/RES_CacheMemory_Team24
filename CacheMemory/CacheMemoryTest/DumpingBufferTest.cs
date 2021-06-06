using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMemory.DumpingBuffer;
using NUnit.Framework;
using Moq;
using CacheMemory.Common;

namespace CacheMemoryTest
{
    [TestFixture]
    class DumpingBufferTest
    {
        CollectionDescription cd = null;
        CollectionDescription cd1 = null;
        CollectionDescription cd2 = null;
        CollectionDescription cd3 = null;
        CollectionDescription cd4 = null;
        CollectionDescription cd5 = null;
        ILogger log = null;
        


        [SetUp]
        public void Setup()
        {
            Mock<ILogger> moq = new Mock<ILogger>();
            log = moq.Object;

            DateTime dt = new DateTime(2020, 2, 2);
            DateTime dt2 = new DateTime(2021, 12, 10);
            DateTime dt3 = new DateTime(2021, 8, 20);

            Value v = new Value(dt, 5, 500);
            Value v2 = new Value(dt2, 8, 935);
            Value v3 = new Value(dt3, 3, 821);

            Data d = new Data("CODE_SOURCE", v);
            Data d2 = new Data("CODE_SOURCE", v2);
            Data d3 = new Data("CODE_CONSUMER", v3);
            Data d4 = new Data("CODE_ANALOG", v3);
            Data d5 = new Data("CODE_CUSTOM", v3);
            Data d6 = new Data("CODE_SINGLENODE", v3);

            cd = new CollectionDescription(5, d);
            cd1 = new CollectionDescription(7, d2); 
            cd2 = new CollectionDescription(2, d3);
            cd3 = new CollectionDescription(7, d4);
            cd4 = new CollectionDescription(5, d5);
            cd5 = new CollectionDescription(2, d6);
          
        }

        [Test]
        public void konstruktorPrazanDB()
        {
            DumpingBuff db = new DumpingBuff();
            Assert.IsNotNull(db.listCD);
        }

        [Test]
        public void proveriNoviCode()
        {
            DumpingBuff db = new DumpingBuff();
            bool netacno = db.ProveriCode(cd);

            Assert.AreEqual(false, netacno);
        }

        [Test]
        public void upisiNovi()
        {
            DumpingBuff db = new DumpingBuff();
            db.UpisiListCD(cd, log);
            int count = db.listCD.Count();

            Assert.AreEqual(count, 1);
        }

        [Test]
        public void upisiSaIstimKodom()
        {
            DumpingBuff db = new DumpingBuff(); 
            db.UpisiListCD(cd, log);        //novi el
            db.UpisiListCD(cd1, log);        //azurira se vr

            int count = db.listCD.Count();

            Assert.AreEqual(count, 1);
        }

        [Test]
        public void upisIstiDataSet()
        {
            DumpingBuff db = new DumpingBuff();
            db.UpisiListCD(cd, log);        //novi el
            db.UpisiListCD(cd2, log);        //isti dataSet, razlicit code

            int count = db.listCD.Count();

            Assert.AreEqual(count, 1);
            int brUSetu;
            foreach (CollectionDescription c in db.listCD)
            {
                brUSetu = c.DumpingPropertyCollection.Count();
                Assert.AreEqual(brUSetu, 2);
            }        
        }

        [Test]
        [TestCase(3)]
        public void pakujDeltaCD(int brtr)
        {
            DumpingBuff db = new DumpingBuff();

            db.UpisiListCD(cd3, log);
            db.UpisiListCD(cd4, log);
            db.UpisiListCD(cd5, log);

            DeltaCD dcd =  db.PakujDCD(brtr, db.listCD, log);

            int add = dcd.Add.Count();
            int up = dcd.Update.Count();
            int rem = dcd.Remove.Count();

            Assert.AreEqual(add, 1);
            Assert.AreEqual(up, 1);
            Assert.AreEqual(rem, 1);

        }
    }
}