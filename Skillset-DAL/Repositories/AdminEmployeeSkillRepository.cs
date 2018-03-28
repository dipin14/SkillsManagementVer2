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
                    return context.Employees.Where(m => m.Status == true && m.RoleId!=1 && m.EmployeeCode.Contains(searchKey)).Select(m => m).ToList();
                }
                else if (option == "Name")
                {
                    return context.Employees.Where(m => m.Status == true && m.RoleId != 1 && m.Name.Contains(searchKey)).Select(m => m).ToList();
                }
                else if (option == "Designation")
                {
                    var query = from e in context.Employees
                                from d in context.Designations
                                where ((e.Status == true) && (e.RoleId != 1) && (e.DesignationId == d.Id) && (d.Name.Contains(searchKey)))
                                select e;
                    return query.ToList();
                }
                else
                {
                    return context.Employees.Where(e => e.Status == true && e.RoleId != 1).Select(e => e).ToList();
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
                //var query = (from sr in context.SkillRatings
                //            from s in context.Skills
                //            where (sr.EmployeeId == empId && sr.Status && s.Status )
                //            select sr).ToList();
                var query = (from sr in context.SkillRatings
                             from s in context.Skills
                             where (sr.EmployeeId == empId && sr.SkillId == s.SkillId && sr.Status && s.Status)
                             select sr).ToList();
                return query.ToList();
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

        public string FindEmployeeName(string employeeCode)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Employees.Where(d => d.EmployeeCode == employeeCode).Select(d => d.Name).FirstOrDefault();
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