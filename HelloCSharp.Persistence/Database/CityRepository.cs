using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public class CityRepository : AbstractRepository<CityEntity, City>, ICityRepository, IRepository<City>
{

    public CityRepository(DbSet<CityEntity> db) : base(db)
    {
    }

    protected override  City ConvertToT(CityEntity entity)
    {
        return entity.ConvertToCity();
    }

}