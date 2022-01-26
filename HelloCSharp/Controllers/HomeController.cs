using System;
using System.Web.Mvc;
using HelloCSharp.Models;

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
                ViewBag.Persons = Database.Database.GetInstance().PersonRepository.FindAll();
                ViewBag.Title = "Personen";
            } else{
                ViewBag.Id = id;
                ViewBag.Person = Database.Database.GetInstance().PersonRepository.GetById(Int32.Parse(id));
                ViewBag.Title = ViewBag.Person.Name;
            }
          
            return View();
        }
        
        public ActionResult City(string id)
        {
            if (id == null) {
                ViewBag.Cities = Database.Database.GetInstance().CityRepository.FindAll();
                ViewBag.Title = "Städte";
            } else{
                ViewBag.Id = id;
                ViewBag.City = Database.Database.GetInstance().CityRepository.GetById(Int32.Parse(id));
                ViewBag.Title = ViewBag.City.Name;
            }
          
            return View();
        }
        
        public ActionResult RelationshipType(string id)
        {
            if (id == null)
            {
                ViewBag.RelationshipTypes = RelationshipTypeMethods.Values();
                ViewBag.Title = "Beziehungstypen";
            } else{
                ViewBag.Id = id;
                ViewBag.City = Database.Database.GetInstance().CityRepository.GetById(Int32.Parse(id));
                ViewBag.Title = ViewBag.City.Name;
            }
          
            return View();
        }
    }
}