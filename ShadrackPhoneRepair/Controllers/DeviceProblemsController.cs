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
    public class DeviceProblemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceProblems
        public ActionResult Index()
        {
            return View(db.DeviceProblems.ToList());
        }

        // GET: DeviceProblems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceProblem deviceProblem = db.DeviceProblems.Find(id);
            if (deviceProblem == null)
            {
                return HttpNotFound();
            }
            return View(deviceProblem);
        }

        // GET: DeviceProblems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceProblems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceProblemId,Description,CostOfP")] DeviceProblem deviceProblem)
        {
            if (ModelState.IsValid)
            {
                db.DeviceProblems.Add(deviceProblem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deviceProblem);
        }

        // GET: DeviceProblems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceProblem deviceProblem = db.DeviceProblems.Find(id);
            if (deviceProblem == null)
            {
                return HttpNotFound();
            }
            return View(deviceProblem);
        }

        // POST: DeviceProblems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceProblemId,Description,CostOfP")] DeviceProblem deviceProblem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceProblem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deviceProblem);
        }

        // GET: DeviceProblems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceProblem deviceProblem = db.DeviceProblems.Find(id);
            if (deviceProblem == null)
            {
                return HttpNotFound();
            }
            return View(deviceProblem);
        }

        // POST: DeviceProblems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceProblem deviceProblem = db.DeviceProblems.Find(id);
            db.DeviceProblems.Remove(deviceProblem);
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
