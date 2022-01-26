using System.Data.SQLite;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    public class PersonRepositoryTest : AbstractRepositoryTest<PersonRepository, Person>
    {
        protected override PersonRepository CreateRepository(SQLiteConnection connection)
        {
            return new PersonRepository(connection);
        }

        protected override Person GetExampleObject()
        {
            return new Person(2, "Powder", 17, new City(2, "Zaun"));
        }

        protected override void AssertAreEqual(Person expected, Person actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Age, actual.Age);
            Assert.AreEqual(expected.City.Name, actual.City.Name);
        }
        
        [Test]
        public void FindByCityId()
        {
            var example = GetExampleObject();
            var result = _classUnderTest.FindByCityId((int) example.City.Id);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1); // has at least the example object

            var found = result.Find(c => c.Id .Equals(example.Id));
            Assert.NotNull(found);
            Assert.AreEqual(example.Id, found.Id);
            AssertAreEqual(example, found);
        }
        
        [Test]
        public void FindByCityIdUnknown()
        {
            var result = _classUnderTest.FindByCityId(-1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}