using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeartPharam.Models;
using HeartPharam.Controllers.Factory;

namespace HeartPharam.Controllers
{
    public class PharmOrdController : Controller
    {
        private PharmOrdDBContext db = new PharmOrdDBContext();
        private MedicationDBContext mDb = new MedicationDBContext();

        public void ExportToCSV()
        {
            string type = "CSV";
            List<PharmacyOrder> po = new List<PharmacyOrder>();


            Exporter doc = new ConcreteExporterFactory().CreateFile(type, po);
            doc.ExportShowsToFile();
            Response.ClearContent();

        }

        public void ExportToJSON()
        {
            string type = "JSON";
            List<PharmacyOrder> po = new List<PharmacyOrder>();


            Exporter doc = new ConcreteExporterFactory().CreateFile(type, po);
            doc.ExportShowsToFile();
            Response.ClearContent();

        }

        // GET: PharmOrd
        public ActionResult Index(string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                var phrm = from p in db.PharmOrderList
                           select p;

                if (!String.IsNullOrEmpty(searchString))
                {
                    phrm = phrm.Where(s => s.PharmacistName.Contains(searchString));
                }
                return View(phrm);

            }
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: PharmOrd/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyOrder pharmacyOrder = db.PharmOrderList.Find(id);
            if (pharmacyOrder == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyOrder);
        }

        // GET: PharmOrd/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PharmOrd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DrugName,PharmacistName,UnitCost,Quantity,TotalCost")] PharmacyOrder pharmacyOrder)
        {
            Boolean ok = false; 

            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                foreach (var m in mDb.DrugsList)
                {
                    if (m.Stock > 0 && m.DrugName.Equals(pharmacyOrder.DrugName))
                        ok = true;
                }

                if (ok == true)
                {
                    db.PharmOrderList.Add(pharmacyOrder);
                    db.SaveChanges();

                    Drugs drg = mDb.DrugsList.Single(t => t.DrugName.Contains(pharmacyOrder.DrugName));
                    drg.Stock = drg.Stock - pharmacyOrder.Quantity;
                    mDb.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                    return HttpNotFound();
            }

            return View(pharmacyOrder);
        }

        // GET: PharmOrd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyOrder pharmacyOrder = db.PharmOrderList.Find(id);
            if (pharmacyOrder == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyOrder);
        }

        // POST: PharmOrd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DrugName,PharmacistName,UnitCost,Quantity,TotalCost")] PharmacyOrder pharmacyOrder)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                db.Entry(pharmacyOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pharmacyOrder);
        }

        // GET: PharmOrd/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyOrder pharmacyOrder = db.PharmOrderList.Find(id);
            if (pharmacyOrder == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyOrder);
        }

        // POST: PharmOrd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PharmacyOrder pharmacyOrder = db.PharmOrderList.Find(id);
            db.PharmOrderList.Remove(pharmacyOrder);
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
