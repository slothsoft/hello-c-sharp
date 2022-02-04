using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface IRelationshipRepository : IRepository<Relationship>
{
    public List<Relationship> FindByPersonId(int personId);

    public List<Relationship> FindByType(RelationshipType relationshipType);
}