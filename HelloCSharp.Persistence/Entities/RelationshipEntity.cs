using HelloCSharp.Api.Models;

namespace HelloCSharp.Persistence.Entities;

public class RelationshipEntity : IdentifiableEntity
{
    internal Relationship ConvertToRelationship()
    {
        return new Relationship(Id, Type, FromId, From.Name, ToId, To.Name);    
    }
        
    public RelationshipType Type { get; set; }

    public int FromId { get; set; }
        
    public PersonEntity From { get; set; }
        
    public int ToId { get; set; }
        
    public PersonEntity To { get; set; }
        
    public override string ToString() => $"RelationshipEntity {Id}";
}