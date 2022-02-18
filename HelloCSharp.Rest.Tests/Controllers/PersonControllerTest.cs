using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Rest.Controllers;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

[TestFixture]
public class PersonControllerTest : AbstractRepositoryControllerTest<PersonController, Person>
{
    protected override PersonController CreateRepositoryController(DatabaseContext databaseContext)
    {
        return new PersonController(databaseContext);
    }

    protected override Person GetExampleObject()
    {
        return PersonExtensions.CreateExampleObject();
    }

    protected override void AssertAreEqual(Person expected, Person actual)
    {
        expected.AssertAreEqual(actual);
    }
}