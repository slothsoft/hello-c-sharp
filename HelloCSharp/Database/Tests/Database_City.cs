using System;
using NUnit.Framework;

namespace HelloCSharp.Database
{
    [TestFixture]
    public class Database_City
    {

        /* This is the one city we test for by ID, so don't change it (or change the tests, too) */
        private const int PILTOVER_ID = 1;
        
        private Database _classUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new Database();
        }

        [Test]
        public void GetCity()
        {
            var result = _classUnderTest.GetCity(PILTOVER_ID);

            Assert.NotNull(result);
            Assert.AreEqual(PILTOVER_ID, result.Id);
            Assert.AreEqual("Piltover", result.Name);
        }
        
        [Test]
        public void GetCityUnknown()
        {
            try
            {
                _classUnderTest.GetCity(-1);
                Assert.Fail("Needs more exceptions!");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Could not find entity with ID -1", e.Message);
            }
        }
        
        [Test]
        public void FindAllCities()
        {
            var result = _classUnderTest.FindAllCities();

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 2);

            var city = result.Find(c => c.Id == PILTOVER_ID);
            Assert.NotNull(city);
            Assert.AreEqual(PILTOVER_ID, city.Id);
            Assert.AreEqual("Piltover", city.Name);
        }

    }
}