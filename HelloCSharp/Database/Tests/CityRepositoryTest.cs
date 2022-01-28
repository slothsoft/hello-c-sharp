using System.Data.SQLite;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    [TestFixture]
    public class CityRepositoryTest : AbstractRepositoryTest<CityRepository, City>
    {
        protected override CityRepository CreateRepository(Database database)
        {
            return new CityRepository(database.Cities);
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