namespace Skillset_DAL.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

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
                context.Employees.AddOrUpdate(
                p => p.Id,
                new Employee { Id = 1, EmployeeCode = "E101", Name = "Raj",DateOfJoining=Convert.ToDateTime("2017-11-01"),DesignationId=1,RoleId=1,QualificationId=1,Experience=10,DateOfBirth=Convert.ToDateTime("1996-05-05"),EmployeeId=2,Address="Infopark",Email="johnhonai@gmail.com",MobileNumber=9526815652,Gender="Male",Status=true},
                 new Employee { Id = 2, EmployeeCode = "E102", Name = "Pradeep",DateOfJoining= Convert.ToDateTime("2017-11-01"),DesignationId=2,RoleId=2,QualificationId=2,Experience=10,DateOfBirth= Convert.ToDateTime("1995-05-05"),EmployeeId=2,Address="kakkanad",Email="pradeep@gmail.com",MobileNumber=9447142786,Gender="Female",Status=true}
               
                
                
             );
          
        }
    }
}
