using System;
using System.Collections;
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
    public class PeriodicalsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Periodicals
        public ActionResult Index(string attributes, string searchString)
        {
            IQueryable<Periodical> results = null;

            switch (attributes)
            {
                case "Title":
                    results = db.ItemBases.OfType<Periodical>().Where(p => p.Title.Contains(searchString));
                    break;
                case "Author":
                    results = db.ItemBases.OfType<Periodical>().Where(p => p.Author.Contains(searchString));
                    break;
                case "Year":
                    results = db.ItemBases.OfType<Periodical>().Where(p => p.Year.Contains(searchString));
                    break;
            }

            if (String.IsNullOrEmpty(attributes))
            {
                results = db.ItemBases.OfType<Periodical>();
            }

            var periodicals = results.ToList();

            var attributeList = new List<string>();
            attributeList.Add("Title");
            attributeList.Add("Author");
            attributeList.Add("Year");

            ViewBag.Attributes = new SelectList(attributeList);

            //            return View();
//            ICollection<ItemBase> items = db.ItemBases.ToList();
//            ICollection<Periodical> periodicals = new List<Periodical>();
//
//            foreach (ItemBase item in items)
//            {
//                // need to check discriminator type, since UserBases contains Student and Falculty
//                if (item.GetType().Name.Equals("Periodical"))
//                {
//                    periodicals.Add((Periodical)item);
//                }
//            }

            return View(periodicals);
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
