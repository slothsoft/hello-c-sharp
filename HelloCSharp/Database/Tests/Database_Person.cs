using System;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database
{
    [TestFixture]
    public class Database_Person
    {

        /* This is the one person we test for by ID, so don't change it (or change the tests, too) */
        private const int POWDER_ID = 2;
        
        private Database _classUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new Database();
        }

        [Test]
        public void GetPerson()
        {
            var result = _classUnderTest.GetPerson(POWDER_ID);

            Assert.NotNull(result);
            Assert.AreEqual(POWDER_ID, result.Id);
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
            catch (Exception e)
            {
                Assert.AreEqual("Could not find person with ID -1", e.Message);
            }
        }
        
        [Test]
        public void FindAllPersons()
        {
            var result = _classUnderTest.FindAllPersons();

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 3);

            var person = result.Find(c => c.Id == POWDER_ID);
            Assert.NotNull(person);
            Assert.AreEqual(POWDER_ID, person.Id);
            Assert.AreEqual("Powder", person.Name);
            Assert.AreEqual(17, person.Age);
        }

    }
}