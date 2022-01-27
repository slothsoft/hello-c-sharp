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
            ViewBag.Id = id;
            return View();
        }
    }
}