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
        public ActionResult Create([Bind(Include = "CourseEnrolmentId,StudentId,CourseId")] CourseEnrolment courseEnrolment)
        {
            if (ModelState.IsValid)
            {
                db.CourseEnrolments.Add(courseEnrolment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", courseEnrolment.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", courseEnrolment.StudentId);
            return View(courseEnrolment);
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
        public ActionResult Edit([Bind(Include = "CourseEnrolmentId,StudentId,CourseId")] CourseEnrolment courseEnrolment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseEnrolment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", courseEnrolment.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", courseEnrolment.StudentId);
            return View(courseEnrolment);
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
