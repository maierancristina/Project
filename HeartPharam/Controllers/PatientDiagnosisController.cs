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
    public class PatientDiagnosisController : Controller
    {
        private PatientDDBContext db = new PatientDDBContext();
        private DiagnosisDBContext dDb = new DiagnosisDBContext();

        // GET: PatientDiagnosis
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View(db.PatDiagnosList.ToList());
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: PatientDiagnosis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDiagnosis patientDiagnosis = db.PatDiagnosList.Find(id);
            if (patientDiagnosis == null)
            {
                return HttpNotFound();
            }
            return View(patientDiagnosis);
        }

        // GET: PatientDiagnosis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientDiagnosis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DiagnosisRank,PatientName,PatDiagnos")] PatientDiagnosis patientDiagnosis)
        {

            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {

                foreach (var d1 in dDb.DiagnosList)
                {
                    if (d1.Notes.Equals(patientDiagnosis.PatDiagnos))
                    {
                        db.PatDiagnosList.Add(patientDiagnosis);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                Diagnosis diagnos = new Diagnosis();
                diagnos.ID = patientDiagnosis.ID;
                diagnos.Notes = patientDiagnosis.PatDiagnos;
                dDb.DiagnosList.Add(diagnos);
                dDb.SaveChanges();
                db.PatDiagnosList.Add(patientDiagnosis);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: PatientDiagnosis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDiagnosis patientDiagnosis = db.PatDiagnosList.Find(id);
            if (patientDiagnosis == null)
            {
                return HttpNotFound();
            }
            return View(patientDiagnosis);
        }

        // POST: PatientDiagnosis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DiagnosisRank,PatientName,PatDiagnos")] PatientDiagnosis patientDiagnosis)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                db.Entry(patientDiagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return View(patientDiagnosis);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: PatientDiagnosis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDiagnosis patientDiagnosis = db.PatDiagnosList.Find(id);
            if (patientDiagnosis == null)
            {
                return HttpNotFound();
            }
            return View(patientDiagnosis);
        }

        // POST: PatientDiagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientDiagnosis patientDiagnosis = db.PatDiagnosList.Find(id);
            db.PatDiagnosList.Remove(patientDiagnosis);
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
