using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Api.Tests;

public static class PersonExtensions
{
    
    public static void AssertAreEqual(this Person expected, Person actual)
    {
        Assert.AreEqual(expected.Name, actual.Name);
        Assert.AreEqual(expected.Age, actual.Age);
        Assert.AreEqual(expected.City.Id, actual.City.Id);
        Assert.AreEqual(expected.City.Name, actual.City.Name);
    }
    
    public static void AssertAreEqual(this SavePerson expected, Person actual)
    {
        Assert.AreEqual(expected.Name, actual.Name);
        Assert.AreEqual(expected.Age, actual.Age);
        Assert.AreEqual(expected.CityId, actual.City.Id);
    }
    
    public static Person CreateExampleObject()
    {
        return new Person(2, "Powder", 17, new City(2, "Zaun"));
    }
}