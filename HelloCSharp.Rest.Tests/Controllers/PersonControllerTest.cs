using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Tests.TestData;
using HelloCSharp.Rest.Controllers;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

[TestFixture]
public class PersonControllerTest : AbstractRepositoryControllerTest<PersonController, Person, SavePerson>
{
    protected override PersonController CreateRepositoryController(DatabaseContext databaseContext)
    {
        return new PersonController(databaseContext);
    }

    protected override PersonTestData CreateTestData(DatabaseContext databaseContext)
    {
        return new PersonTestData(databaseContext);
    }
}