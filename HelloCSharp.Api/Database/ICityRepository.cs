using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface ICityRepository : IRepository<City, SaveCity>
{
    
}