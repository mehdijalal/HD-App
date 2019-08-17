using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.Models
{
    public class SupportTeam
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        
        public double? PerMinuteCost { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
