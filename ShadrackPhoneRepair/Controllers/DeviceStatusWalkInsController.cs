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
    public class DeviceStatusWalkInsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceStatusWalkIns
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var deviceStatusesWalkIns = db.DeviceStatusesWalkIns.Include(d => d.RepairStatus);
            if (User.IsInRole("Customer"))
            {
                //deviceStatuses = db.DeviceStatuses.Include(d => d.RepairStatus).Where(x => x.UserId == userId);
                return View(deviceStatusesWalkIns.Where(x => x.UserId == userId).ToList());
            }
            return View(deviceStatusesWalkIns.ToList());
        }

        // GET: DeviceStatusWalkIns/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceStatusWalkIns deviceStatusWalkIns = db.DeviceStatusesWalkIns.Find(id);
            if (deviceStatusWalkIns == null)
            {
                return HttpNotFound();
            }
            return View(deviceStatusWalkIns);
        }

        // GET: DeviceStatusWalkIns/Create
        public ActionResult Create()
        {
            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status");
            return View();
        }

        // POST: DeviceStatusWalkIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrackingNumber,Brand,DeviceProblem,DeviceName,Capacity,Colour,IMEI,WalkInDate,WalkInTime,Price,WalkInStatus,RepairStatusId,RequestDateTime,UserId,TechnicianId")] DeviceStatusWalkIns deviceStatusWalkIns)
        {
            if (ModelState.IsValid)
            {
                db.DeviceStatusesWalkIns.Add(deviceStatusWalkIns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status", deviceStatusWalkIns.RepairStatusId);
            return View(deviceStatusWalkIns);
        }

        // GET: DeviceStatusWalkIns/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceStatusWalkIns deviceStatusWalkIns = db.DeviceStatusesWalkIns.Find(id);
            if (deviceStatusWalkIns == null)
            {
                return HttpNotFound();
            }
            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status", deviceStatusWalkIns.RepairStatusId);
            return View(deviceStatusWalkIns);
        }

        // POST: DeviceStatusWalkIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrackingNumber,Brand,DeviceProblem,DeviceName,Capacity,Colour,IMEI,WalkInDate,WalkInTime,Price,WalkInStatus,RepairStatusId,RequestDateTime,UserId,TechnicianId")] DeviceStatusWalkIns deviceStatusWalkIns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceStatusWalkIns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RepairStatusId = new SelectList(db.RepairStatuses, "RepairStatusId", "Status", deviceStatusWalkIns.RepairStatusId);
            return View(deviceStatusWalkIns);
        }

        // GET: DeviceStatusWalkIns/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceStatusWalkIns deviceStatusWalkIns = db.DeviceStatusesWalkIns.Find(id);
            if (deviceStatusWalkIns == null)
            {
                return HttpNotFound();
            }
            return View(deviceStatusWalkIns);
        }

        // POST: DeviceStatusWalkIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DeviceStatusWalkIns deviceStatusWalkIns = db.DeviceStatusesWalkIns.Find(id);
            db.DeviceStatusesWalkIns.Remove(deviceStatusWalkIns);
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
