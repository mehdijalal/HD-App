using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.ViewModels
{
    public class SupportChartVM
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int TotalSupport { get; set; }
        public int MinutesWorked { get; set; }
        public string TeamName { get; set; }
        public string Responsible { get; set; }

    }
}
