using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_System.DAL;
using Library_System.Models;

namespace Library_System.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Students
        public ActionResult Index(string attributes, string searchString)
        {
            IQueryable<Student> results = null;

            switch (attributes)
            {
                case "First Name":
                    results = db.UserBases.OfType<Student>().Where(p => p.FirstName.Contains(searchString));
                    break;
                case "Last Name":
                    results = db.UserBases.OfType<Student>().Where(p => p.LastName.Contains(searchString));
                    break;
                case "Id":
                    results = db.UserBases.OfType<Student>().Where(p => p.ClientId.Equals(searchString));
                    break;
            }

            if (String.IsNullOrEmpty(attributes))
            {
                results = db.UserBases.OfType<Student>();
            }

            var students = results.ToList();

            var attributeList = new List<string>();
            attributeList.Add("First Name");
            attributeList.Add("Last Name");
            attributeList.Add("Id");

            ViewBag.Attributes = new SelectList(attributeList);
            //            return View();
            //            ICollection<UserBase> users = db.UserBases.ToList();
            //            ICollection<Student> students = new List<Student>();
            //
            //            foreach (UserBase user in users)
            //            {
            //                // need to check discriminator type, since UserBases contains Student and Falculty
            //                if (user.GetType().Name.Equals("Student"))
            //                {
            //                    students.Add((Student) user);
            //                }
            //            }

            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = (Student)db.UserBases.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,ClientId")] Student student)
        {
            if (ModelState.IsValid)
            {
                var studentExist = 
                    db.UserBases.OfType<Student>()
                    .Where(s => s.ClientId == student.ClientId)
                    .FirstOrDefault();

                if (studentExist == null)
                {
                    db.UserBases.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "This client ID is already taken.");
                    return View();
                }
                
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = (Student)db.UserBases.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,StudentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = (Student)db.UserBases.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = (Student)db.UserBases.Find(id);
            db.UserBases.Remove(student);
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
