using Microsoft.AspNet.Identity.EntityFramework;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.AppEntity;
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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Rename Identity tables
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
        }

        //entities set
        public virtual IDbSet<Employee> Employees { get; set; }
    }
}
