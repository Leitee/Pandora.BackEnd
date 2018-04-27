namespace Pandora.BackEnd.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Pandora.BackEnd.Data.Concrets;
    using Pandora.BackEnd.Data.Helpers;
    using Pandora.BackEnd.Model.AppEntity;
    using Pandora.BackEnd.Model.Users;
    using System;
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
                new AppRole { Name = "Admin", Description = "The highest privilege role at Application" }
                );

            context.Users.AddOrUpdate(u => u.UserName,
                new AppUser { UserName = "devadmin", PasswordHash = new PasswordHasher().HashPassword("dev321"), EmailConfirmed = true,
                    Email = "info@pandorasistemas.com", FirstName = "Jhon", LastName = "Doe", JoinDate = DateTime.Now, SecurityStamp = Guid.NewGuid().ToString() }
                );
            context.SaveChanges();

            var usrMngr = ContextHelper.GetUserManager(context);

            usrMngr.AddToRole(usrMngr.FindByName("devadmin").Id, "Admin");

            context.Employees.AddOrUpdate(e => e.EmployeeId,
                new Employee { BirthDate = new DateTime(1999, 12, 30), Gender = Model.GenderEnum.MAN, AppUser = usrMngr.FindByName("devadmin") }
                );
        }
    }
}
