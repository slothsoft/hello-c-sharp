using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Api.Tests;

public static class CityExtensions
{
    
    public static void AssertAreEqual(this City expected, City actual)
    {
        Assert.AreEqual(expected.Name, actual.Name);
    }
    
    public static City CreateExampleObject()
    {
        return new City(1, "Piltover");
    }
}