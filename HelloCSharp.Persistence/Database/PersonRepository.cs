using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public class PersonRepository : AbstractRepository<PersonEntity, Person>, IPersonRepository, IRepository<Person>
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
        return Db.Include(p => p.City);
    }
       
    public List<Person> FindByCityId(int cityId)
    {
        return FindByFilter(p => p.City.Id == cityId);
    }
}