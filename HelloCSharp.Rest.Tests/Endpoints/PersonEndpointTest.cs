using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

[TestFixture]
public class PersonEndpointTest : AbstractEndpointTest<Person>
{

    public PersonEndpointTest() : base("api/persons/")
    {
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