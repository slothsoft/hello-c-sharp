using System;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    [TestFixture]
    public class DatabaseCity
    {

        /* This is the one city we test for by ID, so don't change it (or change the tests, too) */
        private const int PiltoverId = 1;
        
        private Database _classUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new Database();
        }

        [Test]
        public void GetCity()
        {
            var result = _classUnderTest.GetCity(PiltoverId);

            Assert.NotNull(result);
            Assert.AreEqual(PiltoverId, result.Id);
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

            var city = result.Find(c => c.Id == PiltoverId);
            Assert.NotNull(city);
            Assert.AreEqual(PiltoverId, city.Id);
            Assert.AreEqual("Piltover", city.Name);
        }

    }
}