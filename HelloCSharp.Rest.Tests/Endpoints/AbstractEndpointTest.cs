using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Endpoints;

public abstract class AbstractEndpointTest<TIdentifiable>
    where TIdentifiable : Identifiable
{

    private static RestTestApplicationFactory _factory;

    private HttpClient _client;
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
        _factory?.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
    }
    
    [Test]
    public void GetSingle()
    {
        var example = GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        
        var response = _client.GetFromJsonAsync<TIdentifiable>(_endpoint + example.Id);
        response.Wait();
        
        var result = response.Result!;
        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result.Id);
        AssertAreEqual(example, result);
    }

    protected abstract TIdentifiable GetExampleObject();
        
    protected abstract void AssertAreEqual(TIdentifiable expected, TIdentifiable actual);
        
    [Test]
    public void GetSingleUnknown()
    {
        try
        {
           var response = _client.GetFromJsonAsync<TIdentifiable>(_endpoint + "unknown");
           response.Wait();
           Assert.Fail("There should have been an error here!");
        }
        catch (AggregateException e)
        {
            Assert.IsTrue(e.Message.Contains("404 (Not Found)"),"Wrong error found: " + e.Message);
        }
    }
        
        
    [Test]
    public void GetList()
    {
        var response = _client.GetFromJsonAsync<List<TIdentifiable>>(_endpoint);
        response.Wait();
        var result = response.Result!;

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found.Id);
        AssertAreEqual(example, found);
    }

}