using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Rest.Controllers;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

[TestFixture]
public class CityControllerTest : AbstractRepositoryControllerTest<CityController, City>
{
    protected override CityController CreateRepositoryController(DatabaseContext databaseContext)
    {
        return new CityController(databaseContext);
    }

    protected override City GetExampleObject()
    {
        return CityExtensions.CreateExampleObject();
    }

    protected override void AssertAreEqual(City expected, City actual)
    {
        expected.AssertAreEqual(actual);
    }
}