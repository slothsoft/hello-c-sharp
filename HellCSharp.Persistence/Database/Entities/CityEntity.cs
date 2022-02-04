using HelloCSharp.Api.Models;

namespace HellCSharp.Persistence.Database.Entities;

public class CityEntity : IdentifiableEntity
{
    internal City ConvertToCity()
    {
        return new City(Id, Name);    
    }

    public string Name { get; set; }

    public override string ToString() => $"CityEntity {Id}";
}