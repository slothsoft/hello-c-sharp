using System;
using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Persistence.Tests.TestData;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.Database;

public abstract class AbstractRepositoryTest<TRepository, TIdentifiable, TSave>
    where TRepository : IRepository<TIdentifiable, TSave>
    where TIdentifiable : Identifiable
{
    private DatabaseContext _databaseContext = null!;
    protected TRepository ClassUnderTest = default!;
    protected ITestData<TIdentifiable, TSave> TestData = null!;

    [SetUp]
    public void SetUp()
    {
        _databaseContext = new DatabaseContext(new DbContextOptionsBuilder()
            .UseInMemoryDatabase("Filename=TestDatabase.db").Options);
        _databaseContext.Database.EnsureCreated();

        ClassUnderTest = CreateRepository(_databaseContext);
        TestData = CreateTestData(_databaseContext);
    }

    protected abstract TRepository CreateRepository(DatabaseContext databaseContext);
    
    protected abstract ITestData<TIdentifiable, TSave> CreateTestData(DatabaseContext databaseContext);

    [Test]
    public void Create()
    {
        var example = TestData.CreateRandomObject();
        var result = ClassUnderTest.Create(example);

        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        TestData.AssertAreEqual(example, result);
    }

    [Test]
    public void GetById()
    {
        var example = TestData.GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        var result = ClassUnderTest.GetById(example.Id);

        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result.Id);
        TestData.AssertAreEqual(example, result);
    }

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
        var example = TestData.GetExampleObject();
        Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
        var result = ClassUnderTest.FindById(example.Id);

        Assert.NotNull(result);
        Assert.AreEqual(example.Id, result!.Id);
        TestData.AssertAreEqual(example, result);
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

        var example = TestData.GetExampleObject();
        var found = result.Find(c => c.Id.Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        TestData.AssertAreEqual(example, found);
    }

    [Test]
    public void FindByFilterTrue()
    {
        var result = ClassUnderTest.FindByFilter(_ => true);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var example = TestData.GetExampleObject();
        var found = result.Find(c => c.Id.Equals(example.Id));
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found!.Id);
        TestData.AssertAreEqual(example, found);
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
        var example = TestData.GetExampleObject();
        var result = ClassUnderTest.FindByFilter(t => t.Id == example.Id);

        Assert.NotNull(result);
        Assert.AreEqual(1, result.Count);

        var found = result[0];
        Assert.NotNull(found);
        Assert.AreEqual(example.Id, found.Id);
        TestData.AssertAreEqual(example, found);
    }
    
    [Test]
    public void Update()
    {
        var create = ClassUnderTest.Create(TestData.CreateRandomObject());

        var toBeUpdated = TestData.CreateRandomObject();
        var update = ClassUnderTest.Update(create.Id, toBeUpdated);

        Assert.NotNull(update);
        Assert.AreEqual(create.Id, update.Id);
        TestData.AssertAreEqual(toBeUpdated, update);
    }

}