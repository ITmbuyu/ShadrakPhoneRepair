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
    public class DeviceStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceStatus
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var deviceStatuses = db.DeviceStatuses.Include(d => d.RepairStatus)/*.Where(x => x.UserId == User.Identity.GetUserId()*/;
            if (User.IsInRole("Customer"))
            {
                //deviceStatuses = db.DeviceStatuses.Include(d => d.RepairStatus).Where(x => x.UserId == userId);
                return View(deviceStatuses.Where(x => x.UserId == userId).ToList());
            }
            return View(deviceStatuses.ToList());
        }

        // GET: DeviceStatus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceStatus deviceStatus = db.DeviceStatuses.Find(id);
            if (deviceStatus == null)
            {
                return HttpNotFound();
            }
            return View(deviceStatus);
        }

        // GET: DeviceStatus/Create
        public ActionResult Create()
        {
            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status");
            return View();
        }

        // POST: DeviceStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrackingNumber,Brand,DeviceProblem,DeviceName,Capacity,Colour,IMEI,Price,RepairStatusId,RequestDateTime,UserId,TechnicianId")] DeviceStatus deviceStatus)
        {
            if (ModelState.IsValid)
            {
                db.DeviceStatuses.Add(deviceStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status", deviceStatus.RepairStatusId);
            return View(deviceStatus);
        }

        // GET: DeviceStatus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceStatus deviceStatus = db.DeviceStatuses.Find(id);
            if (deviceStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status", deviceStatus.RepairStatusId);
            return View(deviceStatus);
        }

        // POST: DeviceStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrackingNumber,Brand,DeviceProblem,DeviceName,Capacity,Colour,IMEI,Price,RepairStatusId,RequestDateTime,UserId,TechnicianId")] DeviceStatus deviceStatus)
        {
            if (ModelState.IsValid)
            {
                deviceStatus.TechnicianId = User.Identity.GetUserId();
                db.Entry(deviceStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status", deviceStatus.RepairStatusId);
            return View(deviceStatus);
        }

        // GET: DeviceStatus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceStatus deviceStatus = db.DeviceStatuses.Find(id);
            if (deviceStatus == null)
            {
                return HttpNotFound();
            }
            return View(deviceStatus);
        }

        // POST: DeviceStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DeviceStatus deviceStatus = db.DeviceStatuses.Find(id);
            db.DeviceStatuses.Remove(deviceStatus);
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
