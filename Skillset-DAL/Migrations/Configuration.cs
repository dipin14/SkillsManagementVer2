namespace Skillset_DAL.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Skillset_DAL.ContextClass.SkillsetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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
            new Role { Id = 2, Name = "Manager" },
            new Role { Id = 3, Name = "Employee" }
           );
            context.Ratings.AddOrUpdate(
                p => p.Id,
                new Rating { Id = 1, Value = 1, Note = "Not so good" },
                new Rating { Id = 2, Value = 2, Note = "Need help" },
                new Rating { Id = 3, Value = 3, Note = "Need reference" },
                new Rating { Id = 4, Value = 4, Note = "Okay" },
                new Rating { Id = 5, Value = 5, Note = "Perfect" }
             );
            context.Skills.AddOrUpdate(
                  p => p.SkillId,
                   new Skill { SkillId = 1, SkillName = "Special skill", SkillDescription = "If you have any special Skills", Status = false }

                );

            context.SaveChanges();

            if (context.Employees.Find(1) == null)
            {
                context.Database.ExecuteSqlCommand("INSERT INTO public.\"Employees\"(\"Id\", \"EmployeeCode\", \"Name\", \"DateOfJoining\", \"DesignationId\", \"RoleId\", \"QualificationId\", \"Experience\", \"DateOfBirth\", \"EmployeeId\", \"Address\", \"Email\", \"MobileNumber\", \"Gender\", \"Status\") VALUES(2, 'E102', 'Pradeep', '2017-11-01', 2, 2, 2, 10, '1995-05-05', 2, 'Kakkanad', 'pradeep@gmail.com', 9191919191, 'Male', true);");
                context.Database.ExecuteSqlCommand("INSERT INTO public.\"Employees\"(\"Id\", \"EmployeeCode\", \"Name\", \"DateOfJoining\", \"DesignationId\", \"RoleId\", \"QualificationId\", \"Experience\", \"DateOfBirth\", \"EmployeeId\", \"Address\", \"Email\", \"MobileNumber\", \"Gender\", \"Status\") VALUES(1, 'E101', 'Raj', '2017-11-01', 1, 1, 1, 10, '1996-05-05', 2, 'Infopark', 'johnhonai@gmail.com', 9876543210, 'Male', true);");
            }
        }
    }
}
