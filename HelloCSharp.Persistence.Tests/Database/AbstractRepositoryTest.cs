using System;
using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.Database;

public abstract class AbstractRepositoryTest<TRepository, TIdentifiable>
    where TRepository : IRepository<TIdentifiable>
    where TIdentifiable : Identifiable
{
    protected HelloCSharp.Persistence.Database.DatabaseContext DatabaseContext = null!;
    protected TRepository ClassUnderTest = default!;

    [SetUp]
    public void SetUp()
    {
        DatabaseContext = new HelloCSharp.Persistence.Database.DatabaseContext(new DbContextOptionsBuilder()
            .UseInMemoryDatabase("Filename=TestDatabase.db").Options);
        DatabaseContext.Database.EnsureCreated();

        ClassUnderTest = CreateRepository(DatabaseContext);
    }

    protected abstract TRepository CreateRepository(Persistence.Database.DatabaseContext databaseContext);

    [Test]
    public void Create()
    {
        var example = CreateRandomObject();
        var result = ClassUnderTest.Create(example);

        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        AssertAreEqual(example, result);
    }

    protected abstract TIdentifiable CreateRandomObject(int? id = null);
    
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
        Assert.AreEqual(example.Id, result!.Id);
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
        var found = result.Find(c => c.Id.Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        AssertAreEqual(example, found);
    }

    [Test]
    public void FindByFilterTrue()
    {
        var result = ClassUnderTest.FindByFilter(_ => true);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = GetExampleObject();
        var found = result.Find(c => c.Id.Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        AssertAreEqual(example, found);
    }

    [Test]
    public void FindByFilterFalse()
    {
        var result = ClassUnderTest.FindByFilter(_ => false);

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
    
    [Test]
    public void Update()
    {
        var create = ClassUnderTest.Create(CreateRandomObject());

        var toBeUpdated = CreateRandomObject(create.Id);
        var update = ClassUnderTest.Update(toBeUpdated);

        Assert.NotNull(update);
        Assert.AreEqual(create.Id, update.Id);
        AssertAreEqual(toBeUpdated, update);
    }

}