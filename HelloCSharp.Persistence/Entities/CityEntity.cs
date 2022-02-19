using System.ComponentModel.DataAnnotations;
using HelloCSharp.Api.Models;

namespace HelloCSharp.Persistence.Entities;

public class CityEntity : IdentifiableEntity
{
    internal City ToCity()
    {
        return new City(Id!.Value, Name!);    
    }
    
    internal void FromCity(City city)
    {
        Name = city.Name;
    }

    [Required]
    public string? Name { get; set; }

    public override string ToString() => $"CityEntity {Id}";
}