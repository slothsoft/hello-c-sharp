using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Rest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

public abstract class AbstractRepositoryControllerTest<TController, TIdentifiable>
    where TController : AbstractRepositoryController<TIdentifiable>
    where TIdentifiable : Identifiable
{

    private DatabaseContext _databaseContext;
    protected TController ClassUnderTest;
        
    [SetUp]
    public void SetUp()
    {
        _databaseContext = new DatabaseContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Filename=TestDatabase.db").Options);
        _databaseContext.Database.EnsureCreated();
                
        ClassUnderTest = CreateRepositoryController(_databaseContext);
    }
        
    protected abstract TController CreateRepositoryController(DatabaseContext databaseContext);
        
    [TearDown]
    public void TearDown()
    {
        _databaseContext?.Close();
    }

    [Test]
    public void GetSingle()
    {
        var example = GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        var okResult = ClassUnderTest.GetSingle(example.Id);
        Assert.IsTrue(okResult is OkObjectResult, "Result should be OkObjectResult but was " +okResult);

        var result = (TIdentifiable) ((OkObjectResult) okResult).Value!;
        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result.Id);
        AssertAreEqual(example, result);
    }

    protected abstract TIdentifiable GetExampleObject();
        
    protected abstract void AssertAreEqual(TIdentifiable expected, TIdentifiable actual);
        
    [Test]
    public void GetSingleUnknown()
    {
        var result = ClassUnderTest.GetSingle(-1);
        Assert.IsTrue(result is NotFoundResult, "Result should be NotFoundResult but was " +result);
    }
        
        
    [Test]
    public void GetList()
    {
        var result = ClassUnderTest.GetList();

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found.Id);
        AssertAreEqual(example, found);
    }

}