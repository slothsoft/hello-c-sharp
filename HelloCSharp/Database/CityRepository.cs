using HelloCSharp.Database.Entities;
using HelloCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Database;

public class CityRepository : AbstractRepository<CityEntity, City>, ICityRepository
{

    public CityRepository(DbSet<CityEntity> db) : base(db)
    {
    }

    protected override  City ConvertToT(CityEntity entity)
    {
        return entity.ConvertToCity();
    }

}