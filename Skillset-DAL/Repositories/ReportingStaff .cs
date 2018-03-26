using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillset_DAL.Models;
using Skillset_DAL.ContextClass;

namespace Skillset_DAL.Repositories
{
    public class ReportingStaff : IReportingStaff
    {
        /// <summary>
        /// Retrieve designations of employees under a manager
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of Designations</returns>
        public IEnumerable<Designation> GetDesignationDetails(string managerCode)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var designations = (from d in context.Designations
                                    from e in context.Employees
                                    where (e.Status && e.EmployeeCode == managerCode && d.Id == e.DesignationId)
                                    select d).ToList();

                return designations;
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
                var skills = context.Skills.Where(s => s.status).ToList();

                return skills;
            }

        }

        /// <summary>
        /// Retrieve ratings
        /// </summary>
        /// <returns>List of Ratings</returns>
        public IEnumerable<Rating> GetRatingDetails()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var ratings = context.Ratings.ToList();

                return ratings;
            }

        }

        /// <summary>
        /// Retrieve List of employees under a manager
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of Employees</returns>
        public IEnumerable<Employee> GetEmployeeDetails(string managerCode)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var employees = context.Employees.ToList().Where(s => s.Status && s.EmployeeCode == managerCode);
                return employees;
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
                                    select d).ToList();
                return skillRatings;
            }

        }
    }
  }
