﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.Models
{
    public class ICTServices
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceMonthlyCost { get; set; }
        public string Description { get; set; }
    }
}
