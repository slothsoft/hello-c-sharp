using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharpRest.Tests.Endpoints;

public class CityEndpointTest : AbstractEndpointTest<City>
{

    public CityEndpointTest() : base("api/city/")
    {
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