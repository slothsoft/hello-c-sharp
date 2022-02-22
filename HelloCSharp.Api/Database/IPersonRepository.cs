using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface IPersonRepository : IRepository<Person, SavePerson>
{
    List<Person> FindByCityId(int cityId);
}