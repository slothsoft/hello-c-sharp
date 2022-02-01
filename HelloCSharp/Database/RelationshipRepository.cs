using System.Collections.Generic;
using HelloCSharp.Database.Entities;
using HelloCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Database;

public class RelationshipRepository : AbstractRepository<RelationshipEntity, Relationship>
{
    public RelationshipRepository(DbSet<RelationshipEntity> db) : base(db)
    {
    }

    protected override Relationship ConvertToT(RelationshipEntity entity)
    {
        return entity.ConvertToRelationship();
    }
        
    protected override IEnumerable<RelationshipEntity> FindAllEntities()
    {
        return Db.Include(p => p.From).Include(p => p.To);
    }

    internal List<Relationship> FindAllIncludingOpposites()
    {
        var baseResult= FindAll();
        var result = new List<Relationship>(baseResult);
        foreach (var relationship in baseResult)
        {
            var opposite = relationship.Type.Opposite();
            if (opposite != null)
            {
                result.Add(new Relationship(relationship.Id,(RelationshipType) opposite, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName));
            }
        }
        return result;
    }

    public List<Relationship> FindByPersonId(int personId)
    {
        return FindAllIncludingOpposites().FindAll(r => r.FromId.Equals(personId) || (r.ToId.Equals(personId) && !r.Type.Opposite().HasValue));
    }

    public List<Relationship> FindByType(RelationshipType relationshipType)
    {
        return FindAllIncludingOpposites().FindAll(r => r.Type == relationshipType);
    }
}