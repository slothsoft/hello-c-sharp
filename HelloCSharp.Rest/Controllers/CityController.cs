using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Rest.Controllers;

[ApiController]
[Route("api/cities")]
public class CityController : AbstractRepositoryController<City, SaveCity>
{
    public CityController(ICityRepository cityRepository) : base(() => cityRepository)
    {
    }
}