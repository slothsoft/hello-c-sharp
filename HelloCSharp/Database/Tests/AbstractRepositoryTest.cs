using System;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests;

public abstract class AbstractRepositoryTest<TRepository, TIdentifiable>
    where TRepository : IRepository<TIdentifiable>
    where TIdentifiable : Identifiable
{

        
    private Database _database;
    protected TRepository ClassUnderTest;
        
    [SetUp]
    public void SetUp()
    {
        _database = new Database();
        _database.Database.EnsureCreated();
                
        ClassUnderTest = CreateRepository(_database);
    }
        
    protected abstract TRepository CreateRepository(Database database);
        
    [TearDown]
    public void TearDown()
    {
        _database?.Close();
    }

    [Test]
    public void GetById()
    {
        var example = GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        var result = ClassUnderTest.GetById(example.Id);

        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result.Id);
        AssertAreEqual(example, result);
    }

    protected abstract TIdentifiable GetExampleObject();
        
    protected abstract void AssertAreEqual(TIdentifiable expected, TIdentifiable actual);
        
    [Test]
    public void GetByIdUnknown()
    {
        try
        {
            ClassUnderTest.GetById(-1);
            Assert.Fail("Needs more exceptions!");
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Could not find entity with ID -1", e.Message);
        }
    }
        
    [Test]
    public void FindById()
    {
        var example = GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        var result = ClassUnderTest.FindById(example.Id);

        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result.Id);
        AssertAreEqual(example, result);
    }
        
    [Test]
    public void FindByIdUnknown()
    {
        Assert.Null(ClassUnderTest.FindById(-1));
    }

    [Test]
    public void FindAll()
    {
        var result = ClassUnderTest.FindAll();

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found.Id);
        AssertAreEqual(example, found);
    }

    [Test]
    public void FindByFilterTrue()
    {
        var result = ClassUnderTest.FindByFilter(t => true);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = GetExampleObject();
        var found = result.Find(c => c.Id .Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found.Id);
        AssertAreEqual(example, found);
    }
        
    [Test]
    public void FindByFilterFalse()
    {
        var result = ClassUnderTest.FindByFilter(t => false);

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
    }
        
    [Test]
    public void FindByFilterById()
    {
        var example = GetExampleObject();
        var result = ClassUnderTest.FindByFilter(t => t.Id == example.Id);

        Assert.NotNull(result);
        Assert.AreEqual(1, result.Count);

        var found = result[0];
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found.Id);
        AssertAreEqual(example, found);
    }
}