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
        
    public static string Message(this RelationshipType relationshipType)
    {
        return relationshipType switch
        {
            RelationshipType.Partners => " ist der Partner von ",
            RelationshipType.Siblings => " ist das Geschwister von ",
            RelationshipType.ParentOf => " ist das Elternteil von ",
            RelationshipType.ChildOf => " ist das Kind von ",
            RelationshipType.Hates => " hasst ",
            _ => throw new ArgumentException("Unknown relationship type: " + relationshipType)
        };
    }
        
    public static string DisplayName(this RelationshipType relationshipType)
    {
        return relationshipType switch
        {
            RelationshipType.Partners => "Partner",
            RelationshipType.Siblings => "Geschwister",
            RelationshipType.ParentOf => "Eltern",
            RelationshipType.ChildOf => "Kind",
            RelationshipType.Hates => "Feindschaft",
            _ => throw new ArgumentException("Unknown relationship type: " + relationshipType)
        };
    }
}