namespace HelloCSharp.Api.Models;


public static class RelationshipTypeMethods
{
        
    public static RelationshipType ValueOf(string value)
    {
        return (RelationshipType) Enum.Parse(typeof(RelationshipType), Convert.ToString(value));
    }

    public static IEnumerable<RelationshipType> Values()
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
        
    public static string GetMessage(this RelationshipType relationshipType)
    {
        return relationshipType switch
        {
            RelationshipType.Partners => Resources.strings.RelationshipType_Partners_Message,
            RelationshipType.Siblings => Resources.strings.RelationshipType_Siblings_Message,
            RelationshipType.ParentOf => Resources.strings.RelationshipType_Partners_Message,
            RelationshipType.ChildOf => Resources.strings.RelationshipType_ChildOf_Message,
            RelationshipType.Hates => Resources.strings.RelationshipType_Hates_Message,
            _ => throw new ArgumentException("Unknown relationship type: " + relationshipType)
        };
    }
        
    public static string GetDisplayName(this RelationshipType relationshipType)
    {
        return relationshipType switch
        {
            RelationshipType.Partners => Resources.strings.RelationshipType_Partners_DisplayName,
            RelationshipType.Siblings => Resources.strings.RelationshipType_Siblings_DisplayName,
            RelationshipType.ParentOf => Resources.strings.RelationshipType_Partners_DisplayName,
            RelationshipType.ChildOf => Resources.strings.RelationshipType_ChildOf_DisplayName,
            RelationshipType.Hates => Resources.strings.RelationshipType_Hates_DisplayName,
            _ => throw new ArgumentException("Unknown relationship type: " + relationshipType)
        };
    }
}