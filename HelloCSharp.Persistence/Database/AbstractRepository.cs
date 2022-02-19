using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public abstract class AbstractRepository<TEntity, TValue>  : IRepository<TValue>
    where TEntity : IdentifiableEntity
    where TValue : Identifiable
{
    private readonly DatabaseContext _context;
    internal readonly DbSet<TEntity> Db;

    protected AbstractRepository(DatabaseContext context, DbSet<TEntity> db)
    {
        _context = context;
        Db = db;
    }

    public TValue Create(TValue value)
    {
        var result = Db.Add(ConvertToEntity(value));
        _context.SaveChanges();
        return GetById(result.Entity.Id!.Value);
    }

    protected abstract TEntity ConvertToEntity(TValue value, TEntity? result = null);

    public List<TValue> FindByFilter(Predicate<TValue> filter)
    {
        return FindAll().FindAll(filter);
    }

    public List<TValue> FindAll()
    {
        return FindAllEntities().Select(ConvertToT).ToList();
    }
    
    protected abstract TValue ConvertToT(TEntity entity);

    protected virtual IEnumerable<TEntity> FindAllEntities()
    {
        return Db;
    }

    public TValue GetById(int id)
    {
        try
        {
            return ConvertToT(GetEntityById(id));
        }
        catch (InvalidOperationException e)
        {
            // Sequence contains no elements
            throw new ArgumentException("Could not find entity with ID " + id, e);
        }
    }

    private TEntity GetEntityById(int id)
    {
        return FindAllEntities().Single(c => c.Id.Equals(id));
    }

    public TValue? FindById(int id)
    {
        try
        {
            return GetById(id);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
    
    public TValue Update(TValue value)
    {
        Db.Update(ConvertToEntity(value, GetEntityById(value.Id)));
        _context.SaveChanges();
        return GetById(value.Id);
    }
}