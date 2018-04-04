﻿using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves all the employee record with status true
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetAllEmployees();
        /// <summary>
        /// Retrieves the employee record according to the search key
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<Employee> GetSearchRecords(string search);
        /// <summary>
        /// Adds new employee to the database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int AddEmployee(Employee employee);
        /// <summary>
        ///Updates the employee record
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int EditEmployee(Employee employee);
        /// <summary>
        /// Set the status of employee to false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteEmployee(string id);
        /// <summary>
        /// Retrieve the details of the employee with particular id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Employee GetEmployeeDetails(string id);
        /// <summary>
        /// Checks if duplicate employee exist with same employee code, mobile number or email
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        int CheckDuplicateEmployee(List<Employee> employeeList, Employee newEmployee);
        /// <summary>
        /// retrieves the list of designations
        /// </summary>
        /// <returns></returns>
        List<Designation> GetDesignations();
        /// <summary>
        /// retrieves the list of qualifications
        /// </summary>
        /// <returns></returns>
        List<Qualification> GetQualifications();
        /// <summary>
        /// retrieves the list of managers
        /// </summary>
        /// <returns></returns>
        List<Employee> GetManagers();
        /// <summary>
        /// retrieves the list of roles
        /// </summary>
        /// <returns></returns>
        List<Role> GetRole();
        /// <summary>
        /// Get recently rated 5 employee details
        /// </summary>
        /// <returns></returns>
        List<Employee> GetRecentEmployees();
        /// <summary>
        /// retrieve the name for a particular designation id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetDesignationName(string id);
        /// <summary>
        /// retrieve the name for a particular qualification id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetQualificationName(string id);
        /// <summary>
        /// retrieve the name for a particular manager id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetManagerName(string id);
        /// <summary>
        /// retrieve the name for a particular role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetRoleName(string id);
        /// <summary>
        /// Get skill names of skills rated by employee
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkill();
        /// <summary>
        /// Get skill names of skills rated by employee excluding special skill
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkillExcludeSpecial();
        /// <summary>
        /// Get total ratings for each skill given by employees
        /// </summary>
        /// <returns></returns>
        string GetEmployeeRating();
        /// <summary>
        /// Return total skill count
        /// </summary>
        /// <returns></returns>
        int GetSkillsCount();
        /// <summary>
        /// Return total employees count
        /// </summary>
        /// <returns></returns>
        int GetEmployeesCount();
        /// <summary>
        /// Return total skill ratings count
        /// </summary>
        /// <returns></returns>
        int GetSkillRatingsCount();
        string GetRatingAverage();
        void Dispose();
        Employee GetProfile(string id);
    }
}
