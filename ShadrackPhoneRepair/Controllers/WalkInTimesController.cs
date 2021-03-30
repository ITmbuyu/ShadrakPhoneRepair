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
    public class WalkInTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WalkInTimes
        public ActionResult Index()
        {
            return View(db.WalkInTimes.ToList());
        }

        // GET: WalkInTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkInTimes walkInTimes = db.WalkInTimes.Find(id);
            if (walkInTimes == null)
            {
                return HttpNotFound();
            }
            return View(walkInTimes);
        }

        // GET: WalkInTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkInTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WalkInTimesId,WalkInTime")] WalkInTimes walkInTimes)
        {
            if (ModelState.IsValid)
            {
                db.WalkInTimes.Add(walkInTimes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(walkInTimes);
        }

        // GET: WalkInTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkInTimes walkInTimes = db.WalkInTimes.Find(id);
            if (walkInTimes == null)
            {
                return HttpNotFound();
            }
            return View(walkInTimes);
        }

        // POST: WalkInTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WalkInTimesId,WalkInTime")] WalkInTimes walkInTimes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(walkInTimes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(walkInTimes);
        }

        // GET: WalkInTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkInTimes walkInTimes = db.WalkInTimes.Find(id);
            if (walkInTimes == null)
            {
                return HttpNotFound();
            }
            return View(walkInTimes);
        }

        // POST: WalkInTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WalkInTimes walkInTimes = db.WalkInTimes.Find(id);
            db.WalkInTimes.Remove(walkInTimes);
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
