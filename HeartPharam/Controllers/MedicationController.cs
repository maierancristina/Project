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
    public class MedicationController : Controller
    {
        private MedicationDBContext db = new MedicationDBContext();

        // GET: Medication
        public ActionResult Index(string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                var drg = from d in db.DrugsList
                          select d;

                if (!String.IsNullOrEmpty(searchString))
                {
                    drg = drg.Where(s => s.DrugName.Contains(searchString));
                }
                return View(drg);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: Medication/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drugs drugs = db.DrugsList.Find(id);
            if (drugs == null)
            {
                return HttpNotFound();
            }
            return View(drugs);
        }

        // GET: Medication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DrugName,Dosage,Instructions,Stock")] Drugs drugs)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                db.DrugsList.Add(drugs);
                db.SaveChanges();
                return View(drugs);
            }
            else
                return RedirectToAction("Login", "Account");

            
        }

        // GET: Medication/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drugs drugs = db.DrugsList.Find(id);
            if (drugs == null)
            {
                return HttpNotFound();
            }
            return View(drugs);
        }

        // POST: Medication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DrugName,Dosage,Instructions,Stock")] Drugs drugs)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                db.Entry(drugs).State = EntityState.Modified;
                db.SaveChanges();
                return View(drugs);
            }
            else
                return RedirectToAction("Login", "Account");           
        }

        // GET: Medication/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drugs drugs = db.DrugsList.Find(id);
            if (drugs == null)
            {
                return HttpNotFound();
            }
            return View(drugs);
        }

        // POST: Medication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drugs drugs = db.DrugsList.Find(id);
            db.DrugsList.Remove(drugs);
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
