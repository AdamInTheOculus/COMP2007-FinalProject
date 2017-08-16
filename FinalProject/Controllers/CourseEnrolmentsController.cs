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
    public class CourseEnrolmentsController : Controller
    {
        private ContextModel db = new ContextModel();

        // GET: CourseEnrolments
        public ActionResult Index()
        {
            var courseEnrolments = db.CourseEnrolments.Include(c => c.Course).Include(c => c.Student);
            return View(courseEnrolments.ToList());
        }

        // GET: CourseEnrolments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseEnrolment courseEnrolment = db.CourseEnrolments.Find(id);
            if (courseEnrolment == null)
            {
                return HttpNotFound();
            }

            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");
            return View();
        }

        // POST: CourseEnrolments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId, StudentId")] CourseEnrolment model)
        {
            if (ModelState.IsValid)
            {
                CourseEnrolment tempModel = db.CourseEnrolments.SingleOrDefault(x => x.CourseId == model.CourseId && x.StudentId == model.StudentId);
                if(tempModel == null)
                {
                    model.CourseEnrolmentId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.EditDate = model.CreateDate;
                    db.CourseEnrolments.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate CourseEnrolment Found");
                }
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", model.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", model.StudentId);
            return View(model);
        }

        // GET: CourseEnrolments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseEnrolment courseEnrolment = db.CourseEnrolments.Find(id);
            if (courseEnrolment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", courseEnrolment.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", courseEnrolment.StudentId);
            return View(courseEnrolment);
        }

        // POST: CourseEnrolments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseEnrolmentId,StudentId,CourseId")] CourseEnrolment model)
        {
            if (ModelState.IsValid)
            {
                CourseEnrolment tempModel = db.CourseEnrolments.Find(model.CourseEnrolmentId);
                if(tempModel != null)
                {
                    CourseEnrolment checkModel = db.CourseEnrolments.SingleOrDefault(x => x.CourseEnrolmentId == model.CourseEnrolmentId &&
                                                                                          x.StudentId == model.StudentId && 
                                                                                          x.CourseId == model.CourseId);
                    if(checkModel == null)
                    {
                        tempModel.CourseId = model.CourseId;
                        tempModel.StudentId = model.StudentId;
                        tempModel.EditDate = DateTime.Now;

                        db.Entry(tempModel).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicated CourseEnrolment detected.");
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", model.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", model.StudentId);
            return View(model);
        }

        // GET: CourseEnrolments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseEnrolment courseEnrolment = db.CourseEnrolments.Find(id);
            if (courseEnrolment == null)
            {
                return HttpNotFound();
            }
            return View(courseEnrolment);
        }

        // POST: CourseEnrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CourseEnrolment courseEnrolment = db.CourseEnrolments.Find(id);
            db.CourseEnrolments.Remove(courseEnrolment);
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
