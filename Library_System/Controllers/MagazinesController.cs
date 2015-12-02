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
    public class MagazinesController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Magazines
        public ActionResult Index(string attributes, string searchString)
        {
            IQueryable<Magazine> results = null;

            switch (attributes)
            {
                case "Title":
                    results = db.ItemBases.OfType<Magazine>().Where(p => p.Title.Contains(searchString));
                    break;
                case "Year":
                    results = db.ItemBases.OfType<Magazine>().Where(p => p.Year.Contains(searchString));
                    break;
            }

            if (String.IsNullOrEmpty(attributes))
            {
                results = db.ItemBases.OfType<Magazine>();
            }

            var magazines = results.ToList();

            var attributeList = new List<string>();
            attributeList.Add("Title");
            attributeList.Add("Year");

            ViewBag.Attributes = new SelectList(attributeList);

            //            return View();
            //            ICollection<ItemBase> items = db.ItemBases.ToList();
            //            ICollection<Magazine> magazines = new List<Magazine>();
            //
            //            foreach (ItemBase item in items)
            //            {
            //                // need to check discriminator type, since UserBases contains Student and Falculty
            //                if (item.GetType().Name.Equals("Magazine"))
            //                {
            //                    magazines.Add((Magazine)item);
            //                }
            //            }

            return View(magazines);
        }

        // GET: Magazines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = (Magazine)db.ItemBases.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // GET: Magazines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Year,Title,Amount,Publisher")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                db.ItemBases.Add(magazine);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(magazine);
        }

        // GET: Magazines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = (Magazine)db.ItemBases.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // POST: Magazines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Year,Title,Amount,Publisher")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magazine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(magazine);
        }

        // GET: Magazines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = (Magazine)db.ItemBases.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // POST: Magazines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magazine magazine = (Magazine)db.ItemBases.Find(id);
            db.ItemBases.Remove(magazine);
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
