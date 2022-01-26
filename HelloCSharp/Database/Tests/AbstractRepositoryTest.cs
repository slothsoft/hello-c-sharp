using System;
using System.Data.SQLite;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    public abstract class AbstractRepositoryTest<R, T>
        where R : IRepository<T>
        where T : Identifiable
    {

        
        private SQLiteConnection _connection;
        protected R _classUnderTest;
        
        [SetUp]
        public void SetUp()
        {
            _connection = new SQLiteConnection("Data Source=:memory:");
            _connection.Open();
            Database.InitDatabase(_connection);
                
            _classUnderTest = CreateRepository(_connection);
        }
        
        protected abstract R CreateRepository(SQLiteConnection connection);
        
        [TearDown]
        public void TearDown()
        {
            _connection?.Close();
        }

        [Test]
        public void GetById()
        {
            var example = GetExampleObject();
            Assert.NotNull(example.Id, "ID of example object shouldn't be null!");
            var result = _classUnderTest.GetById((int) example.Id);

            Assert.NotNull(result);
            Assert.AreEqual(example.Id, result.Id);
            AssertAreEqual(example, result);
        }

        protected abstract T GetExampleObject();
        
        protected abstract void AssertAreEqual(T expected, T actual);
        
        [Test]
        public void GetByIdUnknown()
        {
            try
            {
                _classUnderTest.GetById(-1);
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
            var result = _classUnderTest.FindById((int) example.Id);

            Assert.NotNull(result);
            Assert.AreEqual(example.Id, result.Id);
            AssertAreEqual(example, result);
        }
        
        [Test]
        public void FindByIdUnknown()
        {
            Assert.Null(_classUnderTest.FindById(-1));
        }

        [Test]
        public void FindAll()
        {
            var result = _classUnderTest.FindAll();

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
            var result = _classUnderTest.FindByFilter(t => true);

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
            var result = _classUnderTest.FindByFilter(t => false);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public void FindByFilterById()
        {
            var example = GetExampleObject();
            var result = _classUnderTest.FindByFilter(t => t.Id == example.Id);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count);

            var found = result[0];
            Assert.NotNull(found);
            Assert.AreEqual(example.Id, found.Id);
            AssertAreEqual(example, found);
        }
    }
}