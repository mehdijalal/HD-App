using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICTCR.Models;
using ICTCR.ViewModels;

namespace ICTCR.Controllers
{
   
    public class SupportTypesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: SupportTypes

        public ActionResult Index()
        {
            var StypeQuery = from s in db.MySupportTypeContext
                             join t in db.DateTimeSpans on s.DateTimeSpanId equals t.Id
                             select new SupportTypeVM
                             {
                                 Id = s.Id,
                                 SupportName = s.SupportName,
                                 CostPerSupport = s.CostPerSupport,
                                 Minutes = s.Minutes,
                                 TimeToAction = s.TimeToAction,
                                 DateTimeName = t.DateTimeName
                             };
            return View(StypeQuery);
        }

        // GET: SupportTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportType supportType = db.MySupportTypeContext.Find(id);
            if (supportType == null)
            {
                return HttpNotFound();
            }
            return View(supportType);
        }
        [Authorize(Roles = "Admin, Helpdesk")]
        // GET: SupportTypes/Create
        public ActionResult Create()
        {
            //----------Get All Time Span----//
            var AllTimeSpan = db.DateTimeSpans.ToList();
            List<SelectListItem> MyTimeSpan = new List<SelectListItem>();
            MyTimeSpan.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllTimeSpan)
            {
                MyTimeSpan.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.DateTimeName });
            }
            ViewBag.TimeSpanOption = MyTimeSpan;
            return View();
        }

        // POST: SupportTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupportName,CostPerSupport,Minutes,TimeToAction,DateTimeSpanId,Description")] SupportType supportType, int TimeSpanOption)
        {
            if (ModelState.IsValid)
            {
                supportType.DateTimeSpanId = TimeSpanOption;
                db.MySupportTypeContext.Add(supportType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supportType);
        }
        [Authorize(Roles = "Admin, Helpdesk")]
        // GET: SupportTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportType supportType = db.MySupportTypeContext.Find(id);
            if (supportType == null)
            {
                return HttpNotFound();
            }

            //----------Get All Time----//
            var AllTimeSpan = db.DateTimeSpans.ToList();

            List<SelectListItem> MyTimeSpan = new List<SelectListItem>();
     
            foreach (var STItem in AllTimeSpan)
            {
                var item = new SelectListItem();
                if (STItem.Id == supportType.DateTimeSpanId)
                {
                    item.Selected = true;
                }
                item.Value = STItem.Id.ToString();
                item.Text = STItem.DateTimeName;

                MyTimeSpan.Add(item);
            }
            ViewBag.TimeSpanOption = MyTimeSpan;
            return View(supportType);
        }

        // POST: SupportTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupportName,CostPerSupport,Minutes,TimeToAction,DateTimeSpanId,Description")] SupportType supportType, int TimeSpanOption)
        {
            if (ModelState.IsValid)
            {
                supportType.DateTimeSpanId = TimeSpanOption;
                db.Entry(supportType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supportType);
        }

        // GET: SupportTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportType supportType = db.MySupportTypeContext.Find(id);
            if (supportType == null)
            {
                return HttpNotFound();
            }
            return View(supportType);
        }

        // POST: SupportTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportType supportType = db.MySupportTypeContext.Find(id);
            db.MySupportTypeContext.Remove(supportType);
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
