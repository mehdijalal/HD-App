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
    public class MyUsersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: MyUsers
        public ActionResult Index()
        {
            return View(db.MyUsersContext.ToList());
        }

        // GET: MyUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyUsers myUsers = db.MyUsersContext.Find(id);
            if (myUsers == null)
            {
                return HttpNotFound();
            }
            return View(myUsers);
        }

        // GET: MyUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,RoleId")] MyUsers myUsers)
        {
            if (ModelState.IsValid)
            {
                db.MyUsersContext.Add(myUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myUsers);
        }

        // GET: MyUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyUsers myUsers = db.MyUsersContext.Find(id);
            if (myUsers == null)
            {
                return HttpNotFound();
            }
            return View(myUsers);
        }

        // POST: MyUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,RoleId")] MyUsers myUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myUsers);
        }

        // GET: MyUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyUsers myUsers = db.MyUsersContext.Find(id);
            if (myUsers == null)
            {
                return HttpNotFound();
            }
            return View(myUsers);
        }

        // POST: MyUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyUsers myUsers = db.MyUsersContext.Find(id);
            db.MyUsersContext.Remove(myUsers);
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
