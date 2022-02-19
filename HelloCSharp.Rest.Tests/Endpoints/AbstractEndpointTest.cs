using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

public abstract class AbstractEndpointTest<TIdentifiable>
    where TIdentifiable : Identifiable
{
    private static RestTestApplicationFactory _factory = null!;

    private HttpClient _client = null!;
    private readonly string _endpoint;

    protected AbstractEndpointTest(string endpoint)
    {
        _endpoint = endpoint;
    }

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

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
    
    [Test]
    public async Task GetSingle()
    {
        var example = GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        
        var result = await _client.GetFromJsonAsync<TIdentifiable>(_endpoint + example.Id);
        
        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result!.Id);
        AssertAreEqual(example, result);
    }

    protected abstract TIdentifiable GetExampleObject();
        
    protected abstract void AssertAreEqual(TIdentifiable expected, TIdentifiable actual);
        
    [Test]
    public async Task GetSingleUnknown()
    {
        var response = await _client.GetAsync(_endpoint + "unknown");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        Assert.AreEqual("Not Found", response.ReasonPhrase);
    }
        
        
    [Test]
    public async Task GetList()
    {
        var result = await _client.GetFromJsonAsync<List<TIdentifiable>>(_endpoint);

        Assert.NotNull(result);
        Assert.IsTrue(result!.Count >= 1); // every table has example rows

        var example = GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        AssertAreEqual(example, found);
    }

}