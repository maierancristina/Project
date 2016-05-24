namespace HeartPharam.Migrations
{
    using HeartPharam.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HeartPharam.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "HeartPharam.Models.ApplicationDbContext";
        }

        protected override void Seed(HeartPharam.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = { "Admin", "Pharmacist" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                if (!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));
                }
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole("3fbaa370-d4e3-474b-9a4e-ac6cf99bb8d5", "Admin");
            UserManager.AddToRole("a694a9b6-f4c6-4a3b-9d0d-9e2601c020b8", "Admin");
        }

    }
}
