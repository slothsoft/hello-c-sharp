using System.ComponentModel.DataAnnotations;
using HelloCSharp.Api.Models;

namespace HelloCSharp.Persistence.Entities;

public class PersonEntity : IdentifiableEntity
{
    internal Person ToPerson()
    {
        return new Person(Id!.Value, Name!, Age!.Value, City!.ToCity());    
    }
    
    internal void FromPerson(SavePerson person)
    {
        Name = person.Name;
        Age = person.Age;
        CityId = person.CityId;    
    }
        
    [Required]
    public string? Name { get; set;}
        
    [Required]
    public int? Age { get; set; }
        
    [Required]
    public int? CityId { get; set; }
        
    public CityEntity? City { get; set; }

    public override string ToString() => $"PersonEntity {Id}";
}