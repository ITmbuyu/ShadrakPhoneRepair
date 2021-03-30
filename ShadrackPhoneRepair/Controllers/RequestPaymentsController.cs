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
    public class RequestPaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestPayments
        public ActionResult Index()
        {
            return View(db.requestPayments.ToList());
        }

        // GET: RequestPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestPayments requestPayments = db.requestPayments.Find(id);
            if (requestPayments == null)
            {
                return HttpNotFound();
            }
            return View(requestPayments);
        }

        // GET: RequestPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( RequestPayments requestPayments, DeviceStatus deviceStatus)
        {
            if (ModelState.IsValid)
            {
                requestPayments.DateTimeofpayment = DateTime.Now;
                requestPayments.Priceofrepair = Convert.ToString(deviceStatus.Price);
                requestPayments.TrackingNumberOfRequest = deviceStatus.TrackingNumber;
                requestPayments.UserId = User.Identity.GetUserId();
                db.requestPayments.Add(requestPayments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requestPayments);
        }

        // GET: RequestPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestPayments requestPayments = db.requestPayments.Find(id);
            if (requestPayments == null)
            {
                return HttpNotFound();
            }
            return View(requestPayments);
        }

        // POST: RequestPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( RequestPayments requestPayments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestPayments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestPayments);
        }

        // GET: RequestPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestPayments requestPayments = db.requestPayments.Find(id);
            if (requestPayments == null)
            {
                return HttpNotFound();
            }
            return View(requestPayments);
        }

        // POST: RequestPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestPayments requestPayments = db.requestPayments.Find(id);
            db.requestPayments.Remove(requestPayments);
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
