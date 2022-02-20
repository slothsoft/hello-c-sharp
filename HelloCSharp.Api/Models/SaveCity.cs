namespace HelloCSharp.Api.Models;

public class SaveCity
{
    public string Name { get; init; }

    public override string ToString() => $"SaveCity {Name}";
}