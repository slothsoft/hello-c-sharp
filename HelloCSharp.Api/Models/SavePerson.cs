namespace HelloCSharp.Api.Models;

public class SavePerson
{

    public string Name { get; init;  }
    public int Age { get; init; }
    public int CityId { get; init; }

    public override string ToString() => $"SavePerson {Name}";
}