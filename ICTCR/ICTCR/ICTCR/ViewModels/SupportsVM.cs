using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.ViewModels
{
    public class SupportsVM
    {
        public int Id { get; set; }
        [Required]
        public string RequesterFullName { get; set; }
        [Required]
        public string RequesterEmail { get; set; }
        [Required]
        public int UnitProjectId { get; set; }
        [Required]
        public int SupportTypeId { get; set; }
        [Required]
        public int TotalNumberOfSupport { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        public string Remarks { get; set; }

        public int ResponsibleId { get; set; }
        //---------To Track if reassigned--------//
        public int? PrimaryResponsibleId { get; set; }
        public string ResonForTaskTransfer { get; set; }
        //======This is used for info and saving cost records===========//
        public double? SupportCost { get; set; }
        //*****************General dates******************//
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        //================Relational============//
        public string SupportTypeName { get; set; }
        public double SupportTypeCost { get; set; }
        public double TimeToAction { get; set; }
        public string ResponsibleFullName { get; set; }
        public string UnitProjectName { get; set; }
        //=================Time to Action==================//
        public DateTime DueTimeToComplete { get; set; }
        //@@@@@@@@@@@@@@@@--For Add Update--@@@@@@@@@@@@@@@@//
        public int? MinutesWorked { get; set; }
        public double? ActualCost { get; set; }
        public string AdditionalComments { get; set; }
        //##############Cost per Minute of Person############//
        public double? CostPerMinute { get; set; }
        public int? StatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }

    }
}
