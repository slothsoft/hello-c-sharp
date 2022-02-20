using System;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;

namespace HelloCSharp.Persistence.Tests.TestData;

public class CityTestData : ITestData<City, SaveCity>
{
    
    public City GetExampleObject()
    {
        return CityExtensions.CreateExampleObject();
    }
    
    public SaveCity CreateRandomObject()
    {
        return new SaveCity { Name = Guid.NewGuid().ToString() };
    }

    public void AssertAreEqual(City expected, City actual) =>  expected.AssertAreEqual(actual);
    
    public void AssertAreEqual(SaveCity expected, City actual) => expected.AssertAreEqual(actual);
}