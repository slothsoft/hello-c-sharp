using System;
using System.Collections.Generic;
using System.Data.SQLite;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests
{
    [TestFixture]
    public class RelationshipRepositoryTest : AbstractRepositoryTest<RelationshipRepository, Relationship>
    {
        protected override RelationshipRepository CreateRepository(SQLiteConnection connection)
        {
            return new RelationshipRepository(connection);
        }

        protected override Relationship GetExampleObject()
        {
            return new Relationship(1, RelationshipType.Siblings, 1, "Vi", 2, "Powder");
        }

        protected override void AssertAreEqual(Relationship expected, Relationship actual)
        {
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.FromName, actual.FromName);
            Assert.AreEqual(expected.FromId, actual.FromId);
            Assert.AreEqual(expected.ToName, actual.ToName);
            Assert.AreEqual(expected.ToId, actual.ToId);
        }
        
        /**
         * Relationships - in contrary to other database entities - can be symmentrical. If A is sibling to B,
         * then B is sibling to A. These tests make sure of it.
         */
        
        
        [Test, TestCaseSource("CreateSymmetricalRelationships")]
        public void FindAllIncludingOppositesSymmetricalRelationships(Relationship relationship)
        {
            var result = _classUnderTest.FindAllIncludingOpposites();

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1); // every table has example rows

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(2, found.Count); // since symmetrical, should be two

            AssertAreEqual(relationship, found[0]);
            AssertAreEqual(new Relationship(-relationship.Id, relationship.Type, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName), found[1]);
        }

        private Predicate<Relationship> HasId(int? id)
        {
            return c => c.Id.Equals(id);
        }

        private static IEnumerable<TestCaseData> CreateSymmetricalRelationships()
        {
            yield return new TestCaseData(new Relationship(1, RelationshipType.Siblings, 1, "Vi", 2, "Powder"));
            yield return new TestCaseData(new Relationship(2, RelationshipType.Partners, 1, "Vi", 3, "Caitlyn"));
        }

        [Test, TestCaseSource("CreateSymmetricalRelationships")]
        public void FindByPersonIdFromSymmetricalRelationships(Relationship relationship)
        {
            var result = _classUnderTest.FindByPersonId(relationship.FromId);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1);

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count); 

            AssertAreEqual(relationship, found[0]);
        }
        
        [Test, TestCaseSource("CreateSymmetricalRelationships")]
        public void FindByPersonIdToSymmetricalRelationships(Relationship relationship)
        {
            var result = _classUnderTest.FindByPersonId(relationship.ToId);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1);

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count); 

            AssertAreEqual(new Relationship(-relationship.Id, relationship.Type, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName), found[0]);
        }

        /**
         * Some relationships are kinda symmentrical. If A is parent to B,
         * then B is child to A. These tests make sure of it.
         */
        
    
        [Test, TestCaseSource("CreatePseudoSymmetricalRelationships")]
        public void FindAllIncludingOppositesPseudoSymmetricalRelationships(Relationship relationship, RelationshipType oppositeType)
        {
            var result = _classUnderTest.FindAllIncludingOpposites();

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1); // every table has example rows

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(2, found.Count); // since symmetrical, should be two

            AssertAreEqual(relationship, found[0]);
            AssertAreEqual(new Relationship(relationship.Id, oppositeType, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName), found[1]);
        }
    
        private static IEnumerable<TestCaseData> CreatePseudoSymmetricalRelationships()
        {
            yield return new TestCaseData(new Relationship(4, RelationshipType.ParentOf, 4, "Silco", 2, "Powder"), RelationshipType.ChildOf);
        }
        
        [Test, TestCaseSource("CreatePseudoSymmetricalRelationships")]
        public void FindByPersonIdFromPseudoSymmetricalRelationships(Relationship relationship, RelationshipType oppositeType)
        {
            var result = _classUnderTest.FindByPersonId(relationship.FromId);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1);

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count); 

            AssertAreEqual(relationship, found[0]);
        }
        
        [Test, TestCaseSource("CreatePseudoSymmetricalRelationships")]
        public void FindByPersonIdToPseudoSymmetricalRelationships(Relationship relationship, RelationshipType oppositeType)
        {
            var result = _classUnderTest.FindByPersonId(relationship.ToId);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1);

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count);

            AssertAreEqual(new Relationship(relationship.Id, oppositeType, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName), found[0]);
        }
        
        /**
         * Some relationships are not symmentrical. If A is hates B,
         * then B does not have to hate A. These tests make sure of it.
         */
        
    
        [Test, TestCaseSource("CreateNonSymmetricalRelationships")]
        public void FindAllIncludingOppositesNonSymmetricalRelationships(Relationship relationship)
        {
            var result = _classUnderTest.FindAllIncludingOpposites();

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1); // every table has example rows

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count); 

            AssertAreEqual(relationship, found[0]);
        }
    
        private static IEnumerable<TestCaseData> CreateNonSymmetricalRelationships()
        {
            yield return new TestCaseData(new Relationship(3, RelationshipType.Hates, 2, "Powder", 3, "Caitlyn"));
        }
        
        [Test, TestCaseSource("CreateNonSymmetricalRelationships")]
        public void FindByPersonIdFromNonSymmetricalRelationships(Relationship relationship)
        {
            var result = _classUnderTest.FindByPersonId(relationship.FromId);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1);

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count);

            AssertAreEqual(relationship, found[0]);
        }
        
        [Test, TestCaseSource("CreateNonSymmetricalRelationships")]
        public void FindByPersonIdToNonSymmetricalRelationships(Relationship relationship)
        {
            var result = _classUnderTest.FindByPersonId(relationship.ToId);

            Assert.NotNull(result);
            Assert.IsTrue(result.Count >= 1);

            var found = result.FindAll(HasId(relationship.Id));
            Assert.NotNull(found);
            Assert.AreEqual(1, found.Count);

            AssertAreEqual(relationship, found[0]);
        }
    }
}