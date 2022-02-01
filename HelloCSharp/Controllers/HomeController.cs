using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloCSharp.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Person(int? id)
    {
        ViewBag.Id = id;
        return View();
    }
        
    public IActionResult City(int? id)
    {
        ViewBag.Id = id;
        return View();
    }
        
    public IActionResult RelationshipType(string id)
    {
        ViewBag.Id = id;
        return View();
    }
}
