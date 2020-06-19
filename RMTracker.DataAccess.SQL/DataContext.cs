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
        public DbSet<Sub_C2B> Subc2bs { get; set; }
        public DbSet<On_Hold> Onhold { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<s_Lamination> SLaminations { get; set; }
        public DbSet<s_Cut> SCuts { get; set; }
        public DbSet<s_Edgebanding> SEdgebandings { get; set; }
        public DbSet<s_Drill> SDrills { get; set; }
        public DbSet<s_Painting> SPaintings { get; set; }
        public DbSet<s_Cleaning> SCleanings { get; set; }
        public DbSet<s_Packing> SPackings { get; set; }
        public DbSet<s_QC> SQCs { get; set; }
        public DbSet<s_Pickup> SPickups { get; set; }
        public DbSet<UserList> UserLists { get; set; }
        public DbSet<MachineList> MachineLists { get; set; }
        public DbSet<Departmentuser> Departmentusers { get; set; }
        public DbSet<WorksPause> WorksPauses { get; set; }
        public DbSet<WorksDenine> WorksDenines { get; set; }
        public DbSet<SO_PAUSE> SOPAUSEs { get; set; }
        public DbSet<ReasonDenine> ReasonDenines { get; set; }
        public DbSet<ReasonPause> ReasonPauses { get; set; }
    }
}
