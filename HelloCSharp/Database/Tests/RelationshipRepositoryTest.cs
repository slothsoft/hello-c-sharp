using System;
using System.Collections.Generic;
using System.Linq;
using HelloCSharp.Models;
using NUnit.Framework;

namespace HelloCSharp.Database.Tests;

[TestFixture]
public class RelationshipRepositoryTest : AbstractRepositoryTest<RelationshipRepository, Relationship>
{
    protected override RelationshipRepository CreateRepository(Database database)
    {
        return new RelationshipRepository(database.Relationship);
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
         * Relationships - in contrary to other database entities - can be symmetrical. If A is sibling to B,
         * then B is sibling to A. These tests make sure of it.
         */
        
        
    [Test, TestCaseSource(nameof(CreateSymmetricalRelationships))]
    public void FindAllIncludingOppositesSymmetricalRelationships(Relationship relationship)
    {
        var result = ClassUnderTest.FindAllIncludingOpposites();

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1); // every table has example rows

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(2, found.Count); // since symmetrical, should be two

        AssertAreEqual(relationship, found[0]);
        AssertAreEqual(new Relationship(-relationship.Id, relationship.Type, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName), found[1]);
    }

    private static Predicate<Relationship> HasId(int? id)
    {
        return c => c.Id.Equals(id);
    }

    private static IEnumerable<TestCaseData> CreateSymmetricalRelationships()
    {
        yield return new TestCaseData(new Relationship(1, RelationshipType.Siblings, 1, "Vi", 2, "Powder"));
        yield return new TestCaseData(new Relationship(2, RelationshipType.Partners, 1, "Vi", 3, "Caitlyn"));
    }

    [Test, TestCaseSource(nameof(CreateSymmetricalRelationships))]
    public void FindByPersonIdFromSymmetricalRelationships(Relationship relationship)
    {
        var result = ClassUnderTest.FindByPersonId(relationship.FromId);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(1, found.Count); 

        AssertAreEqual(relationship, found[0]);
    }
        
    [Test, TestCaseSource(nameof(CreateSymmetricalRelationships))]
    public void FindByPersonIdToSymmetricalRelationships(Relationship relationship)
    {
        var result = ClassUnderTest.FindByPersonId(relationship.ToId);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(1, found.Count); 

        AssertAreEqual(new Relationship(-relationship.Id, relationship.Type, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName), found[0]);
    }

    /**
         * Some relationships are kinda symmetrical. If A is parent to B,
         * then B is child to A. These tests make sure of it.
         */
        
    
    [Test, TestCaseSource(nameof(CreatePseudoSymmetricalRelationships))]
    public void FindAllIncludingOppositesPseudoSymmetricalRelationships(Relationship relationship, RelationshipType oppositeType)
    {
        var result = ClassUnderTest.FindAllIncludingOpposites();

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
        
    [Test, TestCaseSource(nameof(CreatePseudoSymmetricalRelationships))]
    public void FindByPersonIdFromPseudoSymmetricalRelationships(Relationship relationship, RelationshipType oppositeType)
    {
        var result = ClassUnderTest.FindByPersonId(relationship.FromId);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(1, found.Count); 

        AssertAreEqual(relationship, found[0]);
    }
        
    [Test, TestCaseSource(nameof(CreatePseudoSymmetricalRelationships))]
    public void FindByPersonIdToPseudoSymmetricalRelationships(Relationship relationship, RelationshipType oppositeType)
    {
        var result = ClassUnderTest.FindByPersonId(relationship.ToId);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(1, found.Count);

        AssertAreEqual(CreateOppositeRelationship(relationship, oppositeType), found[0]);
    }

    private static Relationship CreateOppositeRelationship(Relationship relationship, RelationshipType oppositeType)
    {
        return new Relationship(relationship.Id, oppositeType, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName);
    }

    /**
         * Some relationships are not symmetrical. If A is hates B,
         * then B does not have to hate A. These tests make sure of it.
         */
        
    
    [Test, TestCaseSource(nameof(CreateNonSymmetricalRelationships))]
    public void FindAllIncludingOppositesNonSymmetricalRelationships(Relationship relationship)
    {
        var result = ClassUnderTest.FindAllIncludingOpposites();

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
        
    [Test, TestCaseSource(nameof(CreateNonSymmetricalRelationships))]
    public void FindByPersonIdFromNonSymmetricalRelationships(Relationship relationship)
    {
        var result = ClassUnderTest.FindByPersonId(relationship.FromId);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(1, found.Count);

        AssertAreEqual(relationship, found[0]);
    }
        
    [Test, TestCaseSource(nameof(CreateNonSymmetricalRelationships))]
    public void FindByPersonIdToNonSymmetricalRelationships(Relationship relationship)
    {
        var result = ClassUnderTest.FindByPersonId(relationship.ToId);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.AreEqual(1, found.Count);

        AssertAreEqual(relationship, found[0]);
    }
        
    /**
         * Fetch relationships by type. #CreateAllRelationships() should have a relationship of each type
         */
        
    [Test, TestCaseSource(nameof(CreateAllRelationships))]
    public void FindByType(Relationship relationship)
    {
        var result = ClassUnderTest.FindByType(relationship.Type);

        Assert.NotNull(result);
        Assert.IsTrue(result.Count >= 1);

        var found = result.FindAll(HasId(relationship.Id));
        Assert.NotNull(found);
        Assert.IsTrue(found.Count >= 1);

        AssertAreEqual(relationship, found[0]);
    }
        
    private static IEnumerable<TestCaseData> CreateAllRelationships()
    {
        return CreateSymmetricalRelationships()
            .Concat(CreatePseudoSymmetricalRelationships().SelectMany(SeparateRelationships))
            .Concat(CreateNonSymmetricalRelationships());
    }

    private static IEnumerable<TestCaseData> SeparateRelationships(TestCaseData data)
    {
        // CreatePseudoSymmetricalRelationships() has only one relationship, we need to build the other one
        var relationship = (Relationship) data.Arguments[0];
        yield return new TestCaseData(relationship);
        yield return new TestCaseData(CreateOppositeRelationship(relationship, (RelationshipType) data.Arguments[1]));
    }
}