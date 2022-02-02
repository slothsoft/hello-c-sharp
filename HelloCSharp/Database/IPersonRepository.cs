using System.Collections.Generic;
using HelloCSharp.Models;

namespace HelloCSharp.Database;

public interface IPersonRepository : IRepository<Person>
{

    List<Person> FindByCityId(int cityId);

}