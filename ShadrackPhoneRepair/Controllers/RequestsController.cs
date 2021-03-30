using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using ShadrackPhoneRepair.Models;

namespace ShadrackPhoneRepair.Controllers
{
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Colour).Include(r => r.DeviceDescription).Include(r => r.DeviceProblem).Include(r => r.Storage).Include(w => w.PaymentStatus);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name");
            ViewBag.DeviceDescriptionId = new SelectList(db.DeviceDescriptions, "DeviceDescriptionId", "DeviceName");
            ViewBag.DeviceProblemId = new SelectList(db.DeviceProblems, "DeviceProblemId", "Description");
            ViewBag.StorageId = new SelectList(db.Storages, "StorageId", "StorageCapacity");
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "Status");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Request request)
        {
            if (ModelState.IsValid)
            {
                request.BrandName = db.DeviceDescriptions.Find(request.DeviceDescriptionId).Brand.BrandName;
                request.RequestDateTime = DateTime.Now;
                request.Price = request.CalcPrice();
                request.PaymentStatus = db.PaymentStatus.Find(1);
                request.UserId = User.Identity.GetUserId();
                db.Requests.Add(request);
                db.SaveChanges();

                DeviceStatus status = new DeviceStatus();
                status.TrackingNumber = "STR" + Convert.ToString(request.RequestId);
                status.Brand = request.BrandName;
                status.DeviceProblem = db.DeviceProblems.Find(request.DeviceProblemId).Description;//request.DeviceProblem.Description;
                status.DeviceName = request.DeviceDescription.DeviceName;
                //variable name=  databaseinstance.find(primarykeyofrespectivetable).itemlookingfor
                status.Capacity = db.Storages.Find(request.StorageId).StorageCapacity;
                status.Colour = db.Colours.Find(request.ColourId).Name;
                status.IMEI = request.IMEI;
                status.Price = request.Price;
                status.PaymentStatus = db.PaymentStatus.Find(request.PaymentStatusId).Status;
                status.RepairStatus = db.RepairStatuses.Find(1);
                status.RequestDateTime = request.RequestDateTime;
                status.UserId = request.UserId ;
                //status.StatusId = 1;
                db.DeviceStatuses.Add(status);
                db.SaveChanges();
                SendEmail(request,status);
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(
                    new { Controller = "Requests", Action = "Details", Id = request.RequestId }));
            }

            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", request.ColourId);
            ViewBag.DeviceDescriptionId = new SelectList(db.DeviceDescriptions, "DeviceDescriptionId", "DeviceName", request.DeviceDescriptionId);
            ViewBag.DeviceProblemId = new SelectList(db.DeviceProblems, "DeviceProblemId", "Description", request.DeviceProblemId);
            ViewBag.StorageId = new SelectList(db.Storages, "StorageId", "StorageCapacity", request.StorageId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "Status");
            return View(request);
        }

        //method to send email 
        public void SendEmail(Request request, DeviceStatus status)
        {
            var user = db.Users.Find(request.UserId);
            var email = User.Identity.GetUserName();
            string message =
                $"Hi there, \n\n" +
                $"You have made a request with Shadrack Phone Repair. Here are the details: \n\n" +
                $"Your Tracking Number is: {status.TrackingNumber} \n" +
                $"Your Request Number is: {request.RequestId} \n" +
                $"Your Device Brand is: {request.BrandName} \n" +
                $"Your Device Name is: {request.DeviceDescription.DeviceName} \n" +
                $"Your Device Storage is: {db.Storages.Find(request.StorageId).StorageCapacity} \n" +
                $"Colour Of Device is: { db.Colours.Find(request.ColourId).Name} \n" +
                $"Device IMEI Number is: {request.IMEI} \n" +
                $"Problem with device: {db.DeviceProblems.Find(request.DeviceProblemId).Description} \n" +
                $"Price of repair R: {request.Price} \n\n" +
                $"Driver is on the way to pick up your device, please check dashboard for status of repair \n\n" +
                $"Kind Regards";

            // Sendemail
            var senderEmail = new MailAddress("shadrachphonerepair@gmail.com", "Sharac Phone Repair Tech");
            var recieverMail = new MailAddress(email, "Client");
            var password = "Aigdloves2Nar1";
            var sub = $"New Request #{request.RequestId}";
            var body = message;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, recieverMail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", request.ColourId);
            ViewBag.DeviceDescriptionId = new SelectList(db.DeviceDescriptions, "DeviceDescriptionId", "DeviceName", request.DeviceDescriptionId);
            ViewBag.DeviceProblemId = new SelectList(db.DeviceProblems, "DeviceProblemId", "Description", request.DeviceProblemId);
            ViewBag.StorageId = new SelectList(db.Storages, "StorageId", "StorageCapacity", request.StorageId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "Status");
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                request.BrandName = db.DeviceDescriptions.Find(request.DeviceDescriptionId).Brand.BrandName;
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", request.ColourId);
            ViewBag.DeviceDescriptionId = new SelectList(db.DeviceDescriptions, "DeviceDescriptionId", "DeviceName", request.DeviceDescriptionId);
            ViewBag.DeviceProblemId = new SelectList(db.DeviceProblems, "DeviceProblemId", "Description", request.DeviceProblemId);
            ViewBag.StorageId = new SelectList(db.Storages, "StorageId", "StorageCapacity", request.StorageId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "Status");
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
