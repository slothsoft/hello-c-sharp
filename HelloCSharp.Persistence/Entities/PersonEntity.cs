using HelloCSharp.Api.Models;

namespace HelloCSharp.Persistence.Entities;

public class PersonEntity : IdentifiableEntity
{
        
    internal Person ConvertToPerson()
    {
        return new Person(Id, Name, Age, City?.ConvertToCity());    
    }
        
    public string Name { get; set;}
        
    public int Age { get;set; }
        
    public int CityId { get;set; }
        
    public CityEntity City { get;set; }

    public override string ToString() => $"PersonEntity {Id}";
}