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
    public class DeviceDescriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceDescriptions
        public ActionResult Index()
        {
            var deviceDescriptions = db.DeviceDescriptions.Include(d => d.Brand);
            return View(deviceDescriptions.ToList());
        }

        // GET: DeviceDescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceDescription deviceDescription = db.DeviceDescriptions.Find(id);
            if (deviceDescription == null)
            {
                return HttpNotFound();
            }
            return View(deviceDescription);
        }

        // GET: DeviceDescriptions/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: DeviceDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceDescriptionId,BrandId,DeviceName")] DeviceDescription deviceDescription)
        {
            if (ModelState.IsValid)
            {
                db.DeviceDescriptions.Add(deviceDescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", deviceDescription.BrandId);
            return View(deviceDescription);
        }

        // GET: DeviceDescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceDescription deviceDescription = db.DeviceDescriptions.Find(id);
            if (deviceDescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", deviceDescription.BrandId);
            return View(deviceDescription);
        }

        // POST: DeviceDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceDescriptionId,BrandId,DeviceName")] DeviceDescription deviceDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", deviceDescription.BrandId);
            return View(deviceDescription);
        }

        // GET: DeviceDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceDescription deviceDescription = db.DeviceDescriptions.Find(id);
            if (deviceDescription == null)
            {
                return HttpNotFound();
            }
            return View(deviceDescription);
        }

        // POST: DeviceDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceDescription deviceDescription = db.DeviceDescriptions.Find(id);
            db.DeviceDescriptions.Remove(deviceDescription);
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
