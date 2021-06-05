using CacheMemory.Common;
using CacheMemory.Historical;
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
	class HistoricalPropertyTest
	{
		IHistoricalValue hv;

		Mock<IHistoricalValue> moq;
		

		[SetUp]
		public void SetUp()
		{
			moq = new Mock<IHistoricalValue>();
			hv = moq.Object;
		}

		[Test]
		[TestCase("CODE_ANALOG")]
		[TestCase("CODE_DIGITAL")]
		public void KonstruktorDobriParametriTest(string code)
		{

			HistoricalProperty hp = new HistoricalProperty(code, hv);

			Assert.AreEqual(hp.Code, code);
			Assert.AreEqual(hp.Value, hv);
		}
	}
}
