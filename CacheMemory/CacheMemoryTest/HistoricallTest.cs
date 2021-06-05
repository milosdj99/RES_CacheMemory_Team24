using CacheMemory.Common;
using CacheMemory.DumpingBuffer;
using CacheMemory.Historical;
using CacheMemory.Logger;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemoryTest
{
    [TestFixture]
    class HistoricallTest
    {
        
        ILogger logger = null;
        List<CollectionDescription> listForCheckDataTest = null;
        Historicall hs = null;
        DeltaCD delta = null;

        [SetUp]
        public void Setup()
        {
            
            hs = new Historicall(logger);
            Value v1 = new Value(DateTime.Now, 1, 1);
            Value v2 = new Value(DateTime.Now, 2, 2);
            Value v3 = new Value(DateTime.Now, 3, 3);
            Value v4 = new Value(DateTime.Now, 4, 4);
            Value v5 = new Value(DateTime.Now, 5, 5);

            Data d1 = new Data("CODE_ANALOG", v1);
            Data d2 = new Data("CODE_CUSTOM", v2);
            Data d3 = new Data("CODE_SINGLENODE", v3);
            Data d4 = new Data("CODE_CONSUMER", v4);
            Data d5 = new Data("CODE_MOTION", v5);

            CollectionDescription c1 = new CollectionDescription(1, d1);
            CollectionDescription c2 = new CollectionDescription(1, d2);
            CollectionDescription c3 = new CollectionDescription(1, d3);
            CollectionDescription c4 = new CollectionDescription(1, d4);
            CollectionDescription c5 = new CollectionDescription(1, d5);

            listForCheckDataTest = new List<CollectionDescription>() { c1, c2, c3, c4, c5 };

            List<CollectionDescription> Add = new List<CollectionDescription>() { c1, c2 };
            List<CollectionDescription> Update = new List<CollectionDescription>() { c3, c4 };
            List<CollectionDescription> Remove = new List<CollectionDescription>() { c5 };

            delta = new DeltaCD(1, Add, Update, Remove);

            

        }

        [Test]
        public void KonstruktorDobarParametarTest()
        {
            Mock<ILogger> moq = new Mock<ILogger>();

            logger = moq.Object;

            Historicall hs = new Historicall(logger);

            Assert.AreEqual(hs.Logger, logger);
        }

        [Test]
        public void KonstruktorNullParametarTest()
        {
            Historicall hs = new Historicall(null);

            Assert.AreEqual(hs.Logger, null);
        }

        [Test]
        public void CheckDataDobraListaTest()
		{
            List<CollectionDescription> ret = hs.CheckData(listForCheckDataTest);

            Assert.AreEqual(ret, listForCheckDataTest);
		}

        [Test]
        public void RecieveTest()
		{
            hs.Recieve(delta);

            ////ADD
            ///
            List<CollectionDescription> listaDolazna = delta.Add;

            List<Description> listaPrepakovana = new List<Description>();
            foreach (Description d in hs.LD)
            {
                if (d.Tip == 1)
                {
                    listaPrepakovana.Add(d);
                }
            }

            for(int i = 0; i < listaDolazna.Count; i++)
			{
                Assert.AreEqual(listaDolazna[i].Id, listaPrepakovana[i].Id);
                Assert.AreEqual(listaDolazna[i].DataSet, listaPrepakovana[i].DataSet);

                for(int j = 0; j < listaDolazna[i].DumpingPropertyCollection.Count; j++)
				{
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Code, listaPrepakovana[i].HistoricalProperties[j].Code);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.IdGeografskogPodrucja, listaPrepakovana[i].HistoricalProperties[j].Value.IdGeografskogPodrucja);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.Potrosnja, listaPrepakovana[i].HistoricalProperties[j].Value.Potrosnja);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.TimeStamp, listaPrepakovana[i].HistoricalProperties[j].Value.TimeStamp);
				}
			}

            //// UPDATE
            ///
            listaDolazna = delta.Update;

            listaPrepakovana = new List<Description>();
            foreach (Description d in hs.LD)
            {
                if (d.Tip == 2)
                {
                    listaPrepakovana.Add(d);
                }
            }

            for (int i = 0; i < listaDolazna.Count; i++)
            {
                Assert.AreEqual(listaDolazna[i].Id, listaPrepakovana[i].Id);
                Assert.AreEqual(listaDolazna[i].DataSet, listaPrepakovana[i].DataSet);

                for (int j = 0; j < listaDolazna[i].DumpingPropertyCollection.Count; j++)
                {
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Code, listaPrepakovana[i].HistoricalProperties[j].Code);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.IdGeografskogPodrucja, listaPrepakovana[i].HistoricalProperties[j].Value.IdGeografskogPodrucja);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.Potrosnja, listaPrepakovana[i].HistoricalProperties[j].Value.Potrosnja);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.TimeStamp, listaPrepakovana[i].HistoricalProperties[j].Value.TimeStamp);
                }
            }

            ////// REMOVE
            ///
            listaDolazna = delta.Remove;

            listaPrepakovana = new List<Description>();
            foreach (Description d in hs.LD)
            {
                if (d.Tip == 3)
                {
                    listaPrepakovana.Add(d);
                }
            }

            for (int i = 0; i < listaDolazna.Count; i++)
            {
                Assert.AreEqual(listaDolazna[i].Id, listaPrepakovana[i].Id);
                Assert.AreEqual(listaDolazna[i].DataSet, listaPrepakovana[i].DataSet);

                for (int j = 0; j < listaDolazna[i].DumpingPropertyCollection.Count; j++)
                {
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Code, listaPrepakovana[i].HistoricalProperties[j].Code);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.IdGeografskogPodrucja, listaPrepakovana[i].HistoricalProperties[j].Value.IdGeografskogPodrucja);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.Potrosnja, listaPrepakovana[i].HistoricalProperties[j].Value.Potrosnja);
                    Assert.AreEqual(listaDolazna[i].DumpingPropertyCollection[j].Value.TimeStamp, listaPrepakovana[i].HistoricalProperties[j].Value.TimeStamp);
                }
            }


        }


        [Test]
        [TestCase(100, 50)]
        [TestCase(200, 300)]
        public void CheckDeadBandTestIzlazi(int oldValue, int newValue)
		{
            bool ret = hs.CheckDeadBand(oldValue, newValue);
            Assert.AreEqual(true, ret);

		}

        [Test]
        [TestCase(100, 101)]
        [TestCase(200, 198)]
        public void CheckDeadBandTestNeIzlazi(int oldValue, int newValue)
        {
            bool ret = hs.CheckDeadBand(oldValue, newValue);
            Assert.AreEqual(false, ret);


        }

        

		[TearDown]
        public void TearDown()
        {
            logger = null;
            listForCheckDataTest = null;
            delta = null;
        }


    }
}
