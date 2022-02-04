using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HellCSharp.Persistence.Database.Tests;

[TestFixture]
public class PersonRepositoryTest : AbstractRepositoryTest<PersonRepository, Person>
{
    protected override PersonRepository CreateRepository(Database database)
    {
        return new PersonRepository(database.Persons);
    }

    protected override Person GetExampleObject()
    {
        return new Person(2, "Powder", 17, new City(2, "Zaun"));
    }

    protected override void AssertAreEqual(Person expected, Person actual)
    {
        Assert.AreEqual(expected.Name, actual.Name);
        Assert.AreEqual(expected.Age, actual.Age);
        Assert.AreEqual(expected.City.Name, actual.City.Name);
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
        Assert.AreEqual(example.Id, found.Id);
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