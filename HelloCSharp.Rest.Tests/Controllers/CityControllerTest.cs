using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Tests.TestData;
using HelloCSharp.Rest.Controllers;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

[TestFixture]
public class CityControllerTest : AbstractRepositoryControllerTest<CityController, City, SaveCity>
{
    protected override CityController CreateRepositoryController(DatabaseContext databaseContext)
    {
        return new CityController(databaseContext);
    }

    protected override CityTestData CreateTestData(DatabaseContext databaseContext)
    {
        return new CityTestData();
    }
}