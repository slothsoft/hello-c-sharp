using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public class RelationshipRepository : AbstractRepository<RelationshipEntity, Relationship>, IRelationshipRepository
{
    public RelationshipRepository(DatabaseContext context, DbSet<RelationshipEntity> db) : base(context, db)
    {
    }

    protected override Relationship ConvertToT(RelationshipEntity entity) => entity.ToRelationship();

    protected override RelationshipEntity ConvertToEntity(Relationship value, RelationshipEntity? entity = null)
    {
        var result = entity ?? new RelationshipEntity();
        result.FromRelationship(value);
        return result;
    }

    protected override IEnumerable<RelationshipEntity> FindAllEntities()
    {
        return Db.Include(p => p.From).Include(p => p.To);
    }

    public List<Relationship> FindAllIncludingOpposites()
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