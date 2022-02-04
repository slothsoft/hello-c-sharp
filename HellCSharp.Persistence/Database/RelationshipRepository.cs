using HellCSharp.Persistence.Database.Entities;
using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HellCSharp.Persistence.Database;

public class RelationshipRepository : AbstractRepository<RelationshipEntity, Relationship>, IRelationshipRepository, IRepository<Relationship>
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
        result.AddRange(from relationship in baseResult let opposite = relationship.Type.Opposite() where opposite != null select new Relationship(relationship.Id, (RelationshipType) opposite, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName));
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