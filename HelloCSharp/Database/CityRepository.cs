using HelloCSharp.Api.Database;
using HelloCSharp.Api.Database.Entities;
using HelloCSharp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Database;

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