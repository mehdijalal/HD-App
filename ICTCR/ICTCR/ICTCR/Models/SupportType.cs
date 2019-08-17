using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.Models
{
    public class SupportType
    {
        public int Id { get; set; }
        public string SupportName { get; set; }
        public double CostPerSupport { get; set; }
        public int Minutes { get; set; }
        public double TimeToAction { get; set; }
        public int DateTimeSpanId { get; set; }
        public string Description { get; set; }


    }
}
