using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class StudentsController : Controller
    {
        private ContextModel db = new ContextModel();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Student model)
        {
            if (ModelState.IsValid)
            {
                Student checkModel = db.Students.SingleOrDefault(x => x.Name == model.Name);

                if(checkModel == null)
                {
                    model.StudentId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.EditDate = model.CreateDate;
                    db.Students.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Name,Average")] Student model)
        {
            if (ModelState.IsValid)
            {
                Student tempModel = db.Students.Find(model.StudentId);
                if(tempModel != null)
                {
                    Student checkModel = db.Students.SingleOrDefault(x => x.Name == model.Name && x.Average == model.Average);

                    if(checkModel == null)
                    {
                        tempModel.Name = model.Name;
                        tempModel.Average = model.Average;
                        tempModel.EditDate = DateTime.Now;

                        db.Entry(tempModel).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicated Student detected.");
                    }
                }
                else
                {
                    return Content("tempModel not found! " + model.Name);
                }
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(model);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
