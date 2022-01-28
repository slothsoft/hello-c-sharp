using System.Collections.Generic;
using System.Linq;
using HelloCSharp.Database.Entities;
using HelloCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Database
{
    public class PersonRepository : AbstractRepository<PersonEntity, Person>
    {
    
        public PersonRepository(DbSet<PersonEntity> db) : base(db)
        {
        }

        protected override Person ConvertToT(PersonEntity entity)
        {
            return entity.ConvertToPerson();
        }
        
        protected override IEnumerable<PersonEntity> FindAllEntities()
        {
            return this.db.Include(p => p.City);
        }
       
        public List<Person> FindByCityId(int cityId)
        {
            return FindByFilter(p => p.City.Id == cityId);
        }
    }
}