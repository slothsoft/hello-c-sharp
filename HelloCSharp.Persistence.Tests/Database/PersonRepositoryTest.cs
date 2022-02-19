using System;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Entities;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.Database;

[TestFixture]
public class PersonRepositoryTest : AbstractRepositoryTest<PersonRepository, Person>
{
    protected override PersonRepository CreateRepository(DatabaseContext databaseContext)
    {
        return new PersonRepository(databaseContext, databaseContext.Persons);
    }

    protected override Person GetExampleObject()
    {
        return PersonExtensions.CreateExampleObject();
    }
    
    protected override Person CreateRandomObject(int? id = null)
    {
        var city = DatabaseContext.Add(new CityEntity { Name = "Dresden" });
        DatabaseContext.SaveChanges();
        return new Person(id ?? -1, Guid.NewGuid().ToString(), city.Entity.Id!.Value, 
            new City(city.Entity.Id!.Value, city.Entity.Name!));
    }

    protected override void AssertAreEqual(Person expected, Person actual)
    {
        expected.AssertAreEqual(actual);
    }
        
    [Test]
    public void FindByCityId()
    {
        var example = GetExampleObject();
        var result = ClassUnderTest.FindByCityId(example.City.Id);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // has at least the example object

        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        AssertAreEqual(example, found);
    }
        
    [Test]
    public void FindByCityIdUnknown()
    {
        var result = ClassUnderTest.FindByCityId(-1);

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
    }
}