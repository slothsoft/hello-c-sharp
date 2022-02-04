using HellCSharp.Persistence.Database;
using HellCSharp.Persistence.Database.Tests;
using HelloCSharp.Api.Models;
using HelloCSharp.Rest.Controllers;
using NUnit.Framework;

namespace HelloCSharpRest.Tests.Controllers;

[TestFixture]
public class CityControllerTest : AbstractRepositoryControllerTest<CityController, City>
{
    protected override CityController CreateRepositoryController(Database database)
    {
        return new CityController(database);
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