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
 
    public class SupportTeamsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: SupportTeams
        public ActionResult Index()
        {
            return View(db.MySupportTeam.ToList());
        }

        // GET: SupportTeams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTeam supportTeam = db.MySupportTeam.Find(id);
            if (supportTeam == null)
            {
                return HttpNotFound();
            }
            return View(supportTeam);
        }

        // GET: SupportTeams/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupportTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,Phone,Title,PerMinuteCost")] SupportTeam supportTeam)
        {
            if (ModelState.IsValid)
            {
                db.MySupportTeam.Add(supportTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supportTeam);
        }

        // GET: SupportTeams/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTeam supportTeam = db.MySupportTeam.Find(id);
            if (supportTeam == null)
            {
                return HttpNotFound();
            }
            return View(supportTeam);
        }

        // POST: SupportTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,Phone,Title,PerMinuteCost")] SupportTeam supportTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supportTeam);
        }

        // GET: SupportTeams/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTeam supportTeam = db.MySupportTeam.Find(id);
            if (supportTeam == null)
            {
                return HttpNotFound();
            }
            return View(supportTeam);
        }

        // POST: SupportTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportTeam supportTeam = db.MySupportTeam.Find(id);
            db.MySupportTeam.Remove(supportTeam);
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
