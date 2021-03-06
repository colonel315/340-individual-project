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
    public class CheckOutsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: CheckOuts
        public ActionResult Index()
        {
            var checkOuts = db.CheckOuts.Include(c => c.ItemBase).Include(c => c.Users);
            return View(checkOuts.ToList());
        }

        // GET: CheckOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // GET: CheckOuts/Create
        public ActionResult Create(int? id)
        {
            var item = db.ItemBases.Find(id);
            if (item == null)
            {
                ModelState.AddModelError(String.Empty, "Book does not exist.");
                return RedirectToAction("Dashboard", "Home");
            }
//            ViewBag.ItemId = new SelectList(db.ItemBases, "Id", "Title");
            ViewBag.Item = item;
            ViewBag.UserId = new SelectList(db.UserBases.OfType<ClientBase>(), "Id", "ClientId");
            return View();
        }

        // POST: CheckOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ItemId,IsReserve")] CheckOut checkOut)
        {
            bool canCheckOut = checkOut.canCheckout(db.UserBases.Find(checkOut.UserId),
                db.ItemBases.Find(checkOut.ItemId));

            if (ModelState.IsValid && canCheckOut && (isAvailable(checkOut) || checkOut.IsReserve))
            {
                if (checkOut.IsReserve)
                {
                    checkOut.CheckoutDate = DateTime.Today.AddDays(7);
                }
                else
                {
                    checkOut.CheckoutDate = DateTime.Today;
                }

                db.CheckOuts.Add(checkOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (!canCheckOut)
            {
                ModelState.AddModelError(String.Empty, "Item can't be checked out due to item being either magazine or student checking out periodicals.");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "This book is not available.");
            }

            ViewBag.Item = db.ItemBases.Find(checkOut.ItemId);
            ViewBag.UserId = new SelectList(db.UserBases.OfType<ClientBase>(), "Id", "ClientId", checkOut.UserId);
            return View(checkOut);
        }

        private bool isAvailable(CheckOut checkOut)
        {
            return count(checkOut.ItemId) < db.ItemBases.Find(checkOut.ItemId).Amount;
        }

        private int count(int itemId)
        {
            return db.CheckOuts.Count(c => c.ItemId == itemId);
        }

        // GET: CheckOuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.ItemBases, "Id", "Year", checkOut.ItemId);
            ViewBag.UserId = new SelectList(db.UserBases, "Id", "LastName", checkOut.UserId);
            return View(checkOut);
        }

        // POST: CheckOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ItemId")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.ItemBases, "Id", "Year", checkOut.ItemId);
            ViewBag.UserId = new SelectList(db.UserBases, "Id", "LastName", checkOut.UserId);
            return View(checkOut);
        }

        // GET: CheckOuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOuts.Where(c => c.Id == id).Include(c => c.ItemBase).Include(c => c.Users).FirstOrDefault();
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            
            return View(checkOut);
        }

        // POST: CheckOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckOut checkOut = db.CheckOuts.Find(id);
            db.CheckOuts.Remove(checkOut);
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
