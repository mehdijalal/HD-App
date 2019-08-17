using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.Models
{
    public class Supports
    {
        public int Id { get; set; }
        
        public string RequesterFullName { get; set; }
        
        public string RequesterEmail { get; set; }
        [Required]
        public int UnitProjectId { get; set; }
        [Required]
        public int SupportTypeId { get; set; }
        [Required]
        public int TotalNumberOfSupport { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
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
        //=================Time to Action==================//
        public DateTime DueTimeToComplete { get; set; }
        //@@@@@@@@@@@@@@@@--For Add Update--@@@@@@@@@@@@@@@@//
        public int? MinutesWorked { get; set; }
        public double? ActualCost { get; set; }
        public string AdditionalComments { get; set; }
        public int? StatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }
    }
}
