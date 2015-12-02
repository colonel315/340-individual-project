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
    public class CdsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Cds
        public ActionResult Index(string attributes, string searchString)
        {
            IQueryable<Cd> results = null;

            switch (attributes)
            {
                case "Title":
                    results = db.ItemBases.OfType<Cd>().Where(p => p.Title.Contains(searchString));
                    break;
                case "Year":
                    results = db.ItemBases.OfType<Cd>().Where(p => p.Year.Contains(searchString));
                    break;
            }

            if (String.IsNullOrEmpty(attributes))
            {
                results = db.ItemBases.OfType<Cd>();
            }

            var cd = results.ToList();

            var attributeList = new List<string>();
            attributeList.Add("Title");
            attributeList.Add("Year");

            ViewBag.Attributes = new SelectList(attributeList);

            // return View();
//            ICollection<ItemBase> items = db.ItemBases.ToList();
//            ICollection<Cd> cd = new List<Cd>();
//
//            foreach (ItemBase item in items)
//            {
//                // need to check discriminator type, since UserBases contains Student and Falculty
//                if (item.GetType().Name.Equals("Cd"))
//                {
//                    cd.Add((Cd)item);
//                }
//            }

            return View(cd.ToList());
        }

        // GET: Cds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cd cd = (Cd)db.ItemBases.Find(id);
            if (cd == null)
            {
                return HttpNotFound();
            }
            return View(cd);
        }

        // GET: Cds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Year,Title,Amount,Artist,Director")] Cd cd)
        {
            if (ModelState.IsValid)
            {
                db.ItemBases.Add(cd);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(cd);
        }

        // GET: Cds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cd cd = (Cd)db.ItemBases.Find(id);
            if (cd == null)
            {
                return HttpNotFound();
            }
            return View(cd);
        }

        // POST: Cds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Year,Title,Amount,Artist,Director")] Cd cd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cd);
        }

        // GET: Cds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cd cd = (Cd)db.ItemBases.Find(id);
            if (cd == null)
            {
                return HttpNotFound();
            }
            return View(cd);
        }

        // POST: Cds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cd cd = (Cd)db.ItemBases.Find(id);
            db.ItemBases.Remove(cd);
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
