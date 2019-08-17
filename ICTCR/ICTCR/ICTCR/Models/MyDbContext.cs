using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTCR.Models
{
    class MyDbContext:DbContext
    {
        public MyDbContext() : base("MyDbConnection")
        {

        }

        public DbSet<SupportType> MySupportTypeContext { get; set; }
        public DbSet<Supports> MySupportContext { get; set; }
        public DbSet<Projects> MyProjectsContext { get; set; }
        public DbSet<Status> MyStatusContext { get; set; }
        public DbSet<SupportTeam> MySupportTeam { get; set; }

        public System.Data.Entity.DbSet<ICTCR.Models.ICTServices> ICTServices { get; set; }

        public System.Data.Entity.DbSet<ICTCR.Models.DateTimeSpan> DateTimeSpans { get; set; }
        public DbSet<MyRoles> MyRolesContext { get; set; }
        public DbSet<MyUsers> MyUsersContext { get; set; }
    }
}
