using ICTCR.Models;
using ICTCR.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ICTCR.Controllers
{
    [Authorize(Roles = "Admin, Helpdesk, Individual, OnlyUpdateMine")]
    public class DashboardController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            var currentYear = DateTime.Now.Year;

            //-----------All Tickets----------------//
            var All_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear) select s;
            ViewBag.All_tickets_count = All_Tictes_count_query.Count();
            //===========Pending tickets================//
            var All_pending_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p=>p.StatusId==1) select s;
            ViewBag.All_pending_count = All_pending_Tictes_count_query.Count();
            //===========Onhold tickets================//
            var All_onhold_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 3) select s;
            ViewBag.All_onhold_count = All_onhold_Tictes_count_query.Count();
            //===========Completed tickets================//
            var All_completed_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 3) select s;
            ViewBag.All_completed_count = All_completed_Tictes_count_query.Count();
            //-----------End of counts---------------//
            var AllTickets = from s in db.MySupportContext.Where(y=>y.CreatedDate.Year==currentYear).GroupBy(s => s.CreatedDate.Month).Select(g => new { Month = g.Key, Total = g.Count() }) select s;
            var CompletedTickets = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s=>s.StatusId==2).GroupBy(s => s.CreatedDate.Month).Select(g => new { Month = g.Key, Total = g.Count() }) select s;
            var PendingTickets = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s => s.StatusId == 1).GroupBy(s => s.CreatedDate.Month).Select(g => new { Month = g.Key, Total = g.Count() }) select s;
            var OnHoldTickets = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s => s.StatusId == 3).GroupBy(s => s.CreatedDate.Month).Select(g => new { Month = g.Key, Total = g.Count() }) select s;
            foreach (var items in AllTickets)
            {
                if(items.Month==1)
                {
                    ViewBag.Jan = items.Total;
                }
                else if(items.Month==2)
                {
                    ViewBag.Feb = items.Total;
                }
                else if(items.Month==3)
                {
                    ViewBag.March = items.Total;
                }
                else if (items.Month == 4)
                {
                    ViewBag.April = items.Total;
                }
                else if (items.Month == 5)
                {
                    ViewBag.May = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.June = items.Total;
                }
                else if (items.Month == 7)
                {
                    ViewBag.July = items.Total;
                }
                else if (items.Month == 8)
                {
                    ViewBag.Aug = items.Total;
                }
                else if (items.Month == 9)
                {
                    ViewBag.Sep = items.Total;
                }
                else if (items.Month == 10)
                {
                    ViewBag.Oct = items.Total;
                }
                else if (items.Month == 11)
                {
                    ViewBag.Nov = items.Total;
                }
                else if (items.Month == 12)
                {
                    ViewBag.Dec = items.Total;
                }
            }

            //======Completed tickets=========//
            foreach (var items in CompletedTickets)
            {
                if (items.Month == 1)
                {
                    ViewBag.JanC = items.Total;
                }
                else if (items.Month == 2)
                {
                    ViewBag.FebC = items.Total;
                }
                else if (items.Month == 3)
                {
                    ViewBag.MarchC = items.Total;
                }
                else if (items.Month == 4)
                {
                    ViewBag.AprilC = items.Total;
                }
                else if (items.Month == 5)
                {
                    ViewBag.MayC = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.JuneC = items.Total;
                }
                else if (items.Month == 7)
                {
                    ViewBag.JulyC = items.Total;
                }
                else if (items.Month == 8)
                {
                    ViewBag.AugC = items.Total;
                }
                else if (items.Month == 9)
                {
                    ViewBag.SepC = items.Total;
                }
                else if (items.Month == 10)
                {
                    ViewBag.OctC = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.NovC = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.DecC = items.Total;
                }
            }

            //======Pending tickets=========//
            foreach (var items in PendingTickets)
            {
                if (items.Month == 1)
                {
                    ViewBag.JanP = items.Total;
                }
                else if (items.Month == 2)
                {
                    ViewBag.FebP = items.Total;
                }
                else if (items.Month == 3)
                {
                    ViewBag.MarchP = items.Total;
                }
                else if (items.Month == 4)
                {
                    ViewBag.AprilP = items.Total;
                }
                else if (items.Month == 5)
                {
                    ViewBag.MayP = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.JuneP = items.Total;
                }
                else if (items.Month == 7)
                {
                    ViewBag.JulyP = items.Total;
                }
                else if (items.Month == 8)
                {
                    ViewBag.AugP = items.Total;
                }
                else if (items.Month == 9)
                {
                    ViewBag.SepP = items.Total;
                }
                else if (items.Month == 10)
                {
                    ViewBag.OctP = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.NovP = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.DecP = items.Total;
                }
            }
            //======On Hold tickets=========//
            foreach (var items in OnHoldTickets)
            {
                if (items.Month == 1)
                {
                    ViewBag.JanH = items.Total;
                }
                else if (items.Month == 2)
                {
                    ViewBag.FebH = items.Total;
                }
                else if (items.Month == 3)
                {
                    ViewBag.MarchH = items.Total;
                }
                else if (items.Month == 4)
                {
                    ViewBag.AprilH = items.Total;
                }
                else if (items.Month == 5)
                {
                    ViewBag.MayH = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.JuneP = items.Total;
                }
                else if (items.Month == 7)
                {
                    ViewBag.JulyH = items.Total;
                }
                else if (items.Month == 8)
                {
                    ViewBag.AugP = items.Total;
                }
                else if (items.Month == 9)
                {
                    ViewBag.SepH = items.Total;
                }
                else if (items.Month == 10)
                {
                    ViewBag.OctH = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.NovP = items.Total;
                }
                else if (items.Month == 6)
                {
                    ViewBag.DecH = items.Total;
                }
            }
            return View();
        }

        public ActionResult MinutesWorked()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDay = DateTime.Now.Day;
            //-----------All Tickets----------------//
            var All_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear) select s;
            ViewBag.All_tickets_count = All_Tictes_count_query.Count();
         
            //===========Pending tickets================//
            var All_pending_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 1) select s;
            ViewBag.All_pending_count = All_pending_Tictes_count_query.Count();
            //===========Onhold tickets================//
            var All_onhold_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 3) select s;
            ViewBag.All_onhold_count = All_onhold_Tictes_count_query.Count();
            //===========Completed tickets================//
            var All_completed_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 2) select s;
            ViewBag.All_completed_count = All_completed_Tictes_count_query.Count();
            //-----------End of counts---------------//

            //var AllTickets = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).GroupBy(s => s.CreatedDate.Month).Select(g => new { Month = g.Key, Total = g.Count() }) select s;
            var TotalMinutesWorkedToday = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s=>s.ModifiedDate.Month==currentMonth).Where(d=>d.ModifiedDate.Day==currentDay)
                               .GroupBy(s => s.ResponsibleId)
                               .Select(g => new { ResponsibleTotalM = g.Key, Total = g.Sum(v=>v.MinutesWorked) })
                               select s;
            //----------Today worked-----------------//
            var TotalMinutesWorked = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear)
                         .GroupBy(s => s.ResponsibleId)
                         .Select(g => new { ResponsibleTotalM = g.Key, Total = g.Sum(v => v.MinutesWorked) })
                                     select s;
            //===============Total Tickets assigned from begining of year===============//
            var TotalTicketsAssigned = from s in db.MySupportContext
                                       .Where(y => y.CreatedDate.Year == currentYear)
                                       join t in db.MySupportTeam on s.ResponsibleId equals t.Id
                                       group 1 by t.FullName into g
                                 
                                       select new SupportChartVM
                                       {
                                           Responsible = g.Key,
                                           TotalSupport = g.Count()
                                       };
            List<string> ResPonsibleName = new List<string>();
            List<int> ResTotal = new List<int>();
            foreach (var items in TotalTicketsAssigned)
            {
                ResPonsibleName.Add(items.Responsible);
                ResTotal.Add(items.TotalSupport);
            }
            ViewBag.ResponsibleName = JsonConvert.SerializeObject(ResPonsibleName);
            ViewBag.ResTotalSupport = JsonConvert.SerializeObject(ResTotal);
            //---------------Complete Tickets-------------------//
            var allTicketsAssigned = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).GroupBy(s => s.ResponsibleId).Select(g => new { Responsible = g.Key, Total = g.Count() }) select s;
            var CompletedTictesAssigned = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s=>s.StatusId==2).GroupBy(s => s.ResponsibleId).Select(g => new { Responsible = g.Key, Total = g.Count() }) select s;
            //--------------Pending Tickets--------------//
            var PendingTictesAssigned = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s => s.StatusId == 1).GroupBy(s => s.ResponsibleId).Select(g => new { Responsible = g.Key, Total = g.Count() }) select s;
            //-------Onhold Tickets---------------//
            var OnholdTictesAssigned = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(s => s.StatusId == 3).GroupBy(s => s.ResponsibleId).Select(g => new { Responsible = g.Key, Total = g.Count() }) select s;

            //===========Todays tickets===============//
            var AllTodayTictesAssigned = from s in db.MySupportContext
                                       .Where(y => y.CreatedDate.Year == currentYear)
                                       .Where(d=>d.CreatedDate.Day==currentDay)
                                       join t in db.MySupportTeam on s.ResponsibleId equals t.Id
                                       group 1 by t.FullName into g

                                       select new SupportChartVM
                                       {
                                           Responsible = g.Key,
                                           TotalSupport = g.Count()
                                       };
            List<string> ResPonsibleNameToDay = new List<string>();
            List<int> ResTotalToDay = new List<int>();
            foreach (var items in AllTodayTictesAssigned)
            {
                ResPonsibleNameToDay.Add(items.Responsible);
                ResTotalToDay.Add(items.TotalSupport);
            }
            ViewBag.ResPonsibleNameToDay = JsonConvert.SerializeObject(ResPonsibleNameToDay);
            ViewBag.ResTotalToDay = JsonConvert.SerializeObject(ResTotalToDay);

            ///============Temp=========================//
            /*
            var TempData = from s in db.MySupportContext
                           join t in db.MySupportTeam on s.ResponsibleId equals t.Id
                           where s.CreatedDate.Year == currentYear
                           group new { s, t } by new
                           {
                               s.ResponsibleId

                           } into v
                           select new
                           {
                               Total = v.Count(),
                               Response = v.Key

                           };

            ViewBag.AllTicketsAssigne = TempData;
            */
            //foreach (var items in AllTodayTictesAssigned)
            //{
            //    if (items.Responsible == 1)
            //    {
            //        ViewBag.FarhadTDA = items.Total;
            //    }
            //    else if (items.Responsible == 2)
            //    {
            //        ViewBag.MehdiTDA = items.Total;
            //    }
            //    else if (items.Responsible == 3)
            //    {
            //        ViewBag.KhalidTDA = items.Total;
            //    }
            //    else if (items.Responsible == 4)
            //    {
            //        ViewBag.NaserTDA = items.Total;
            //    }
            //    else if (items.Responsible == 5)
            //    {
            //        ViewBag.AzizTDA = items.Total;
            //    }
            //    else if (items.Responsible == 6)
            //    {
            //        ViewBag.MasoodTDA = items.Total;
            //    }
            //    else if (items.Responsible == 7)
            //    {
            //        ViewBag.ShafiqTDA = items.Total;
            //    }
            //    else if (items.Responsible == 8)
            //    {
            //        ViewBag.SiawashTDA = items.Total;
            //    }
            //    else if (items.Responsible == 9)
            //    {
            //        ViewBag.FardinTDA = items.Total;
            //    }
            //    else if (items.Responsible == 10)
            //    {
            //        ViewBag.MirwaisTDA = items.Total;
            //    }

            //}

            foreach (var items in allTicketsAssigned)
            {
                if (items.Responsible == 1)
                {
                    ViewBag.Farhad = items.Total;
                }
                else if (items.Responsible == 2)
                {
                    ViewBag.Mehdi = items.Total;
                }
                else if (items.Responsible == 3)
                {
                    ViewBag.Khalid = items.Total;
                }
                else if (items.Responsible == 4)
                {
                    ViewBag.Naser = items.Total;
                }
                else if (items.Responsible == 5)
                {
                    ViewBag.Aziz = items.Total;
                }
                else if (items.Responsible == 6)
                {
                    ViewBag.Masood = items.Total;
                }
                else if (items.Responsible == 7)
                {
                    ViewBag.Shafiq = items.Total;
                }
                else if (items.Responsible == 8)
                {
                    ViewBag.Siawash = items.Total;
                }
                else if (items.Responsible == 9)
                {
                    ViewBag.Fardin = items.Total;
                }
                else if (items.Responsible == 10)
                {
                    ViewBag.Mirwais = items.Total;
                }

            }

            foreach (var items in CompletedTictesAssigned)
            {
                if (items.Responsible == 1)
                {
                    ViewBag.FarhadC = items.Total;
                }
                else if (items.Responsible == 2)
                {
                    ViewBag.MehdiC = items.Total;
                }
                else if (items.Responsible == 3)
                {
                    ViewBag.KhalidC = items.Total;
                }
                else if (items.Responsible == 4)
                {
                    ViewBag.NaserC = items.Total;
                }
                else if (items.Responsible == 5)
                {
                    ViewBag.AzizC = items.Total;
                }
                else if (items.Responsible == 6)
                {
                    ViewBag.MasoodC = items.Total;
                }
                else if (items.Responsible == 7)
                {
                    ViewBag.ShafiqC = items.Total;
                }
                else if (items.Responsible == 8)
                {
                    ViewBag.SiawashC = items.Total;
                }
                else if (items.Responsible == 9)
                {
                    ViewBag.FardinC = items.Total;
                }
                else if (items.Responsible == 10)
                {
                    ViewBag.MirwaisC = items.Total;
                }

            }

            foreach (var items in PendingTictesAssigned)
            {
                if (items.Responsible == 1)
                {
                    ViewBag.FarhadP = items.Total;
                }
                else if (items.Responsible == 2)
                {
                    ViewBag.MehdiP = items.Total;
                }
                else if (items.Responsible == 3)
                {
                    ViewBag.KhalidP = items.Total;
                }
                else if (items.Responsible == 4)
                {
                    ViewBag.NaserP = items.Total;
                }
                else if (items.Responsible == 5)
                {
                    ViewBag.AzizP = items.Total;
                }
                else if (items.Responsible == 6)
                {
                    ViewBag.MasoodP = items.Total;
                }
                else if (items.Responsible == 7)
                {
                    ViewBag.ShafiqP = items.Total;
                }
                else if (items.Responsible == 8)
                {
                    ViewBag.SiawashP = items.Total;
                }
                else if (items.Responsible == 9)
                {
                    ViewBag.FardinP = items.Total;
                }
                else if (items.Responsible == 10)
                {
                    ViewBag.MirwaisP = items.Total;
                }

            }

            foreach (var items in OnholdTictesAssigned)
            {
                if (items.Responsible == 1)
                {
                    ViewBag.FarhadH = items.Total;
                }
                else if (items.Responsible == 2)
                {
                    ViewBag.MehdiH = items.Total;
                }
                else if (items.Responsible == 3)
                {
                    ViewBag.KhalidH = items.Total;
                }
                else if (items.Responsible == 4)
                {
                    ViewBag.NaserH = items.Total;
                }
                else if (items.Responsible == 5)
                {
                    ViewBag.AzizH = items.Total;
                }
                else if (items.Responsible == 6)
                {
                    ViewBag.MasoodH = items.Total;
                }
                else if (items.Responsible == 7)
                {
                    ViewBag.ShafiqH = items.Total;
                }
                else if (items.Responsible == 8)
                {
                    ViewBag.SiawashH = items.Total;
                }
                else if (items.Responsible == 9)
                {
                    ViewBag.FardinH = items.Total;
                }
                else if (items.Responsible == 10)
                {
                    ViewBag.MirwaisH = items.Total;
                }

            }

            foreach (var items in TotalMinutesWorked)
            {
                if (items.ResponsibleTotalM == 1)
                {
                    ViewBag.FarhadMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 2)
                {
                    ViewBag.MehdiMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 3)
                {
                    ViewBag.KhalidMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 4)
                {
                    ViewBag.NaserMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 5)
                {
                    ViewBag.AzizMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 6)
                {
                    ViewBag.MasoodMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 7)
                {
                    ViewBag.ShafiqMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 8)
                {
                    ViewBag.SiawashMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 9)
                {
                    ViewBag.FardinMW = items.Total;
                }
                else if (items.ResponsibleTotalM == 10)
                {
                    ViewBag.MirwaisMW = items.Total;
                }

            }

            foreach (var items in TotalMinutesWorkedToday)
            {
                if (items.ResponsibleTotalM == 1)
                {
                    ViewBag.FarhadMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 2)
                {
                    ViewBag.MehdiMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 3)
                {
                    ViewBag.KhalidMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 4)
                {
                    ViewBag.NaserMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 5)
                {
                    ViewBag.AzizMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 6)
                {
                    ViewBag.MasoodMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 7)
                {
                    ViewBag.ShafiqMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 8)
                {
                    ViewBag.SiawashMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 9)
                {
                    ViewBag.FardinMWTD = items.Total;
                }
                else if (items.ResponsibleTotalM == 10)
                {
                    ViewBag.MirwaisMWTD = items.Total;
                }

            }
            return View();
        }

        public ActionResult ProjectBased()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDay = DateTime.Now.Day;
            //-----------All Tickets----------------//
            var All_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear) select s;
            ViewBag.All_tickets_count = All_Tictes_count_query.Count();

            //===========Pending tickets================//
            var All_pending_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 1) select s;
            ViewBag.All_pending_count = All_pending_Tictes_count_query.Count();
            //===========Onhold tickets================//
            var All_onhold_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 3) select s;
            ViewBag.All_onhold_count = All_onhold_Tictes_count_query.Count();
            //===========Completed tickets================//
            var All_completed_Tictes_count_query = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear).Where(p => p.StatusId == 2) select s;
            ViewBag.All_completed_count = All_completed_Tictes_count_query.Count();
            //-----------End of counts---------------//

            var Get_supportToProjects = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear)
                                        join p in db.MyProjectsContext on s.UnitProjectId equals p.Id
                                        group 1 by p.UnitProjectName into g
                                        select new SupportChartVM
                                        {
                                            ProjectName = g.Key,
                                            TotalSupport = g.Count()
                                        };

            List<string> PN = new List<string>();
            List<int> PT = new List<int>();
            foreach(var items in Get_supportToProjects)
            {
                PN.Add(items.ProjectName);
                PT.Add(items.TotalSupport);
            }
            

            ViewBag.ProjectName = JsonConvert.SerializeObject(PN);

            ViewBag.TotalSupport = JsonConvert.SerializeObject(PT);
            //ViewBag.QueryResult = Get_supportToProjects.ToList();
            return View(Get_supportToProjects);
        }

        public ActionResult SLABased()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDay = DateTime.Now.Day;

            //var TotalTicketsAssigned = from s in db.MySupportContext
            //               .Where(y => y.CreatedDate.Year == currentYear)
            //                           join t in db.MySupportTeam on s.ResponsibleId equals t.Id
            //                           group 1 by t.FullName into g

            //                           select new SupportChartVM
            //                           {
            //                               Responsible = g.Key,
            //                               TotalSupport = g.Count()
            //                           };
            //List<string> ResPonsibleName = new List<string>();
            //List<int> ResTotal = new List<int>();
            //foreach (var items in TotalTicketsAssigned)
            //{
            //    ResPonsibleName.Add(items.Responsible);
            //    ResTotal.Add(items.TotalSupport);
            //}
            //ViewBag.ResponsibleName = JsonConvert.SerializeObject(ResPonsibleName);
            //ViewBag.ResTotalSupport = JsonConvert.SerializeObject(ResTotal);

            //var minutesworked = from s in db.MySupportContext
            //                    .Where(y => y.CreatedDate.Year == currentYear)
            //                    join t in db.MySupportTeam on s.ResponsibleId equals t.Id
            //                    join st in db.MySupportTypeContext on s.SupportTypeId equals st.Id
            //                    group st by new { s.ResponsibleId, st.Minutes, t.FullName } into g
            //                    select new SupportChartVM
            //                    {
            //                        Responsible = g.Key.FullName,
            //                        TotalSupport = g.Sum(m => m.Minutes)
            //                    };

            //using (var context = new MyDbContext())
            //{
            //    //var blogs = context.MySupportContext.SqlQuery("SELECT * FROM dbo.Supports").ToList();
            //    //string myQuery = "Select id, FullName, sum(mins) as Mins from (Select t.id, FullName, NumberOfSupports* st.Minutes as mins from(Select st.Id, st.FullName, count(s.Id) as NumberOfSupports, s.SupportTypeId from SupportTeams stINNER JOIN Supports s on s.ResponsibleId = st.Idgroup by st.id, st.FullName, s.SupportTypeId) t inner join SupportTypes st on st.id = t.SupportTypeId) t group by id, FullName ";


            //   // var blogs = context.Database.SqlQuery<string>(myQuery);
            //    var blogs = context.Database.ExecuteSqlCommand(@"Select id, FullName, sum(mins) as Mins from (Select t.id, FullName, NumberOfSupports* st.Minutes as mins from(Select st.Id, st.FullName, count(s.Id) as NumberOfSupports, s.SupportTypeId from SupportTeams st INNER JOIN Supports s on s.ResponsibleId = st.Id group by st.id, st.FullName, s.SupportTypeId) t inner join SupportTypes st on st.id = t.SupportTypeId) t group by id, FullName ");

            //    string buyerSequenceNumber = string.Empty;

            //    if (blogs != null)
            //    {
            //        buyerSequenceNumber = blogs.ToString();
            //    }
            //}

            var minutesworked = from s in db.MySupportContext.Where(y => y.CreatedDate.Year == currentYear)
                                join t in db.MySupportTeam on s.ResponsibleId equals t.Id
                                group new { s.Id } by new { t.Id, t.FullName, s.SupportTypeId } into g
                                select new { g.Key.Id, g.Key.FullName, g.Key.SupportTypeId} into gt
                                join st in db.MySupportTypeContext on gt.Id equals st.Id
                                group new { st.Minutes } by new { gt.Id, gt.FullName } into g1
                                select new { g1.Key.Id, g1.Key.FullName, mins = g1.Sum(a => a.Minutes) };

            return View();
        }
    }


}