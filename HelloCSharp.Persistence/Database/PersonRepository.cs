using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public class PersonRepository : AbstractRepository<PersonEntity, Person, SavePerson>, IPersonRepository
{
    
    public PersonRepository(DatabaseContext context, DbSet<PersonEntity> db) : base(context, db)
    {
    }

    protected override Person ConvertToT(PersonEntity entity) => entity.ToPerson();

    protected override PersonEntity ConvertToEntity(SavePerson value, PersonEntity? entity = null)
    {
        var result = entity ?? new PersonEntity();
        result.FromPerson(value);
        return result;
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