using System.Collections.Generic;
using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Api.Tests.Models;

[TestFixture]
internal class RelationshipTypeTest
{
    [Test, TestCaseSource(nameof(CreateEnumAndString))]
    public void ValueOf(RelationshipType type, string typeAsString)
    {
        Assert.AreEqual(type, RelationshipTypeMethods.ValueOf(typeAsString));
    }

    private static IEnumerable<TestCaseData> CreateEnumAndString()
    {
        yield return new TestCaseData(RelationshipType.Partners, "Partners");
        yield return new TestCaseData(RelationshipType.Siblings, "Siblings");
        yield return new TestCaseData(RelationshipType.ParentOf, "ParentOf");
        yield return new TestCaseData(RelationshipType.ChildOf, "ChildOf");
        yield return new TestCaseData(RelationshipType.Hates, "Hates");
    }

    [Test, TestCaseSource(nameof(CreateEnumAndString))]
    public void Stringify(RelationshipType type, string typeAsString)
    {
        Assert.AreEqual(typeAsString, type.ToString());
    }

    [Test]
    public void OppositeForAll()
    {
        foreach (var relationshipType in RelationshipTypeMethods.Values())
        {
            Assert.DoesNotThrow(() => relationshipType.Opposite());
        }
    }

    [Test, TestCaseSource(nameof(CreateOpposites))]
    public void Opposite(RelationshipType relationshipType, RelationshipType? opposite)
    {
        Assert.AreEqual(opposite, relationshipType.Opposite());
        if (opposite != null)
        {
            Assert.AreEqual(relationshipType, opposite.Value.Opposite());
        }
    }

    private static IEnumerable<TestCaseData> CreateOpposites()
    {
        yield return new TestCaseData(RelationshipType.Partners, RelationshipType.Partners);
        yield return new TestCaseData(RelationshipType.Siblings, RelationshipType.Siblings);
        yield return new TestCaseData(RelationshipType.ParentOf, RelationshipType.ChildOf);
        yield return new TestCaseData(RelationshipType.ChildOf, RelationshipType.ParentOf);
        yield return new TestCaseData(RelationshipType.Hates, null);
    }


    [Test]
    public void MessageForAll([Values] RelationshipType relationshipType)
    {
        AssertIsCorrectlyTranslated(relationshipType, relationshipType.GetMessage());
    }

    private static void AssertIsCorrectlyTranslated(RelationshipType relationshipType, string translation)
    {
        Assert.NotNull(translation);
        Assert.IsNotEmpty(translation);
    }

    [Test]
    public void DisplayNameForAll([Values] RelationshipType relationshipType)
    {
        AssertIsCorrectlyTranslated(relationshipType, relationshipType.GetDisplayName());
    }
}