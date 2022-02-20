namespace HelloCSharp.Api.Models;

public class SaveRelationship
{
    public RelationshipType Type { get; init; }

    public int FromId { get; init;  }
        
    public int ToId { get; init;  }

    public override string ToString() => $"SaveRelationship {FromId} {Type} {ToId}";
}