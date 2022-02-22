using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface IRelationshipRepository : IRepository<Relationship, SaveRelationship>
{
    List<Relationship> FindByPersonId(int personId);

    List<Relationship> FindByType(RelationshipType relationshipType);
}