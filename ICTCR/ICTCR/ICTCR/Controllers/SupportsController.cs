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
using ICTCR.MJLib;
using System.Web.Configuration;

namespace ICTCR.Controllers
{
    public class SupportsController : Controller
    {
        private MyDbContext db = new MyDbContext();


        // GET: Supports
        [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
        public ActionResult Index()
        {

            string PageNumber = WebConfigurationManager.AppSettings["pageNumber30"];
            int pageNum = Convert.ToInt16(PageNumber);
            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                MyProjects.Add(new SelectListItem { Value = ProItem.Id.ToString(), Text = ProItem.UnitProjectName });
            }
            ViewBag.ProjectOptions = MyProjects;
            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            List<SelectListItem> MySupportTeam = new List<SelectListItem>();
            MySupportTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                MySupportTeam.Add(new SelectListItem { Value = supportTeamItem.Id.ToString(), Text = supportTeamItem.FullName });
            }
            ViewBag.supportTeam = MySupportTeam;

            //----------Get All Support----//
            var AllStatus = db.MyStatusContext.ToList();
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.StatusName });
            }
            ViewBag.Sstatus = MyStatus;
            //=============Details==========================//
            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }
            //--------------details-------------------------//
            string strpost = "&ajax=1";
            var S_Total = from s in db.MySupportContext
                          join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                          join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                          join i in db.MySupportTeam on s.ResponsibleId equals i.Id

                          select new SupportsVM
                          {
                              Id = s.Id,
                              RequesterFullName = s.RequesterFullName,
                              RequesterEmail = s.RequesterEmail,
                              UnitProjectName = p.UnitProjectName,
                              SupportTypeName = st.SupportName,
                              SupportTypeCost = st.CostPerSupport,
                              SupportCost = s.SupportCost,
                              ResponsibleFullName = i.FullName,
                              TotalNumberOfSupport = s.TotalNumberOfSupport,
                              StatusId = s.StatusId,
                              DueTimeToComplete = s.DueTimeToComplete


                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MySupportContext
                        join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                        join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                        join i in db.MySupportTeam on s.ResponsibleId equals i.Id

                        select new SupportsVM
                        {
                            Id = s.Id,
                            RequesterFullName = s.RequesterFullName,
                            RequesterEmail = s.RequesterEmail,
                            UnitProjectName = p.UnitProjectName,
                            SupportTypeName = st.SupportName,
                            SupportTypeCost = st.CostPerSupport,
                            SupportCost = s.SupportCost,
                            ResponsibleFullName = i.FullName,
                            TotalNumberOfSupport = s.TotalNumberOfSupport,
                            CreatedDate = s.CreatedDate,
                            TimeToAction = st.TimeToAction,
                            CostPerMinute = i.PerMinuteCost,
                            MinutesWorked = s.MinutesWorked,
                            ActualCost = s.ActualCost,
                            StatusId = s.StatusId,
                            DueTimeToComplete = s.DueTimeToComplete

                        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);



            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);

            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index2", MyVM);
            }
            return View(MyVM);
        }

        [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
        public ActionResult MyTickets()
        {
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber30"];
            int pageNum = Convert.ToInt16(PageNumber);
            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                MyProjects.Add(new SelectListItem { Value = ProItem.Id.ToString(), Text = ProItem.UnitProjectName });
            }
            ViewBag.ProjectOptions = MyProjects;
            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            List<SelectListItem> MySupportTeam = new List<SelectListItem>();
            MySupportTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                MySupportTeam.Add(new SelectListItem { Value = supportTeamItem.Id.ToString(), Text = supportTeamItem.FullName });
            }
            ViewBag.supportTeam = MySupportTeam;

            //----------Get All Support----//
            var AllStatus = db.MyStatusContext.ToList();
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.StatusName });
            }
            ViewBag.Sstatus = MyStatus;
            //=============Details==========================//
            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }
            //--------------details-------------------------//
            string strpost = "&ajax=1";

            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNDPAF\\".ToCharArray());
            //var ReplaceTheDotWithSpace = TrimedUser.Replace(@".", " ");
            var TheUser = db.MySupportTeam.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));

            var S_Total = from s in db.MySupportContext
                          where s.ResponsibleId == TheUser.Id
                          join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                          join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                          join i in db.MySupportTeam on s.ResponsibleId equals i.Id

                          select new SupportsVM
                          {
                              Id = s.Id,
                              RequesterFullName = s.RequesterFullName,
                              RequesterEmail = s.RequesterEmail,
                              UnitProjectName = p.UnitProjectName,
                              SupportTypeName = st.SupportName,
                              SupportTypeCost = st.CostPerSupport,
                              SupportCost = s.SupportCost,
                              ResponsibleFullName = i.FullName,
                              TotalNumberOfSupport = s.TotalNumberOfSupport,
                              StatusId = s.StatusId,
                              DueTimeToComplete = s.DueTimeToComplete


                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MySupportContext
                        where s.ResponsibleId == TheUser.Id
                        join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                        join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                        join i in db.MySupportTeam on s.ResponsibleId equals i.Id

                        select new SupportsVM
                        {
                            Id = s.Id,
                            RequesterFullName = s.RequesterFullName,
                            RequesterEmail = s.RequesterEmail,
                            UnitProjectName = p.UnitProjectName,
                            SupportTypeName = st.SupportName,
                            SupportTypeCost = st.CostPerSupport,
                            SupportCost = s.SupportCost,
                            ResponsibleFullName = i.FullName,
                            TotalNumberOfSupport = s.TotalNumberOfSupport,
                            CreatedDate = s.CreatedDate,
                            TimeToAction = st.TimeToAction,
                            CostPerMinute = i.PerMinuteCost,
                            MinutesWorked = s.MinutesWorked,
                            ActualCost = s.ActualCost,
                            StatusId = s.StatusId,
                            DueTimeToComplete = s.DueTimeToComplete

                        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);



            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);

            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("MyTickets_Ajax", MyVM);
            }
            return View(MyVM);
        }
        [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
        public ActionResult CompletedTasks()
        {
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber30"];
            int pageNum = Convert.ToInt16(PageNumber);
            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                MyProjects.Add(new SelectListItem { Value = ProItem.Id.ToString(), Text = ProItem.UnitProjectName });
            }
            ViewBag.ProjectOptions = MyProjects;
            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            List<SelectListItem> MySupportTeam = new List<SelectListItem>();
            MySupportTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                MySupportTeam.Add(new SelectListItem { Value = supportTeamItem.Id.ToString(), Text = supportTeamItem.FullName });
            }
            ViewBag.supportTeam = MySupportTeam;

            //----------Get All Support----//
            var AllStatus = db.MyStatusContext.ToList();
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.StatusName });
            }
            ViewBag.Sstatus = MyStatus;
            //=============Details==========================//
            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }

            string strpost = "&ajax=1";
            var S_Total = from s in db.MySupportContext
                          join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                          join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                          join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                          where s.StatusId == 2
                          select new SupportsVM
                          {
                              Id = s.Id,
                              RequesterFullName = s.RequesterFullName,
                              RequesterEmail = s.RequesterEmail,
                              UnitProjectName = p.UnitProjectName,
                              SupportTypeName = st.SupportName,
                              SupportTypeCost = st.CostPerSupport,
                              SupportCost = s.SupportCost,
                              ResponsibleFullName = i.FullName,
                              TotalNumberOfSupport = s.TotalNumberOfSupport,
                              DueTimeToComplete = s.DueTimeToComplete


                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MySupportContext
                        join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                        join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                        join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                        where s.StatusId == 2
                        select new SupportsVM
                        {
                            Id = s.Id,
                            RequesterFullName = s.RequesterFullName,
                            RequesterEmail = s.RequesterEmail,
                            UnitProjectName = p.UnitProjectName,
                            SupportTypeName = st.SupportName,
                            SupportTypeCost = st.CostPerSupport,
                            SupportCost = s.SupportCost,
                            ResponsibleFullName = i.FullName,
                            TotalNumberOfSupport = s.TotalNumberOfSupport,
                            CreatedDate = s.CreatedDate,
                            TimeToAction = st.TimeToAction,
                            CostPerMinute = i.PerMinuteCost,
                            MinutesWorked = s.MinutesWorked,
                            ActualCost = s.ActualCost,
                            DueTimeToComplete = s.DueTimeToComplete

                        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);



            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);

            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index2", MyVM);
            }
            return View(MyVM);
        }

        [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
        public ActionResult OnholdTasks()
        {
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber30"];
            int pageNum = Convert.ToInt16(PageNumber);
            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                MyProjects.Add(new SelectListItem { Value = ProItem.Id.ToString(), Text = ProItem.UnitProjectName });
            }
            ViewBag.ProjectOptions = MyProjects;
            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            List<SelectListItem> MySupportTeam = new List<SelectListItem>();
            MySupportTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                MySupportTeam.Add(new SelectListItem { Value = supportTeamItem.Id.ToString(), Text = supportTeamItem.FullName });
            }
            ViewBag.supportTeam = MySupportTeam;

            //----------Get All Support----//
            var AllStatus = db.MyStatusContext.ToList();
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.StatusName });
            }
            ViewBag.Sstatus = MyStatus;
            //=============Details==========================//

            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }

            string strpost = "&ajax=1";
            var S_Total = from s in db.MySupportContext
                          join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                          join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                          join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                          where s.StatusId == 3
                          select new SupportsVM
                          {
                              Id = s.Id,
                              RequesterFullName = s.RequesterFullName,
                              RequesterEmail = s.RequesterEmail,
                              UnitProjectName = p.UnitProjectName,
                              SupportTypeName = st.SupportName,
                              SupportTypeCost = st.CostPerSupport,
                              SupportCost = s.SupportCost,
                              ResponsibleFullName = i.FullName,
                              TotalNumberOfSupport = s.TotalNumberOfSupport,
                              DueTimeToComplete = s.DueTimeToComplete


                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MySupportContext
                        join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                        join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                        join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                        where s.StatusId == 3
                        select new SupportsVM
                        {
                            Id = s.Id,
                            RequesterFullName = s.RequesterFullName,
                            RequesterEmail = s.RequesterEmail,
                            UnitProjectName = p.UnitProjectName,
                            SupportTypeName = st.SupportName,
                            SupportTypeCost = st.CostPerSupport,
                            SupportCost = s.SupportCost,
                            ResponsibleFullName = i.FullName,
                            TotalNumberOfSupport = s.TotalNumberOfSupport,
                            CreatedDate = s.CreatedDate,
                            TimeToAction = st.TimeToAction,
                            CostPerMinute = i.PerMinuteCost,
                            MinutesWorked = s.MinutesWorked,
                            ActualCost = s.ActualCost,
                            DueTimeToComplete = s.DueTimeToComplete

                        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);



            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);

            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index2", MyVM);
            }
            return View(MyVM);
        }

        [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
        public ActionResult PendingTasks()
        {
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber30"];
            int pageNum = Convert.ToInt16(PageNumber);
            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                MyProjects.Add(new SelectListItem { Value = ProItem.Id.ToString(), Text = ProItem.UnitProjectName });
            }
            ViewBag.ProjectOptions = MyProjects;
            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            List<SelectListItem> MySupportTeam = new List<SelectListItem>();
            MySupportTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                MySupportTeam.Add(new SelectListItem { Value = supportTeamItem.Id.ToString(), Text = supportTeamItem.FullName });
            }
            ViewBag.supportTeam = MySupportTeam;

            //----------Get All Support----//
            var AllStatus = db.MyStatusContext.ToList();
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.StatusName });
            }
            ViewBag.Sstatus = MyStatus;
            //=============Details==========================//
            DateTime a = DateTime.Now;
            DateTime b = DateTime.Parse("2016-07-10 12:25:34.923");

            TimeSpan span = a.Subtract(b);

            ViewBag.DifTime = a.AddHours(3);

            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }

            string strpost = "&ajax=1";
            var S_Total = from s in db.MySupportContext
                          join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                          join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                          join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                          where s.StatusId != 2
                          select new SupportsVM
                          {
                              Id = s.Id,
                              RequesterFullName = s.RequesterFullName,
                              RequesterEmail = s.RequesterEmail,
                              UnitProjectName = p.UnitProjectName,
                              SupportTypeName = st.SupportName,
                              SupportTypeCost = st.CostPerSupport,
                              SupportCost = s.SupportCost,
                              ResponsibleFullName = i.FullName,
                              TotalNumberOfSupport = s.TotalNumberOfSupport,
                              DueTimeToComplete = s.DueTimeToComplete


                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MySupportContext
                        join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
                        join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                        join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                        where s.StatusId != 2
                        select new SupportsVM
                        {
                            Id = s.Id,
                            RequesterFullName = s.RequesterFullName,
                            RequesterEmail = s.RequesterEmail,
                            UnitProjectName = p.UnitProjectName,
                            SupportTypeName = st.SupportName,
                            SupportTypeCost = st.CostPerSupport,
                            SupportCost = s.SupportCost,
                            ResponsibleFullName = i.FullName,
                            TotalNumberOfSupport = s.TotalNumberOfSupport,
                            CreatedDate = s.CreatedDate,
                            TimeToAction = st.TimeToAction,
                            CostPerMinute = i.PerMinuteCost,
                            MinutesWorked = s.MinutesWorked,
                            ActualCost = s.ActualCost,
                            DueTimeToComplete = s.DueTimeToComplete

                        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);



            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);

            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index2", MyVM);
            }
            return View(MyVM);
        }

        // GET: Supports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supports supports = db.MySupportContext.Find(id);
            if (supports == null)
            {
                return HttpNotFound();
            }
            return View(supports);
        }

        // GET: Supports/Create
        [Authorize(Roles = "Admin, Helpdesk, Individual")]
        public ActionResult Create()
        {
            //----------Get All Support----//
            var AllSupportsTypes = db.MySupportTypeContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MySupportTypes = new List<SelectListItem>();
            MySupportTypes.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllSupportsTypes)
            {
                MySupportTypes.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.SupportName });
            }
            ViewBag.STypeOptions = MySupportTypes;

            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                MyProjects.Add(new SelectListItem { Value = ProItem.Id.ToString(), Text = ProItem.UnitProjectName });
            }
            ViewBag.ProjectOptions = MyProjects;

            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            List<SelectListItem> MySupportTeam = new List<SelectListItem>();
            MySupportTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                MySupportTeam.Add(new SelectListItem { Value = supportTeamItem.Id.ToString(), Text = supportTeamItem.FullName });
            }
            ViewBag.supportTeam = MySupportTeam;
            return View();
        }

        // POST: Supports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequesterFullName,RequesterEmail,ProjectId,SupportTypeId,StartDate,Remarks,ResponsibleId,TotalNumberOfSupport,DueDate")] Supports supports, int STypeOptions, int ProjectOptions, int supportTeam)
        {
            if (ModelState.IsValid)
            {
                supports.UnitProjectId = ProjectOptions;
                supports.ResponsibleId = supportTeam;
                supports.PrimaryResponsibleId = supportTeam;
                //var ThisSupportCost = db.MySupportTypeContext.Where(s => s.Id == STypeOptions).FirstOrDefault();
                var ThisSupportCost = (from st in db.MySupportTypeContext.Where(s => s.Id == STypeOptions)
                                       join t in db.DateTimeSpans on st.DateTimeSpanId equals t.Id
                                       where st.Id == STypeOptions
                                       select new SupportTypeVM
                                       {
                                           TimeToAction = st.TimeToAction,
                                           CostPerSupport = st.CostPerSupport,
                                           DateTimeName = t.DateTimeName
                                       }).FirstOrDefault();

                DateTime StartDate = supports.StartDate;
                var MySLATimeToAction = ThisSupportCost.TimeToAction * supports.TotalNumberOfSupport;
                DateTime SLADate;
                if (ThisSupportCost.DateTimeName == "Days")
                {
                    SLADate = StartDate.AddDays(MySLATimeToAction);
                }
                else if (ThisSupportCost.DateTimeName == "Hours")
                {
                    SLADate = StartDate.AddHours(MySLATimeToAction);
                }
                else
                {
                    SLADate = StartDate.AddMinutes(MySLATimeToAction);
                }

                supports.DueTimeToComplete = SLADate;
                supports.StatusId = 1;
                supports.SupportCost = ThisSupportCost.CostPerSupport * supports.TotalNumberOfSupport;
                supports.SupportTypeId = STypeOptions;
                supports.CreatedDate = DateTime.Now;
                supports.ModifiedDate = DateTime.Now;
                supports.CreatedBy = User.Identity.Name;
                supports.ModifiedBy = User.Identity.Name;

                db.MySupportContext.Add(supports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supports);
        }

        // GET: Supports/Edit/5
        [Authorize(Roles = "Admin, Helpdesk")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supports supports = db.MySupportContext.Find(id);
            if (supports == null)
            {
                return HttpNotFound();
            }

            //----------Get All Support----//
            var AllSupportsTypes = db.MySupportTypeContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MySupportTypes = new List<SelectListItem>();
            //MySupportTypes.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllSupportsTypes)
            {
                var item = new SelectListItem();
                if (STItem.Id == supports.SupportTypeId)
                {
                    item.Selected = true;
                }
                item.Value = STItem.Id.ToString();
                item.Text = STItem.SupportName;

                MySupportTypes.Add(item);
            }
            ViewBag.STypeOptions = MySupportTypes;

            //----------Get All Projects----//
            var AllProjects = db.MyProjectsContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyProjects = new List<SelectListItem>();
            //MyProjects.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var ProItem in AllProjects)
            {
                var pitem = new SelectListItem();
                if (ProItem.Id == supports.UnitProjectId)
                {
                    pitem.Selected = true;
                }
                pitem.Value = ProItem.Id.ToString();
                pitem.Text = ProItem.UnitProjectName;

                MyProjects.Add(pitem);
            }
            ViewBag.ProjectOptions = MyProjects;

            //----------Get All Support Team----//
            var AllSupportTeam = db.MySupportTeam.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MySupporTeam = new List<SelectListItem>();
            //MySupporTeam.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var supportTeamItem in AllSupportTeam)
            {
                var item = new SelectListItem();
                if (supportTeamItem.Id == supports.ResponsibleId)
                {
                    item.Selected = true;
                }
                item.Value = supportTeamItem.Id.ToString();
                item.Text = supportTeamItem.FullName;

                MySupporTeam.Add(item);
            }
            ViewBag.supportTeam = MySupporTeam;
            return View(supports);
        }

        // POST: Supports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequesterFullName,RequesterEmail,ProjectId,SupportTypeId,StartDate,Remarks,ResponsibleId,TotalNumberOfSupport,DueDate,CreatedDate,DueTimeToComplete,CreatedBy")] Supports supports, int STypeOptions, int ProjectOptions, int supportTeam)
        {
            if (ModelState.IsValid)
            {
                supports.UnitProjectId = ProjectOptions;
                supports.ResponsibleId = supportTeam;
                double ThisSupportCost = db.MySupportTypeContext.Where(s => s.Id == STypeOptions).Select(s => s.CostPerSupport).FirstOrDefault();
                supports.SupportCost = ThisSupportCost * supports.TotalNumberOfSupport;
                supports.SupportTypeId = STypeOptions;
                supports.ModifiedDate = DateTime.Now;
                supports.ModifiedBy = User.Identity.Name;

                db.Entry(supports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supports);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
        public ActionResult AddUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supports supports = db.MySupportContext.Find(id);
            if (supports == null)
            {
                return HttpNotFound();
            }
            //----Check-----------------//
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNDPAF\\".ToCharArray());
            var TheUser = db.MySupportTeam.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));


            if (TheUser.RoleId != 1 && TheUser.RoleId!=2)
            {

                if(TheUser.Id!=supports.ResponsibleId)
                {
                    return RedirectToAction("MyTickets");
                }

            }
            //----------Get All Support----//
            var AllStatus = db.MyStatusContext.ToList();
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-Select-" });
            foreach (var STItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = STItem.Id.ToString(), Text = STItem.StatusName });
            }
            ViewBag.Sstatus = MyStatus;

            var details = (from s in db.MySupportContext
                           join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                           join t in db.MySupportTypeContext on s.SupportTypeId equals t.Id
                           join i in db.MySupportTeam on s.ResponsibleId equals i.Id
                           where s.Id == id
                           select new SupportsVM
                           {
                               RequesterFullName = s.RequesterFullName,
                               RequesterEmail = s.RequesterEmail,
                               UnitProjectName = p.UnitProjectName,
                               SupportTypeName = t.SupportName,
                               TotalNumberOfSupport = s.TotalNumberOfSupport,
                               DueTimeToComplete = s.DueTimeToComplete,
                               ResponsibleId = s.ResponsibleId,
                               ResponsibleFullName = i.FullName,
                               Remarks = s.Remarks
                           }).FirstOrDefault();
            return View(details);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdate([Bind(Include = "Id,MinutesWorked,AdditionalComments,ResponsibleId")] Supports support, int Sstatus)
        {
            if (ModelState.IsValid)
            {
                support.ModifiedBy = User.Identity.Name;
                support.ModifiedDate = DateTime.Now;

                var Team_Cost = db.MySupportTeam.Where(c => c.Id == support.ResponsibleId).FirstOrDefault();

                var Actual_cost = support.MinutesWorked * Team_Cost.PerMinuteCost;

                support.ActualCost = Actual_cost;
                support.StatusId = Sstatus;
                support.TaskCompletionDate = DateTime.Now;

                db.MySupportContext.Attach(support);
                db.Entry(support).Property(x => x.MinutesWorked).IsModified = true;
                db.Entry(support).Property(x => x.ActualCost).IsModified = true;
                db.Entry(support).Property(x => x.ModifiedDate).IsModified = true;
                db.Entry(support).Property(x => x.ModifiedBy).IsModified = true;
                db.Entry(support).Property(x => x.AdditionalComments).IsModified = true;
                db.Entry(support).Property(x => x.StatusId).IsModified = true;
                db.Entry(support).Property(x => x.TaskCompletionDate).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(support);
        }
        // GET: Supports/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supports supports = db.MySupportContext.Find(id);
            if (supports == null)
            {
                return HttpNotFound();
            }
            return View(supports);
        }

        // POST: Supports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supports supports = db.MySupportContext.Find(id);
            db.MySupportContext.Remove(supports);
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
