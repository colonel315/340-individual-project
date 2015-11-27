using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using Library_System.DAL;
using Library_System.Models;
using Microsoft.AspNet.Identity;

namespace Library_System.Controllers
{
    public class LibrariansController : Controller
    {
        IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;
        private LibraryContext db = new LibraryContext();

        // GET: Librarians
        public ActionResult Index()
        {
            return View();
        }

        // POST: Librarians
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string employeeId, string password)
        {
            var librarian =
                db.UserBases.OfType<Librarian>()
                    .Where(u => u.EmployeeId == employeeId)
                    .Where(p => p.Password == password)
                    .FirstOrDefault();

            if (librarian == null)
            {
                ModelState.AddModelError(String.Empty, "Your employee ID or your password is invalid");
                return View();
            }
            else
            {
                var identity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, employeeId),
                    }, 
                    DefaultAuthenticationTypes.ApplicationCookie,
                    ClaimTypes.Name, ClaimTypes.Role
                );

                identity.AddClaim(new Claim(ClaimTypes.Role, "Librarian"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, $"{employeeId}"));

                Authentication.SignIn(new AuthenticationProperties
                {}, identity);


                return RedirectToAction("Dashboard", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }

        // GET: Librarians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Librarian librarian = (Librarian)db.UserBases.Find(id);
            if (librarian == null)
            {
                return HttpNotFound();
            }
            return View(librarian);
        }

        // GET: Librarians/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Librarians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,Password,LastName,FirstName")] Librarian librarian)
        {
            if (ModelState.IsValid)
            {
                db.UserBases.Add(librarian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(librarian);
        }

        // GET: Librarians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Librarian librarian = (Librarian)db.UserBases.Find(id);
            if (librarian == null)
            {
                return HttpNotFound();
            }
            return View(librarian);
        }

        // POST: Librarians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,Password,LastName,FirstName")] Librarian librarian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(librarian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(librarian);
        }

        // GET: Librarians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Librarian librarian = (Librarian)db.UserBases.Find(id);
            if (librarian == null)
            {
                return HttpNotFound();
            }
            return View(librarian);
        }

        // POST: Librarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Librarian librarian = (Librarian)db.UserBases.Find(id);
            db.UserBases.Remove(librarian);
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
