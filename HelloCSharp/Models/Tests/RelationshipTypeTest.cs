using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace HelloCSharp.Models.Tests
{
    [TestFixture]
    public class RelationshipTypeTest
    {
        
        [Test, TestCaseSource("CreateEnumAndString")]
        public void ValueOf(RelationshipType type, string typeAsString)
        {
            Assert.AreEqual(type, RelationshipTypeMethods.ValueOf(typeAsString));
        }
        
        private static IEnumerable<TestCaseData> CreateEnumAndString()
        {
            yield return new TestCaseData(RelationshipType.Partners, "Partners");
                yield return new TestCaseData(RelationshipType.Siblings, "Siblings");
                yield return new TestCaseData(RelationshipType. ParentOf, "ParentOf");
                yield return new TestCaseData(RelationshipType.ChildOf, "ChildOf");
                yield return new TestCaseData(RelationshipType.Hates, "Hates");
        }

        [Test, TestCaseSource("CreateEnumAndString")]
        public void Stringify(RelationshipType type, string typeAsString)
        {
            Assert.AreEqual(typeAsString, type.ToString());
        }
        
        [Test]
        public void OppositeForAll()
        {
            foreach (var relationshipType in RelationshipTypeMethods.Values())
            {
                Assert.AreEqual(RelationshipTypeMethods.Opposite(relationshipType), ((RelationshipType)relationshipType).Opposite());
            }
        }
        
        [Test, TestCaseSource("CreateOpposites")]
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
            yield return new TestCaseData(RelationshipType. ParentOf, RelationshipType.ChildOf);
            yield return new TestCaseData(RelationshipType.ChildOf, RelationshipType.ParentOf);
            yield return new TestCaseData(RelationshipType.Hates, null);
        }
        
        
        [Test]
        public void MessageForAll()
        {
            foreach (var relationshipType in RelationshipTypeMethods.Values())
            {
                Assert.NotNull(relationshipType.Message());
            }
        }

    }
}