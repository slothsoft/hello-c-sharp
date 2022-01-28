using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using HelloCSharp.Database.Entities;
using HelloCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Database
{
    public abstract class AbstractRepository<TEntity, TValue>  : IRepository<TValue>
        where TEntity : IdentifiableEntity
        where TValue : Identifiable
    {
        internal DbSet<TEntity> db;

        public AbstractRepository(DbSet<TEntity> db)
        {
            this.db = db;
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
            return this.db;
        }

        public TValue GetById(int id)
        {
            try
            {
                return ConvertToT(FindAllEntities().Single(c => c.Id.Equals(id)));
            }
            catch (System.InvalidOperationException e)
            {
                // Sequence contains no elements
                throw new ArgumentException("Could not find entity with ID " + id);
            }
        }

        public TValue FindById(int id)
        {
            try
            {
                return GetById(id);
            }
            catch (ArgumentException e)
            {
                return null;
            }
        }
    }
}