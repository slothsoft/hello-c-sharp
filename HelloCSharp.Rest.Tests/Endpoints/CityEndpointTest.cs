using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

[TestFixture]
public class CityEndpointTest : AbstractEndpointTest<City>
{

    public CityEndpointTest() : base("api/cities/")
    {
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