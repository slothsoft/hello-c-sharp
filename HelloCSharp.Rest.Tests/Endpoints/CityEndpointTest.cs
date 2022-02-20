using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Persistence.Tests.TestData;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

[TestFixture]
public class CityEndpointTest : AbstractEndpointTest<City, SaveCity>
{

    public CityEndpointTest() : base("api/cities/")
    {
    }
    
    protected override CityTestData CreateTestData(DatabaseContext databaseContext)
    {
        return new CityTestData();
    }
}