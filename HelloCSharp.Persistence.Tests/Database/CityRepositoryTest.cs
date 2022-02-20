using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Tests.TestData;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.Database;

[TestFixture]
public class CityRepositoryTest : AbstractRepositoryTest<CityRepository, City, SaveCity>
{
    protected override CityRepository CreateRepository(DatabaseContext databaseContext)
    {
        return new CityRepository(databaseContext, databaseContext.Cities);
    }

    protected override CityTestData CreateTestData(DatabaseContext databaseContext)
    {
        return new CityTestData();
    }
}