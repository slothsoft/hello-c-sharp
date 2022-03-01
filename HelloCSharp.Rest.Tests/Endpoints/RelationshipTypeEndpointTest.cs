using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelloCSharp.Api.Models;
using HelloCSharp.Rest.Controllers;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

public class RelationshipTypeEndpointTest
{

    private static readonly string Endpoint  = "api/relationshiptype/";
    private static RestTestApplicationFactory _factory = null!;

    private HttpClient _client = null!;

    [OneTimeSetUp]
    public static void SetUpOnce()
    {
        _factory = new RestTestApplicationFactory();
    }

    [OneTimeTearDown]
    public static void TearDownOnce()
    {
        _factory.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        _client = _factory.CreateClient();
    }
        
    [Test]
    public async Task GetSingle()
    {
        const RelationshipType example = RelationshipType.Partners;
        var okResult = await _client.GetAsync(Endpoint + example.ToString());
        Assert.AreEqual(HttpStatusCode.OK, okResult.StatusCode, okResult.ReasonPhrase);

        var result = await okResult.Content.ReadFromJsonAsync<RelationshipTypeDto>();
        Assert.NotNull(result);
        Assert.AreEqual(example.ToString(), result.Id);
        Assert.AreEqual(example.GetDisplayName(), result.DisplayName);
    }

    [Test]
    public async Task GetSingleUnknown()
    {
        var result = await _client.GetAsync(Endpoint + "no-real-relationship");
        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode, result.ReasonPhrase);
    }
        
    [Test]
    public async Task GetList()
    {
        var result = await _client.GetFromJsonAsync<IList<RelationshipTypeDto>>(Endpoint);

        Assert.NotNull(result);
        Assert.AreEqual(Enum.GetValues(typeof(RelationshipType)).Length, result!.Count);

        var found = result.Single(c => c.Id == RelationshipType.ChildOf.ToString());
        Assert.NotNull(found);
        Assert.AreEqual(RelationshipType.ChildOf.GetDisplayName(), found.DisplayName);
    }
    
}