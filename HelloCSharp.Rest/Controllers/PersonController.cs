using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Rest.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonController : AbstractRepositoryController<Person>
    {
        public PersonController(IDatabase database) : base(() => database.PersonRepository)
        {
        }
    }
}