namespace HelloCSharp.Api.Models;

public class Identifiable
{
        
    public Identifiable(int id)
    {
        Id = id;
    }

    public int Id { get; }
}