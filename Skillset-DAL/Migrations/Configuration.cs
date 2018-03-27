namespace Skillset_DAL.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Skillset_DAL.ContextClass.SkillsetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Skillset_DAL.ContextClass.SkillsetDbContext context)
        {
            context.Designations.AddOrUpdate(
            p => p.Id,
            new Designation { Id = 1, Name = "Administrator" },
            new Designation { Id = 2, Name = "Project Manager" },
            new Designation { Id = 3, Name = "Team Lead" },
            new Designation { Id = 4, Name = "Software Engineer" },
            new Designation { Id = 5, Name = "Business Analyst" }
           );
            context.Qualifications.AddOrUpdate(
            p => p.Id,
            new Qualification { Id = 1, Name = "MBA" },
            new Qualification { Id = 2, Name = "M.Tech" },
            new Qualification { Id = 3, Name = "MCA" },
            new Qualification { Id = 4, Name = "B.Tech" },
            new Qualification { Id = 5, Name = "BCA" }
           );
            context.Roles.AddOrUpdate(
            p => p.Id,
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 1, Name = "Manager" },
            new Role { Id = 1, Name = "Employee" }
           );
        }
    }
}
