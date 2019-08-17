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
    public class DateTimeSpansController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: DateTimeSpans
        public ActionResult Index()
        {
            return View(db.DateTimeSpans.ToList());
        }

        // GET: DateTimeSpans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTimeSpan dateTimeSpan = db.DateTimeSpans.Find(id);
            if (dateTimeSpan == null)
            {
                return HttpNotFound();
            }
            return View(dateTimeSpan);
        }

        // GET: DateTimeSpans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DateTimeSpans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateTimeName")] DateTimeSpan dateTimeSpan)
        {
            if (ModelState.IsValid)
            {
                db.DateTimeSpans.Add(dateTimeSpan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dateTimeSpan);
        }

        // GET: DateTimeSpans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTimeSpan dateTimeSpan = db.DateTimeSpans.Find(id);
            if (dateTimeSpan == null)
            {
                return HttpNotFound();
            }
            return View(dateTimeSpan);
        }

        // POST: DateTimeSpans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTimeName")] DateTimeSpan dateTimeSpan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dateTimeSpan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dateTimeSpan);
        }

        // GET: DateTimeSpans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTimeSpan dateTimeSpan = db.DateTimeSpans.Find(id);
            if (dateTimeSpan == null)
            {
                return HttpNotFound();
            }
            return View(dateTimeSpan);
        }

        // POST: DateTimeSpans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DateTimeSpan dateTimeSpan = db.DateTimeSpans.Find(id);
            db.DateTimeSpans.Remove(dateTimeSpan);
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
