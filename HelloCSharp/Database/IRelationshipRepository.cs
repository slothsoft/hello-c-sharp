using System.Collections.Generic;
using HelloCSharp.Models;

namespace HelloCSharp.Database;

public interface IRelationshipRepository : IRepository<Relationship>
{
    public List<Relationship> FindByPersonId(int personId);

    public List<Relationship> FindByType(RelationshipType relationshipType);
}