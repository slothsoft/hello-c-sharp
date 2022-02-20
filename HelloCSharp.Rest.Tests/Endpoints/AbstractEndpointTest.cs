using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Persistence.Tests.TestData;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using IServiceScopeFactory = Microsoft.Extensions.DependencyInjection.IServiceScopeFactory;

namespace HelloCSharp.Rest.Tests.Endpoints;

public abstract class AbstractEndpointTest<TIdentifiable, TSave>
    where TIdentifiable : Identifiable
{
    private static RestTestApplicationFactory _factory = null!;

    private HttpClient _client = null!;
    private ITestData<TIdentifiable, TSave> _testData = null!;
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
        
        var scope = _factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var context =  scope.ServiceProvider.GetService<DatabaseContext>();
        Assert.NotNull(context, "DbContext could not be found!");
        _testData = CreateTestData(context!);
    }
    
    protected abstract ITestData<TIdentifiable, TSave> CreateTestData(DatabaseContext databaseContext);

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
    
    [Test]
    public async Task Post()
    {
        var random = _testData.CreateRandomObject();
        var createMessage = await _client.PostAsJsonAsync(_endpoint, random);
        var createResult = await createMessage.Content.ReadFromJsonAsync<TIdentifiable>();

        Assert.NotNull(createResult);
        Assert.NotNull(createResult!.Id);
        _testData.AssertAreEqual(random, createResult);
    }
    
    [Test]
    public async Task GetSingle()
    {
        var example = _testData.GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        
        var result = await _client.GetFromJsonAsync<TIdentifiable>(_endpoint + example.Id);
        
        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result!.Id);
        _testData.AssertAreEqual(example, result);
    }
        
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

        var example = _testData.GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        _testData.AssertAreEqual(example, found);
    }
    
    [Test]
    public async Task PutAtId()
    {
        var createMessage = await _client.PostAsJsonAsync(_endpoint, _testData.CreateRandomObject());
        var createResult = await createMessage.Content.ReadFromJsonAsync<TIdentifiable>();
        
        var random = _testData.CreateRandomObject();
        var updateMessage = await _client.PutAsJsonAsync(_endpoint + createResult!.Id, random);
        var updateResult = await updateMessage.Content.ReadFromJsonAsync<TIdentifiable>();

        Assert.NotNull(updateResult);
        Assert.NotNull(updateResult!.Id);
        _testData.AssertAreEqual(random, updateResult);
    }
}