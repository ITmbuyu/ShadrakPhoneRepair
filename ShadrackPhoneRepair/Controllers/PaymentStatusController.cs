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
    public class PaymentStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentStatus
        public ActionResult Index()
        {
            return View(db.PaymentStatus.ToList());
        }

        // GET: PaymentStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentStatus paymentStatus = db.PaymentStatus.Find(id);
            if (paymentStatus == null)
            {
                return HttpNotFound();
            }
            return View(paymentStatus);
        }

        // GET: PaymentStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentStatusId,Status")] PaymentStatus paymentStatus)
        {
            if (ModelState.IsValid)
            {
                db.PaymentStatus.Add(paymentStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentStatus);
        }

        // GET: PaymentStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentStatus paymentStatus = db.PaymentStatus.Find(id);
            if (paymentStatus == null)
            {
                return HttpNotFound();
            }
            return View(paymentStatus);
        }

        // POST: PaymentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentStatusId,Status")] PaymentStatus paymentStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentStatus);
        }

        // GET: PaymentStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentStatus paymentStatus = db.PaymentStatus.Find(id);
            if (paymentStatus == null)
            {
                return HttpNotFound();
            }
            return View(paymentStatus);
        }

        // POST: PaymentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentStatus paymentStatus = db.PaymentStatus.Find(id);
            db.PaymentStatus.Remove(paymentStatus);
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
