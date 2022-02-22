namespace HelloCSharp.Api.Models;

public class Relationship : Identifiable
{
    public Relationship(int id, RelationshipType type, int fromId, string fromName,  int toId, string toName) : base(id)
    {
        Type = type;
        FromId = fromId;
        FromName = fromName;
        ToId = toId;
        ToName = toName;
    }
        
    public RelationshipType Type { get; }

    public int FromId { get; }
        
    public string FromName { get; }
        
    public int ToId { get; }
        
    public string ToName { get; }

    public override string ToString() => $"Relationship {FromName} {Type} {ToName}";
}