using System;
using System.Collections.Generic;
using System.Linq;
using HelloCSharp.Api.Database;
using HelloCSharp.Api.Database.Entities;
using HelloCSharp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Database;

public abstract class AbstractRepository<TEntity, TValue>  : IRepository<TValue>
    where TEntity : IdentifiableEntity
    where TValue : Identifiable
{
    internal readonly DbSet<TEntity> Db;

    protected AbstractRepository(DbSet<TEntity> db)
    {
        Db = db;
    }

    protected abstract TValue ConvertToT(TEntity entity);

    public List<TValue> FindByFilter(Predicate<TValue> filter)
    {
        return FindAll().FindAll(filter);
    }

    public List<TValue> FindAll()
    {
        return FindAllEntities().Select(ConvertToT).ToList();
    }

    protected virtual IEnumerable<TEntity> FindAllEntities()
    {
        return Db;
    }

    public TValue GetById(int id)
    {
        try
        {
            return ConvertToT(FindAllEntities().Single(c => c.Id.Equals(id)));
        }
        catch (InvalidOperationException e)
        {
            // Sequence contains no elements
            throw new ArgumentException("Could not find entity with ID " + id, e);
        }
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
}