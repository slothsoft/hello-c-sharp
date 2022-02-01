namespace HelloCSharp.Models;

public class City : Identifiable
{
        
    public City(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public override string ToString() => $"City {Name} ({Id})";
}