using CacheMemory.Historical;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMemoryTest
{
	[TestFixture]
	class DescriptionTest
	{

        [Test]
        [TestCase(1,1,1)]
        [TestCase(1,2,3)]
        [TestCase(3,2,3)]
        public void KonstruktorDobriParametriTest(int tip, int id, int dataSet)
        {
            List<HistoricalProperty> lista = new List<HistoricalProperty>();

            Description d = new Description(tip, id, dataSet, lista);

            Assert.AreEqual(tip, d.Tip);
            Assert.AreEqual(id, d.Id);
            Assert.AreEqual(dataSet, d.DataSet);
            Assert.AreEqual(lista, d.HistoricalProperties);
        }

        
    }
}
