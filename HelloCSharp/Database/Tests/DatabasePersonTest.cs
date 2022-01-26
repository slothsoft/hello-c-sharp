using System;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    [TestFixture]
    public class DatabasePersonTest
    {

        /* This is the one person we test for by ID, so don't change it (or change the tests, too) */
        private const int PowderId = 2;
        
        private Database _classUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new Database();
        }

        [Test]
        public void GetPerson()
        {
            var result = _classUnderTest.GetPerson(PowderId);

            Assert.NotNull(result);
            Assert.AreEqual(PowderId, result.Id);
            Assert.AreEqual("Powder", result.Name);
            Assert.AreEqual(17, result.Age);
        }
        
        [Test]
        public void GetPersonUnknown()
        {
            try
            {
                _classUnderTest.GetPerson(-1);
                Assert.Fail("Needs more exceptions!");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Could not find entity with ID -1", e.Message);
            }
        }
        
        [Test]
        public void FindAllPersons()
        {
            var result = _classUnderTest.FindAllPersons();

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 3);

            var person = result.Find(c => c.Id == PowderId);
            Assert.NotNull(person);
            Assert.AreEqual(PowderId, person.Id);
            Assert.AreEqual("Powder", person.Name);
            Assert.AreEqual(17, person.Age);
        }
        
        [Test]
        public void FindByCityId()
        {
            var result = _classUnderTest.FindPersonsByCityId(2);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1); 

            var found = result.Find(c => c.Id .Equals(PowderId));
            Assert.NotNull(found);
            Assert.AreEqual(PowderId, found.Id);
            Assert.AreEqual("Powder", found.Name);
            Assert.AreEqual(17, found.Age);
        }
        
        [Test]
        public void FindByCityIdUnknown()
        {
            var result = _classUnderTest.FindPersonsByCityId(-1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

    }
}