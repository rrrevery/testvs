using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TestEF
{
    public class TEST : DbContext
    {
        public TEST() : base("OracleDbContext") { }

        //public DbSet<RRR1> RRR1 { get; set; }
        //public DbSet<SHDY> SHDY { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BFCRM10");
        }
    }
}
