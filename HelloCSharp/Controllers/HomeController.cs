using System;
using System.Web.Mvc;

namespace HelloCSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Person(string id)
        {
            if (id == null) {
                ViewBag.Persons = Database.Database.GetInstance().FindAllPersons();
                ViewBag.Title = "Personen";
            } else{
                ViewBag.Id = id;
                ViewBag.Person = Database.Database.GetInstance().GetPerson(Int32.Parse(id));
                ViewBag.Title = ViewBag.Person.Name;
            }
          
            return View();
        }
        
        public ActionResult City(string id)
        {
            if (id == null) {
                ViewBag.Cities = Database.Database.GetInstance().FindAllCities();
                ViewBag.Title = "Städte";
            } else{
                ViewBag.Id = id;
                ViewBag.City = Database.Database.GetInstance().GetCity(Int32.Parse(id));
                ViewBag.Title = ViewBag.City.Name;
            }
          
            return View();
        }
    }
}