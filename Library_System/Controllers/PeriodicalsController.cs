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
    public class PeriodicalsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Periodicals
        public ActionResult Index()
        {
            return View();
//            return View(db.ItemBases.ToList());
        }

        // GET: Periodicals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodical periodical = (Periodical)db.ItemBases.Find(id);
            if (periodical == null)
            {
                return HttpNotFound();
            }
            return View(periodical);
        }

        // GET: Periodicals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Periodicals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Year,Title,Amount,Publisher,Version,Author")] Periodical periodical)
        {
            if (ModelState.IsValid)
            {
                db.ItemBases.Add(periodical);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(periodical);
        }

        // GET: Periodicals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodical periodical = (Periodical)db.ItemBases.Find(id);
            if (periodical == null)
            {
                return HttpNotFound();
            }
            return View(periodical);
        }

        // POST: Periodicals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Year,Title,Amount,Publisher,Version,Author")] Periodical periodical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periodical);
        }

        // GET: Periodicals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodical periodical = (Periodical)db.ItemBases.Find(id);
            if (periodical == null)
            {
                return HttpNotFound();
            }
            return View(periodical);
        }

        // POST: Periodicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Periodical periodical = (Periodical)db.ItemBases.Find(id);
            db.ItemBases.Remove(periodical);
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
