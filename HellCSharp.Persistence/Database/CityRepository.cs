using HellCSharp.Persistence.Database.Entities;
using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HellCSharp.Persistence.Database;

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