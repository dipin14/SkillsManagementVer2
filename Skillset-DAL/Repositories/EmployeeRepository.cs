﻿using Skillset_DAL.ContextClass;
using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int AddEmployee(Employee employee)
        {
            var employeeList = new List<Employee>();
            try
            {
                using (SkillsetDbContext context = new SkillsetDbContext())
                {
                    employeeList = context.Employees.ToList();
                    int status = CheckDuplicateEmployee(employeeList, employee);
                    if (status == 0)
                    {
                        employee.Status = true;
                        context.Employees.Add(employee);
                        context.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return status;
                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public int CheckDuplicateEmployee(List<Employee> employeeList, Employee newEmployee)
        {
            var check = new List<Employee>();
            check = employeeList.Where(p => p.EmployeeCode == newEmployee.EmployeeCode).ToList();
            if (check.Count != 0)
            {
                return 1;
            }
            check = employeeList.Where(p => p.MobileNumber == newEmployee.MobileNumber).ToList();
            if (check.Count != 0)
            {
                return 2;
            }
            check = employeeList.Where(p => p.Email == newEmployee.Email).ToList();
            if (check.Count != 0)
            {
                return 3;
            }
            return 0;

        }

        public int DeleteEmployee(string id)
        {
            try
            {
                using (SkillsetDbContext context = new SkillsetDbContext())
                {
                    Employee employee = context.Employees.Where(p => p.EmployeeCode == id && p.Status == true).Single();
                    employee.Status = false;
                    context.Entry(employee).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public int EditEmployee(Employee employee)
        {
            try
            {
                using (SkillsetDbContext context = new SkillsetDbContext())
                {
                    int id = Convert.ToInt32(context.Employees.Where(p => p.EmployeeCode == employee.EmployeeCode).Select(p => p.Id).Single());
                    employee.Id = id;
                    employee.Status = true;
                    context.Entry(employee).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Employees.Where(p => p.Status == true && p.RoleId != 1).OrderBy(p => p.EmployeeCode).ToList();
            }
        }

        public string GetDesignationName(string id)
        {

            int designationId = Convert.ToInt32(id);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var name = context.Designations.Where(p => p.Id == designationId).Select(p => p.Name).Single();
                return name.ToString();
            }

        }

        public List<Designation> GetDesignations()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Designations.Where(p => p.Id != 1).ToList();
            }
        }

        public Employee GetEmployeeDetails(string id)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Employees.Where(p => p.EmployeeCode == id && p.Status == true).Single();
            }
        }

        public string GetManagerName(string id)
        {
            int managerId = Convert.ToInt32(id);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var name = context.Employees.Where(p => p.Id == managerId).Select(p => p.Name).Single();
                return name.ToString();
            }
        }

        public List<Employee> GetManagers()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Employees.Where(p => p.RoleId == 2 && p.Status == true).ToList();
            }
        }

        public string GetQualificationName(string id)
        {
            int qualificationId = Convert.ToInt32(id);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var name = context.Qualifications.Where(p => p.Id == qualificationId).Select(p => p.Name).Single();
                return name.ToString();
            }
        }

        public List<Qualification> GetQualifications()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Qualifications.ToList();
            }
        }

        public List<Role> GetRole()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Roles.Where(p => p.Id != 1).ToList();
            }
        }

        public string GetRoleName(string id)
        {
            int roleId = Convert.ToInt32(id);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var name = context.Roles.Where(p => p.Id == roleId).Select(p => p.Name).Single();
                return name.ToString();
            }
        }

        public IEnumerable<Employee> GetSearchRecords(string option, string search)
        {
            try
            {
                using (SkillsetDbContext context = new SkillsetDbContext())
                {
                    if (option == "Employee Code")
                    { 
                        return context.Employees.Where(p => p.Status == true && p.EmployeeCode==search && p.RoleId !=1).Select(p => p).OrderBy(p=>p.EmployeeCode).ToList();
                    }
                    else if (option == "Name")
                    {
                        return context.Employees.Where(p => p.Status == true && p.Name==search && p.RoleId != 1).Select(p => p).OrderBy(p => p.EmployeeCode).ToList();
                    }
                    else if (option == "Designation")
                    {
                        var employeeList = from e in context.Employees from d in context.Designations where ((e.Status == true) && (e.DesignationId == d.Id) && (d.Name.Equals(search)) && e.RoleId != 1) select e;
                        return employeeList.OrderBy(p => p.EmployeeCode).ToList();
                    }
                    else
                        return null;
                }
            }
            catch
            {
                return null;
            }
           
        }

        /// <summary>
        /// Get recently rated 5 employee details
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetRecentEmployees()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var RecentEmployees = (from s in context.SkillRatings
                                       join j in context.Employees
                                       on s.EmployeeId equals j.Id
                                       where s.Status == true
                                       orderby s.Id descending
                                       select j).ToList();
                var DistnctEmployees = RecentEmployees.Distinct().Take(2).ToList();
                return DistnctEmployees;
            }
        }

        /// <summary>
        /// Get skill names of skills rated by employee
        /// </summary>
        /// <returns></returns>
        public IQueryable<string> GetEmployeeRatedSkill()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where s.Status == true && j.Status==true
                              select new { j.SkillName, j.SkillId }).Distinct();

                var skill = from s in skills
                            orderby s.SkillId descending
                            select s.SkillName;
                
                return skill;
            }
        }

        /// <summary>
        /// Get total ratings for each skill given by employees
        /// </summary>
        /// <returns></returns>
        public string GetEmployeeRating()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                string result = string.Empty;
                string id = string.Empty;
                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where j.Status == true && s.Status==true
                              select j);
                var groupRating = skills.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Distinct().Count() });
               
                foreach (var r in groupRating.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    result += r;
                    result += ", ";
                }
                return result;
            }
        }
        /// <summary>
        /// Return total skill count
        /// </summary>
        /// <returns></returns>
        public int GetSkillsCount()
        {
            int skillsCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext()) 
            {
                skillsCount = context.Skills.Where(s=>s.Status).Distinct().Count();
            }
            return skillsCount;
        }
        /// <summary>
        /// Return total skill ratings count
        /// </summary>
        /// <returns></returns>
        public int GetSkillRatingsCount()
        {
            int skillRatingsCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                skillRatingsCount = context.SkillRatings.Where(s => s.Status).Distinct().Count();
            }
            return skillRatingsCount;
        }
        /// <summary>
        /// Return total employees count
        /// </summary>
        /// <returns></returns>
        public int GetEmployeesCount()
        {
            int employeesCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                //Exclude Admin
                employeesCount = (context.Employees.Where(s => s.Status).Distinct().Count())-1;
            }
            return employeesCount;
        }

        /// <summary>
        /// Get skill names of skills rated by employee excluding special skill
        /// </summary>
        /// <returns></returns>
        public IQueryable<string> GetEmployeeRatedSkillExcludeSpecial()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where s.Status == true && j.Status==true
                              select new { j.SkillName, j.SkillId }).Distinct();

                var skill = from s in skills
                            orderby s.SkillId descending
                            where s.SkillName != "Special Skill"
                            select s.SkillName;

                return skill;
            }
        }

        public string GetRatingAverage()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                List<int> totalValues = new List<int>();
                List<int> specificValues = new List<int>();
                var result = string.Empty;
                string id = string.Empty;
                var rating = context.SkillRatings.Where(x => x.Status == true);
                var groupRating = rating.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Distinct().Count() });

                foreach (var r in groupRating.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    totalValues.Add(r);
                }

                var skill = (from sr in context.SkillRatings
                            join r in context.Ratings
                            on sr.RatingId equals r.Id
                            join s in context.Skills
                            on sr.SkillId equals s.SkillId
                            where sr.Status == true && s.Status==true
                            select new { sr.SkillId , r.Value });

                var groupSkill = skill.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Select(s => s.Value).Sum() });
                foreach (var r in groupSkill.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    specificValues.Add(r);
                }
                for(int i =0;i<specificValues.Count;i++)
                {
                    float ratingAvg = (float)specificValues.ElementAt(i) / (float)totalValues.ElementAt(i);
                    result += ratingAvg;
                    result += ", ";
                }
                return result;

            }
        }
        public Employee GetProfile(string id)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Employees.Where(e => e.EmployeeCode == id).FirstOrDefault();
            }
        }
        public void Dispose()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {

            }
        }
    }
}
