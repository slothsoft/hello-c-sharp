using System.ComponentModel.DataAnnotations;
using HelloCSharp.Api.Models;

namespace HelloCSharp.Persistence.Entities;

public class RelationshipEntity : IdentifiableEntity
{
    internal Relationship ToRelationship()
    {
        return new Relationship(Id!.Value, Type!.Value, FromId!.Value, From!.Name!, ToId!.Value, To!.Name!);    
    }
    
    internal void FromRelationship(SaveRelationship relationship)
    {
        Type =  relationship.Type;
        FromId = relationship.FromId; 
        ToId = relationship.ToId;    
    }
        
    [Required]
    public RelationshipType? Type { get; set; }

    [Required]
    public int? FromId { get; set; }
        
    public PersonEntity? From { get; set; }
        
    [Required]
    public int? ToId { get; set; }
        
    public PersonEntity? To { get; set; }
        
    public override string ToString() => $"RelationshipEntity {Id}";
}