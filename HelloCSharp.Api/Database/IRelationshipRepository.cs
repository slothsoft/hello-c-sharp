using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface IRelationshipRepository : IRepository<Relationship, SaveRelationship>
{
    public List<Relationship> FindByPersonId(int personId);

    public List<Relationship> FindByType(RelationshipType relationshipType);
}