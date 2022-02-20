using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public class CityRepository : AbstractRepository<CityEntity, City, SaveCity>, ICityRepository
{

    public CityRepository(DatabaseContext context, DbSet<CityEntity> db) : base(context, db)
    {
    }

    protected override City ConvertToT(CityEntity entity) => entity.ToCity();

    protected override CityEntity ConvertToEntity(SaveCity value, CityEntity? entity = null)
    {
        var result = entity ?? new CityEntity();
        result.FromCity(value);
        return result;
    }
}