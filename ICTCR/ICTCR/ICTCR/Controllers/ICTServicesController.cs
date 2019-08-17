using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICTCR.Models;

namespace ICTCR.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ICTServicesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ICTServices
        public ActionResult Index()
        {
            return View(db.ICTServices.ToList());
        }

        // GET: ICTServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICTServices iCTServices = db.ICTServices.Find(id);
            if (iCTServices == null)
            {
                return HttpNotFound();
            }
            return View(iCTServices);
        }

        // GET: ICTServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ICTServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceName,ServiceMonthlyCost,Description")] ICTServices iCTServices)
        {
            if (ModelState.IsValid)
            {
                db.ICTServices.Add(iCTServices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iCTServices);
        }

        // GET: ICTServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICTServices iCTServices = db.ICTServices.Find(id);
            if (iCTServices == null)
            {
                return HttpNotFound();
            }
            return View(iCTServices);
        }

        // POST: ICTServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ServiceName,ServiceMonthlyCost,Description")] ICTServices iCTServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iCTServices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iCTServices);
        }

        // GET: ICTServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICTServices iCTServices = db.ICTServices.Find(id);
            if (iCTServices == null)
            {
                return HttpNotFound();
            }
            return View(iCTServices);
        }

        // POST: ICTServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ICTServices iCTServices = db.ICTServices.Find(id);
            db.ICTServices.Remove(iCTServices);
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
