using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Controllers;
public class HomeController : Controller
{
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
