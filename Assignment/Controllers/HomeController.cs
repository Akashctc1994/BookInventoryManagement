using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Assignment.Controllers
{
 
    public class HomeController : Controller
    {
        DataContext dc = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNew(Books obj)
        {
            dc.InsertRec(obj);
            return RedirectToAction("Modify");
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string Titles, string Author, long ISBN)
        {
            List<Books> objLst = dc.SearchBooks(Titles,Author,ISBN);
            return View(objLst);
        }
        public ActionResult Details(int id)
        {
            Books obj = dc.GetBooks(id);
            return View(obj);
        }
        public ActionResult Modify()
        {
            List<Books> objLst = dc.GetBook();
            return View(objLst);
        }
        public ActionResult Edit(int id)
        {
            Books obj = dc.GetBooks(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Books obj)
        {
            dc.updateRec(obj);
            return RedirectToAction("Modify");
        }
        public ActionResult Delete(int id)
        {
            Books obj = dc.GetBooks(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            int n = Convert.ToInt32(id);
            dc.DeleteRec(n);
            return RedirectToAction("AddNew");
        }
    }
}