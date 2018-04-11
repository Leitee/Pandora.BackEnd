namespace Pandora.BackEnd.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Pandora.BackEnd.Data.Concrets;
    using Pandora.BackEnd.Data.Helpers;
    using Pandora.BackEnd.Model.AppEntity;
    using Pandora.BackEnd.Model.Users;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //  This method will be called after migrating to the latest version.
        protected override void Seed(ApplicationDbContext context)
        {
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Roles.AddOrUpdate(r => r.Id,
                new AppRole { Name = "Administrator", Description = "The highest privilege role at Application" }
                );

            context.Users.AddOrUpdate(u => u.UserName,
                new AppUser { UserName = "devadmin", PasswordHash = new PasswordHasher().HashPassword("dev123456") }
                );
            context.SaveChanges();

            var usrMngr = ContextHelper.GetUserManager(context);

            usrMngr.AddToRole(usrMngr.FindByName("devadmin").Id, "Administrator");

            context.Employees.AddOrUpdate(e => e.EmployeeId,
                new Employee { FirstName = "Jhon", LastName = "Doe", Gender = Model.GenderEnum.MAN, User = usrMngr.FindByName("devadmin") }
                );
        }
    }
}
