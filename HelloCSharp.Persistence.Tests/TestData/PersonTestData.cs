using System;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Persistence.Entities;

namespace HelloCSharp.Persistence.Tests.TestData;

public class PersonTestData : ITestData<Person, SavePerson>
{
    private readonly DatabaseContext _context;

    public PersonTestData(DatabaseContext context)
    {
        _context = context;
    }

    public Person GetExampleObject()
    {
        return PersonExtensions.CreateExampleObject();
    }
    
    public SavePerson CreateRandomObject()
    {
        var city = _context.Add(new CityEntity { Name = "Dresden" });
        _context.SaveChanges();
        return new SavePerson
        {
            Name = Guid.NewGuid().ToString(), 
            Age = city.Entity.Id!.Value,
            CityId = city.Entity.Id!.Value
        };
    }

    public void AssertAreEqual(Person expected, Person actual) =>  expected.AssertAreEqual(actual);
    
    public void AssertAreEqual(SavePerson expected, Person actual) => expected.AssertAreEqual(actual);
}