using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.Models
{
    public class Projects
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Unit/Project Name")]
        public string UnitProjectName { get; set; }
        public int ProjectStatus { get; set; }
    }
}
