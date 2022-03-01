using System;
using System.Linq;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Tests.TestData;
using HelloCSharp.Rest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HelloCSharp.Rest.Tests.Controllers;

public class RelationshipTypeControllerTest
{

    private DatabaseContext _databaseContext = null!;
    private RelationshipTypeController _classUnderTest = null!;
        
    [SetUp]
    public void SetUp()
    {
        _databaseContext = new DatabaseContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Filename=TestDatabase.db").Options);
        _databaseContext.Database.EnsureCreated();
                
        _classUnderTest = new RelationshipTypeController();
    }
        
    [Test]
    public void GetSingle()
    {
        const RelationshipType example = RelationshipType.Siblings;
        var okResult = _classUnderTest.GetSingle(example.ToString());
        Assert.IsTrue(okResult is OkObjectResult, "Result should be OkObjectResult but was " + okResult);

        var result = (RelationshipTypeDto) ((OkObjectResult) okResult).Value!;
        Assert.NotNull(result);
        Assert.AreEqual(example.ToString(), result.Id);
        Assert.AreEqual(example.GetDisplayName(), result.DisplayName);
    }

    [Test]
    public void GetSingleUnknown()
    {
        var result = _classUnderTest.GetSingle("no-real-relationship");
        Assert.IsTrue(result is NotFoundResult, "Result should be NotFoundResult but was " + result);
    }
        
    [Test]
    public void GetList()
    {
        var result = _classUnderTest.GetList().ToArray();

        Assert.NotNull(result);
        Assert.AreEqual(Enum.GetValues(typeof(RelationshipType)).Length, result!.Length);

        var found = Array.Find(result, c => c.Id == RelationshipType.Siblings.ToString());
        Assert.NotNull(found);
        Assert.AreEqual(found.DisplayName, RelationshipType.Siblings.GetDisplayName());
    }
    
}