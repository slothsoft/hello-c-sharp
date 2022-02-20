using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Api.Tests;

public static class RelationshipExtensions
{
    
    public static void AssertAreEqual(this Relationship expected, Relationship actual)
    {
        Assert.AreEqual(expected.Type, actual.Type);
        Assert.AreEqual(expected.FromName, actual.FromName);
        Assert.AreEqual(expected.FromId, actual.FromId);
        Assert.AreEqual(expected.ToName, actual.ToName);
        Assert.AreEqual(expected.ToId, actual.ToId);
    }
    
    public static void AssertAreEqual(this SaveRelationship expected, Relationship actual)
    {
        Assert.AreEqual(expected.Type, actual.Type);
        Assert.AreEqual(expected.FromId, actual.FromId);
        Assert.AreEqual(expected.ToId, actual.ToId);
    }
    
    public static Relationship CreateExampleObject()
    {
        return new Relationship(1, RelationshipType.Siblings, 1, "Vi", 2, "Powder");
    }
}