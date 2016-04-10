using IPM.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IPM.DAL
{
    public class IPMContext :DbContext
    {
        public IPMContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Module> Modules { set; get; }
        public DbSet<ModuleAction> ModuleActions { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}