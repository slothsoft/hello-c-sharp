using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Tests.TestData;
using HelloCSharp.Rest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

public abstract class AbstractRepositoryControllerTest<TController, TIdentifiable, TSave>
    where TController : AbstractRepositoryController<TIdentifiable, TSave>
    where TIdentifiable : Identifiable
{

    private DatabaseContext _databaseContext = null!;
    private TController _classUnderTest = null!;
    private ITestData<TIdentifiable, TSave> _testData = null!;
        
    [SetUp]
    public void SetUp()
    {
        _databaseContext = new DatabaseContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Filename=TestDatabase.db").Options);
        _databaseContext.Database.EnsureCreated();
                
        _classUnderTest = CreateRepositoryController(_databaseContext);
        _testData = CreateTestData(_databaseContext);
    }
        
    protected abstract TController CreateRepositoryController(DatabaseContext databaseContext);
    
    protected abstract ITestData<TIdentifiable, TSave> CreateTestData(DatabaseContext databaseContext);
       
    [Test]
    public void Post()
    {
        var random = _testData.CreateRandomObject();
        var createResult = _classUnderTest.Post(random);

        Assert.NotNull(createResult);
        Assert.NotNull(createResult.Id);
        _testData.AssertAreEqual(random, createResult);
    }
    
    [Test]
    public void GetSingle()
    {
        var example = _testData.GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        var okResult = _classUnderTest.GetSingle(example.Id);
        Assert.IsTrue(okResult is OkObjectResult, "Result should be OkObjectResult but was " +okResult);

        var result = (TIdentifiable) ((OkObjectResult) okResult).Value!;
        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result.Id);
        _testData.AssertAreEqual(example, result);
    }

    [Test]
    public void GetSingleUnknown()
    {
        var result = _classUnderTest.GetSingle(-1);
        Assert.IsTrue(result is NotFoundResult, "Result should be NotFoundResult but was " +result);
    }
        
    [Test]
    public void GetList()
    {
        var result = _classUnderTest.GetList();

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = _testData.GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        _testData.AssertAreEqual(example, found);
    }
    
    [Test]
    public void PutAtId()
    {
        var createResult = _classUnderTest.Post(_testData.CreateRandomObject());
        var random = _testData.CreateRandomObject();
        var updateResult = _classUnderTest.PutAtId(createResult.Id, random);

        Assert.NotNull(createResult);
        Assert.AreEqual(createResult.Id, updateResult.Id);
        _testData.AssertAreEqual(random, updateResult);
    }
}