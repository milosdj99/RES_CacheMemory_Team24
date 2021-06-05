using CacheMemory.Common;
using CacheMemory.Historical;
using CacheMemory.Reader;
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
	class ReaderTest
	{
		IHistoricall hs = null;
		Mock<IHistoricall> moq = null;

		[SetUp]
		public void SetUp()
		{
			moq = new Mock<IHistoricall>();

			hs = moq.Object;

			List<HistoricalProperty> lista = new List<HistoricalProperty>();

			int dataset = 1;
			moq.Setup(x => x.DataSetSearch(dataset)).Returns(lista);

			string code = "CODE_DIGITAL";
			moq.Setup(x => x.CodeSearch(code)).Returns(lista);

			DateTime d1 = new DateTime(1999, 6, 6);
			DateTime d2 = new DateTime(2000, 6, 6);
			moq.Setup(x => x.DateSearch(d1,d2)).Returns(lista);
		}

		[Test]
		public void KonstruktorDobarParametarTest()
		{
			
			Reader rd = new Reader(hs);

			Assert.AreEqual(rd.Historical, hs);
		}

		[Test]
		public void KonstruktorNullParametarTest()
		{
			
			Reader rd = new Reader(null);

			Assert.AreEqual(rd.Historical, null);
		}

		[Test]
		public void DataSetSearchTest()
		{
			Reader rd = new Reader(hs);

			rd.DataSetSearch(1);

			moq.Verify(x => x.DataSetSearch(1));
		}

		[Test]
		public void CodeSearchTest()
		{
			Reader rd = new Reader(hs);

			rd.CodeSearch("CODE_DIGITAL");

			moq.Verify(x => x.CodeSearch("CODE_DIGITAL"), Times.Once);
		}

		[Test]
		public void DateSearchTest()
		{
			Reader rd = new Reader(hs);


			DateTime d1 = new DateTime(1999, 6, 6);
			DateTime d2 = new DateTime(2000, 6, 6);

			rd.DateSearch(d1, d2);

			moq.Verify(x => x.DateSearch(d1,d2));
		}

		[TearDown]
		public void TearDown()
		{
			moq = null;

			hs = null; 
		}

	}
}
