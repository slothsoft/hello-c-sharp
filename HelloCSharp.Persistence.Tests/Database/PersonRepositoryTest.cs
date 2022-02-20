using System;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Entities;
using HelloCSharp.Persistence.Tests.TestData;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.Database;

[TestFixture]
public class PersonRepositoryTest : AbstractRepositoryTest<PersonRepository, Person, SavePerson>
{
    protected override PersonRepository CreateRepository(DatabaseContext databaseContext)
    {
        return new PersonRepository(databaseContext, databaseContext.Persons);
    }

    protected override PersonTestData CreateTestData(DatabaseContext databaseContext)
    {
        return new PersonTestData(databaseContext);
    }

    [Test]
    public void FindByCityId()
    {
        var example = TestData.GetExampleObject();
        var result = ClassUnderTest.FindByCityId(example.City.Id);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // has at least the example object

        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        TestData.AssertAreEqual(example, found);
    }
        
    [Test]
    public void FindByCityIdUnknown()
    {
        var result = ClassUnderTest.FindByCityId(-1);

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
    }
}