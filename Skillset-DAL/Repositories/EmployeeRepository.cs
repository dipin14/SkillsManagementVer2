﻿using Skillset_DAL.ContextClass;
using Skillset_DAL.Models;
using System;
using System.Collections;
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
            int employeeCount;
            try
            {
                using (SkillsetDbContext context = new SkillsetDbContext())
                {
                    employeeList = context.Employees.ToList();
                    employeeCount = employeeList.Count();
                    //checks for duplicate employee
                    int status = CheckDuplicateEmployee(employeeList, employee);
                    //if duplicate employee is not found
                    if (status == 0)
                    {
                        employee.Status = true;
                        employee.Id = employeeCount + 1;
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
            //Gets the list of employees with same employeecode
            check = employeeList.Where(p => p.EmployeeCode == newEmployee.EmployeeCode).ToList();
            if (check.Count != 0)
            {
                return 1;
            }
            //Gets the list of employees with same mobilenumber
            check = employeeList.Where(p => p.MobileNumber == newEmployee.MobileNumber).ToList();
            if (check.Count != 0)
            {
                return 2;
            }
            //Gets the list of employees with same email
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
                    //Get the Employee record
                    Employee employee = context.Employees.Where(p => p.EmployeeCode == id && p.Status == true).Single();
                    employee.Status = false;
                    //Get the list of employees whose manager is the employee to be deleted
                    var employeeList = context.Employees.Where(p => p.EmployeeId == employee.Id && p.Status == true).ToList();
                    //Managerid of all employees in the list is changed to 1
                    employeeList.ForEach(p => p.EmployeeId = 1);
                    foreach (var emp in employeeList)
                        context.Entry(emp).State = EntityState.Modified;

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
                    //gets the id of the employee
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

        public IEnumerable<Employee> GetSearchRecords(string search, int pageNumber, int pageSize, out int totalCount)
        {
            var employeeList = new List<Employee>();
            try
            {         
                int employeeCount;
                if(search!=null)
                  search=search.ToUpper();
                using (SkillsetDbContext context = new SkillsetDbContext())
                {
                    //if no search value is there
                    if (search == null || search == string.Empty)
                    {

                        employeeList = context.Employees.Where(p => p.Status == true && p.RoleId != 1).OrderBy(p => p.EmployeeCode).Skip(pageSize * pageNumber).Take(pageSize).ToList();
                        employeeCount = context.Employees.Where(p => p.Status == true && p.RoleId != 1).OrderBy(p => p.EmployeeCode).Count();
                    }
                    else
                    {
                        //Query to get the list of employees whose name code or designation matches the search key
                        var query = from e in context.Employees
                                    from d in context.Designations
                                    where (e.DesignationId == d.Id && e.Status == true && e.RoleId != 1 && d.Id != 1 && (e.Name.ToUpper().Contains(search)||d.Name.ToUpper().Contains(search)||e.EmployeeCode.ToUpper().Contains(search)))
                                    select e;
                        employeeList=query.OrderBy(p=>p.EmployeeCode).Skip(pageSize * pageNumber).Take(pageSize).ToList();
                        employeeCount = query.OrderBy(p => p.EmployeeCode).Count();
                        
                    }
                    totalCount = employeeCount;
                    return employeeList;
                }

            }
            catch
            {
                totalCount = 0;
                return employeeList;
            }

        }

        public List<KeyValuePair<string, string>> GetTopRatedRecentEmployees()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                StringBuilder TopRatedEmployeeName = new StringBuilder();

                var MaximumRatingId = context.Ratings.Where(s => s.Value == 5).Select(s => s.Id).FirstOrDefault();
                int RatingId = Convert.ToInt32(MaximumRatingId);
                var skill = (from sr in context.SkillRatings
                             join e in context.Employees
                             on sr.EmployeeId equals e.Id
                             join r in context.Ratings
                             on sr.RatingId equals r.Id
                             join s in context.Skills
                             on sr.SkillId equals s.SkillId
                             where sr.Status == true && s.Status == true && sr.RatingId == RatingId
                             orderby sr.RatingId descending
                             select new { s.SkillName, e.Name }).ToList();
                //var groupSkill = skill.GroupBy(x => x.SkillName).Select(x => new { SkillName = x.Key, EmployeeName = x.Select(s => s.Name).FirstOrDefault() }).ToList();

                List<KeyValuePair<string, string>> topSkills = new List<KeyValuePair<string, string>>();

                foreach (var skillEmp in skill)
                {
                    topSkills.Add(new KeyValuePair<string, string>(skillEmp.SkillName, skillEmp.Name));
                }

                return topSkills;
            }
        }
        
        public int GetEmployeesCount()
        {
            int employeesCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                //Exclude Admin
                employeesCount = (context.Employees.Where(s => s.Status).Distinct().Count()) - 1;
            }
            return employeesCount;
        }

       

       
        /// Get profile details for employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetProfile(string id)
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                return context.Employees.Where(e => e.EmployeeCode == id).FirstOrDefault();
            }
        }


        /// <summary>
        /// Get Skill details of an employee from table Skillrating
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public List<SkillRating> GetSkillDetails(string employeeCode)
        {
            using (var context = new SkillsetDbContext())
            {
                int empId = FindId(employeeCode);
                var query = from sr in context.SkillRatings
                            from s in context.Skills
                            where (sr.EmployeeId == empId && sr.SkillId == s.SkillId && sr.Status)
                            select sr;
                return query.OrderBy(s => s.RatingDate).ToList();
            }
        }

        /// <summary>
        /// Finding skill name from table Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        public string FindSkillName(int skillId)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Skills.Where(d => d.SkillId == skillId).Select(d => d.SkillName).FirstOrDefault();
            }
        }

        /// <summary>
        /// Finding skill value from table Rating
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        public int FindSkillValue(int ratingId)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Ratings.Where(d => d.Id == ratingId).Select(d => d.Value).FirstOrDefault();
            }
        }

        /// <summary>
        /// Finding employee name from table Employee
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
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
        /// <summary>
        ///  Finding note of a rating value from table Rating
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        public string FindRatingNote(int ratingId)
        {
            using (var context = new SkillsetDbContext())
            {
                return context.Ratings.Where(d => d.Id == ratingId).Select(d => d.Note).FirstOrDefault();
            }
        }

       
    }
}
