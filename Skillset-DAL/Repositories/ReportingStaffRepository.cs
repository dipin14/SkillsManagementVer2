using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillset_DAL.Models;
using Skillset_DAL.ContextClass;

namespace Skillset_DAL.Repositories
{
    public class ReportingStaff : IReportingStaffRepository
    {
        /// <summary>
        /// Retrieve designations of employees under a manager
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of Designations</returns>
        public IEnumerable<Designation> GetDesignationDetails(string managerCode)
        {
            int managerId = GetEmployeeId(managerCode);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var designations = (from d in context.Designations
                                    from e in context.Employees
                                    where (e.Status && e.EmployeeId == managerId && e.Id != e.EmployeeId && d.Id == e.DesignationId)
                                    select d).Distinct().ToList();
                if (designations.Any())

                    return designations;
                else
                    return Enumerable.Empty<Designation>().ToList();
            }
        }

        /// <summary>
        /// Retrieve all skills
        /// </summary>
        /// <returns>List of Skills</returns>
        public IEnumerable<Skill> GetSkillDetails()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var skills = context.Skills.Distinct().ToList();
                if (skills.Any())
                    return skills;
                else
                    return Enumerable.Empty<Skill>().ToList();
            }

        }

        /// <summary>
        /// Retrieve all details regarding rating values.
        /// </summary>
        /// <returns>List of Ratings</returns>
        public IEnumerable<Rating> GetRatingDetails()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var ratings = context.Ratings.Distinct().ToList();
                if (ratings.Any())
                    return ratings;
                else
                    return Enumerable.Empty<Rating>().ToList();
            }

        }

        /// <summary>
        /// Retrieve List of employees under a manager
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of Employees</returns>
        public IEnumerable<Employee> GetEmployeeDetails(string managerCode)
        {
            int managerId = GetEmployeeId(managerCode);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                //Retrieved all employees reporting to a manager excluding himself and admin. Admin's RoleId by default is set to 1 in Migrations/Configuration.cs
                var employees = context.Employees.ToList().Where(s => s.Status && s.EmployeeId == managerId && s.Id != s.EmployeeId && s.RoleId!=1);
                if (employees.Any())
                    return employees.Distinct().ToList();
                else
                    return Enumerable.Empty<Employee>().ToList();
            }

        }

        /// <summary>
        /// Retrieve List of skill ratings of an employee
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of SkillRatings</returns>
        public IEnumerable<SkillRating> GetSkillRatingsDetails(string employeeCode)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var skillRatings = (from d in context.SkillRatings
                                    from e in context.Employees
                                    where (e.Status && e.EmployeeCode.Equals(employeeCode) && d.EmployeeId == e.Id && d.Status)
                                    select d).Distinct().ToList();
                if (skillRatings.Any())
                    return skillRatings;
                else
                    return Enumerable.Empty<SkillRating>().ToList();
            }

        }

        /// <summary>
        /// Retrieve details of an employee corresponding to an Employee Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee</returns>
        public Employee GetProfile(string id)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Employees.Where(e => e.EmployeeCode == id).FirstOrDefault();
            }
        }
        /// <summary>
        /// Get employee id corresponding to an employee code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>int</returns>
        public int GetEmployeeId(string employeeCode)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var managerId = context.Employees.Where(x => x.EmployeeCode == employeeCode).Select(employee => employee.Id).FirstOrDefault();
                return managerId;
            }
        }    
    }
  }
