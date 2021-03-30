using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShadrackPhoneRepair.Models;

namespace ShadrackPhoneRepair.Controllers
{
    public class RepairStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RepairStatus
        public ActionResult Index()
        {
            return View(db.RepairStatuses.ToList());
        }

        // GET: RepairStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairStatus repairStatus = db.RepairStatuses.Find(id);
            if (repairStatus == null)
            {
                return HttpNotFound();
            }
            return View(repairStatus);
        }

        // GET: RepairStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RepairStatusId,Status")] RepairStatus repairStatus)
        {
            if (ModelState.IsValid)
            {
                db.RepairStatuses.Add(repairStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairStatus);
        }

        // GET: RepairStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairStatus repairStatus = db.RepairStatuses.Find(id);
            if (repairStatus == null)
            {
                return HttpNotFound();
            }
            return View(repairStatus);
        }

        // POST: RepairStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RepairStatusId,Status")] RepairStatus repairStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repairStatus);
        }

        // GET: RepairStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairStatus repairStatus = db.RepairStatuses.Find(id);
            if (repairStatus == null)
            {
                return HttpNotFound();
            }
            return View(repairStatus);
        }

        // POST: RepairStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairStatus repairStatus = db.RepairStatuses.Find(id);
            db.RepairStatuses.Remove(repairStatus);
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
