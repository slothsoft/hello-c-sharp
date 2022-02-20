using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Persistence.Tests.TestData;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

[TestFixture]
public class PersonEndpointTest : AbstractEndpointTest<Person, SavePerson>
{

    public PersonEndpointTest() : base("api/persons/")
    {
    }
    
    protected override PersonTestData CreateTestData(DatabaseContext databaseContext)
    {
        return new PersonTestData(databaseContext);
    }
}