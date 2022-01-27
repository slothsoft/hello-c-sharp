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

        public ActionResult Person(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        
        public ActionResult City(int? id)
        {
            ViewBag.Id = id;
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