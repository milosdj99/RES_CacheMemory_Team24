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
	class HistoricalValueTest
	{

		[Test]
		[TestCase(5, 7)]
		[TestCase(10, 15)]
		public void KonstruktorDobriParametri(int idGeografskogPodrucja, int potrosnja)
		{
			DateTime dt = new DateTime(2000, 7, 7);

			HistoricalValue hv = new HistoricalValue(dt, idGeografskogPodrucja, potrosnja);

			Assert.AreEqual(hv.TimeStamp, dt);
			Assert.AreEqual(hv.IdGeografskogPodrucja, idGeografskogPodrucja);
			Assert.AreEqual(hv.Potrosnja, potrosnja);
		}
	}
}
