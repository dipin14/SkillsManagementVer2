using Skillset_DAL.ContextClass;
using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public class AdminEmployeeSkillRepository : IAdminEmployeeSkillRepository
    {
        public List<Employee> GetEmployeeDetails(string option, string searchKey)
        {
            using (var context = new SkillsetDbContext())
            {
                if (option == "Employee Code")
                {
                    return context.Employees.Where(m => m.Status == true && m.EmployeeCode.Contains(searchKey)).Select(m => m).ToList();
                }
                else if (option == "Name")
                {
                    return context.Employees.Where(m => m.Status == true && m.Name.Contains(searchKey)).Select(m => m).ToList();
                }
                else if (option == "Designation")
                {
                    var query = from e in context.Employees
                                from d in context.Designations
                                where ((e.Status == true) && (e.DesignationId == d.Id) && (d.Name.Equals(searchKey)))
                                select e;
                    return query.ToList();
                }
                else
                {
                    return context.Employees.Where(e => e.Status == true).Select(e => e).ToList();
                }
            }
        }

        public string FindDesignation(int designationId)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Designations.Where(d => d.Id == designationId).Select(d => d.Name).FirstOrDefault();
            }
        }
        public List<SkillRating> GetSkillDetails(string employeeCode)
        {
            using (var context = new SkillsetDbContext())
            {
                int empId = FindId(employeeCode);
                return context.SkillRatings.Where(e => e.EmployeeId == empId).Select(e => e).ToList();
            }
        }

        public string FindSkillName(int skillId)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Skills.Where(d => d.SkillId == skillId).Select(d => d.SkillName).FirstOrDefault();
            }
        }

        public int FindSkillValue(int ratingId)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Ratings.Where(d => d.Id == ratingId).Select(d => d.Value).FirstOrDefault();
            }
        }

        //method to find id of the specified employees's record
        int FindId(string employeeCode)
        {
            using (var context = new SkillsetDbContext())
            {
                int empId = context.Employees.Where(m => m.EmployeeCode == employeeCode).Select(m => m.Id).FirstOrDefault();
                return empId;
            }
        }
    }
}