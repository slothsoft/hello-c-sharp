using System;

namespace HelloCSharp.Models
{
    public static class RelationshipTypeMethods
    {
        
        public static RelationshipType ValueOf(string value)
        {
            return (RelationshipType) Enum.Parse(typeof(RelationshipType), Convert.ToString(value));
        }

        public static RelationshipType[] Values()
        {
            return (RelationshipType[]) Enum.GetValues(typeof(RelationshipType));
        }
        
        public static RelationshipType? Opposite(this RelationshipType relationshipType)
        {
            switch (relationshipType)
            {
                case RelationshipType.Partners:
                case RelationshipType.Siblings:
                    return relationshipType;
                
                case RelationshipType.ParentOf:
                    return RelationshipType.ChildOf;
                case RelationshipType.ChildOf:
                    return RelationshipType.ParentOf;
                    
                case RelationshipType.Hates:
                    return null;
                default: 
                    throw new ArgumentException("Unknown relationship type: " + relationshipType);
            }
        }
        
        public static string Message(this RelationshipType relationshipType)
        {
            switch (relationshipType)
            {
                case RelationshipType.Partners:
                    return " ist der Partner von ";
                case RelationshipType.Siblings:
                    return " ist das Geschwister von ";
                case RelationshipType.ParentOf:
                    return " ist das Elternteil von ";
                case RelationshipType.ChildOf:
                    return " ist das Kind von ";
                case RelationshipType.Hates:
                    return " hasst ";
                default: 
                    throw new ArgumentException("Unknown relationship type: " + relationshipType);
            }
        }
    }
}