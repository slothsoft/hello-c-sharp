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
    }
}