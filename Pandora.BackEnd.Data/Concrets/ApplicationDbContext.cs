using Microsoft.AspNet.Identity.EntityFramework;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.AppDomain;
using Pandora.BackEnd.Model.Users;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Pandora.BackEnd.Data.Concrets
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
#if DEBUG
        public ApplicationDbContext() : base("name=DebugConnection") { }
#else
        public ApplicationDbContext() : base("name=ReleaseConnection") { }
#endif

        public DbSet<Employee> Employees { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
