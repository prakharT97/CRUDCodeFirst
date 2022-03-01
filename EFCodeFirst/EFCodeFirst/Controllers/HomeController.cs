using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFCodeFirst.Models;

namespace EFCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db = new StudentContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid == true) {
                db.Students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.InsertMessage = "<script>alert('Data isnerted')</Script
                    TempData["InsertMessage"] = "<script>alert('Data isnerted')</Script>";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    //ViewBag.InsertMessage = "<script>alert('Data not isnerted')</Script>";
                }

            }
            
            return View();
        }

        public ActionResult Edit(int id) {

            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(row);
        
        }

        [HttpPost]
        public ActionResult Edit(Student s) {

            db.Entry(s).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a>0) {

                ViewBag.UpdateMessage = "<script>alert('Data updated')</Script>";

            }
            return View();
        }

        public ActionResult Delete(int id) {

            var StudentIdRow = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(StudentIdRow);
        
        }

        [HttpPost]
        public ActionResult Delete(Student s) {

            db.Entry(s).State = EntityState.Deleted;
            int a = db.SaveChanges();
            if (a > 0) {

                TempData["DeleteMessage"] = "<script>alert('Data deleted')</Script>";
            }
            return View();
        }
    }
}