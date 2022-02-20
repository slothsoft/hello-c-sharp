using System.ComponentModel.DataAnnotations;

namespace HelloCSharp.Persistence.Entities;

public class IdentifiableEntity
{
    [Key]
    public int? Id { get; init; }
}