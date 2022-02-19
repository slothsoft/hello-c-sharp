using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.Database;

[TestFixture]
public class CityRepositoryTest : AbstractRepositoryTest<CityRepository, City>
{
    protected override CityRepository CreateRepository(DatabaseContext databaseContext)
    {
        return new CityRepository(databaseContext, databaseContext.Cities);
    }

    protected override City GetExampleObject()
    {
        return CityExtensions.CreateExampleObject();
    }
    
    protected override City CreateRandomObject(int? id = null)
    {
        return new City(id ?? -1, "Dresden");
    }

    protected override void AssertAreEqual(City expected, City actual)
    {
        expected.AssertAreEqual(actual);
    }
}