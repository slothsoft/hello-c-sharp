using System.Data.SQLite;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    public class CityRepositoryTest : AbstractRepositoryTest<CityRepository, City>
    {
        protected override CityRepository CreateRepository(SQLiteConnection connection)
        {
            return new CityRepository(connection);
        }

        protected override City GetExampleObject()
        {
            return new City(1, "Piltover");
        }

        protected override void AssertAreEqual(City expected, City actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}