using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Controllers;
public class AppController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Person(int? id)
    {
        if (id != null)
            ViewBag.Id = id;
        return View();
    }
        
    public IActionResult City(int? id)
    {
        if (id != null)
            ViewBag.Id = id;
        return View();
    }
        
    public IActionResult RelationshipType(string? id)
    {
        if (id != null)
            ViewBag.Id = id;
        return View();
    }
}
