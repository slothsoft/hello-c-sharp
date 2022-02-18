using HelloCSharp.Api.Models;

namespace HelloCSharp.Persistence.Entities;

public class CityEntity : IdentifiableEntity
{
    internal City ConvertToCity()
    {
        return new City(Id, Name);    
    }

    public string Name { get; set; }

    public override string ToString() => $"CityEntity {Id}";
}