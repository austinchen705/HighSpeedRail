
using HighSpeedRail.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HighSpeedRail.DBContext
{
    public class HSRContext : DbContext
    {
        public DbSet<Canibet> Canibets { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CanibetDetail> CanibetDetails { get; set; }
    }
}