using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeartPharam.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HeartPharam.Controllers
{
    public class UsrController : Controller
    {
        private UsrDBContext db = new UsrDBContext();
        private ApplicationDbContext aDb = new ApplicationDbContext();

        // GET: Usr
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View(db.UsersList.ToList());
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: Usr/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersList.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Usr/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                return View();
            return RedirectToAction("Index");
        }

        // POST: Usr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Username,Password")] Users users)
        {
            if (ModelState.IsValid && User.IsInRole("Admin"))
            {
                db.UsersList.Add(users);
                db.SaveChanges();
                var newUser = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(aDb));
                newUser.AddToRole("users.ID", "Pharmacist");

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Usr/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersList.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Usr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Username,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Usr/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersList.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Usr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.UsersList.Find(id);
            db.UsersList.Remove(users);
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
