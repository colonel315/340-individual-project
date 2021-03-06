﻿using System;
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
    public class FacultiesController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Faculties
        public ActionResult Index(string attributes, string searchString)
        {
            IQueryable<Faculty> results = null;

            switch (attributes)
            {
                case "First Name":
                    results = db.UserBases.OfType<Faculty>().Where(p => p.FirstName.Contains(searchString));
                    break;
                case "Last Name":
                    results = db.UserBases.OfType<Faculty>().Where(p => p.LastName.Contains(searchString));
                    break;
                case "Id":
                    results = db.UserBases.OfType<Faculty>().Where(p => p.ClientId.Equals(searchString));
                    break;
            }

            if (String.IsNullOrEmpty(attributes))
            {
                results = db.UserBases.OfType<Faculty>();
            }

            var faculties = results.ToList();

            var attributeList = new List<string>();
            attributeList.Add("First Name");
            attributeList.Add("Last Name");
            attributeList.Add("Id");

            ViewBag.Attributes = new SelectList(attributeList);
            //            return View();
            //            ICollection<UserBase> users = db.UserBases.ToList();
            //            ICollection<Faculty> faculties = new List<Faculty>();
            //
            //            foreach (UserBase user in users)
            //            {
            //                // need to check discriminator type, since UserBases contains Student and Falculty
            //                if (user.GetType().Name.Equals("Faculty"))
            //                {
            //                    faculties.Add((Faculty)user);
            //                }
            //            }

            return View(faculties);
        }

        // GET: Faculties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = (Faculty)db.UserBases.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Faculties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,ClientId")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                var facultyExist =
                    db.UserBases.OfType<Faculty>()
                        .Where(f => f.ClientId == faculty.ClientId)
                        .FirstOrDefault();

                if (facultyExist == null)
                {
                    db.UserBases.Add(faculty);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "This client ID is already taken.");
                    return View();
                }
            }

            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = (Faculty)db.UserBases.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,FacultyId")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = (Faculty)db.UserBases.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faculty faculty = (Faculty)db.UserBases.Find(id);
            db.UserBases.Remove(faculty);
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
