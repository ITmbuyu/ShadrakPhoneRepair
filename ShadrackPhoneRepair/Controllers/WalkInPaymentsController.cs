using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ShadrackPhoneRepair.Models;

namespace ShadrackPhoneRepair.Controllers
{
    public class WalkInPaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WalkInPayments
        public ActionResult Index()
        {
            return View(db.WalkInPayments.ToList());
        }

        // GET: WalkInPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkInPayments walkInPayments = db.WalkInPayments.Find(id);
            if (walkInPayments == null)
            {
                return HttpNotFound();
            }
            return View(walkInPayments);
        }

        // GET: WalkInPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkInPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WalkInPayments walkInPayments,DeviceStatusWalkIns deviceStatusWalkIns)
        {
            if (ModelState.IsValid)
            {
                walkInPayments.DateTimeofpayment = DateTime.Now;
                walkInPayments.Priceofrepair = Convert.ToString(deviceStatusWalkIns.Price);
                walkInPayments.TrackingNumberOfRequest = deviceStatusWalkIns.TrackingNumber;
                walkInPayments.UserId = User.Identity.GetUserId();
                db.WalkInPayments.Add(walkInPayments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(walkInPayments);
        }

        // GET: WalkInPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkInPayments walkInPayments = db.WalkInPayments.Find(id);
            if (walkInPayments == null)
            {
                return HttpNotFound();
            }
            return View(walkInPayments);
        }

        // POST: WalkInPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WalkInPaymentsId,paymentmethod,CardNumber,CVVnumber,ExpiryDate,UserId,DateTimeofpayment,TrackingNumberOfRequest,Priceofrepair")] WalkInPayments walkInPayments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(walkInPayments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(walkInPayments);
        }

        // GET: WalkInPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkInPayments walkInPayments = db.WalkInPayments.Find(id);
            if (walkInPayments == null)
            {
                return HttpNotFound();
            }
            return View(walkInPayments);
        }

        // POST: WalkInPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WalkInPayments walkInPayments = db.WalkInPayments.Find(id);
            db.WalkInPayments.Remove(walkInPayments);
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
