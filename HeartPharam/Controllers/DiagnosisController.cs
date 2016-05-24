using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeartPharam.Models;

namespace HeartPharam.Controllers
{
    public class DiagnosisController : Controller
    {
        private DiagnosisDBContext db = new DiagnosisDBContext();

        // GET: Diagnosis
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View(db.DiagnosList.ToList());
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: Diagnosis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.DiagnosList.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // GET: Diagnosis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diagnosis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Notes")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                db.DiagnosList.Add(diagnosis);
                db.SaveChanges();
                return View(diagnosis);
            }
            else
                return RedirectToAction("Login", "Account");
            
        }

        // GET: Diagnosis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.DiagnosList.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnosis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Notes")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return View(diagnosis);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: Diagnosis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.DiagnosList.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnosis diagnosis = db.DiagnosList.Find(id);
            db.DiagnosList.Remove(diagnosis);
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
