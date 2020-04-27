using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<User_Works> Userworks { get; set; }
        public DbSet<Sub_C2B> Subc2b { get; set; }
        public DbSet<On_Hold> Onhold { get; set; }
    }
}
