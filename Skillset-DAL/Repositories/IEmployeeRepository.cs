using Skillset_DAL.Models;
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
        /// Retrieves the employee record according to the search key if no search key retreives the employee list
        /// </summary>
        /// <param name="search"></param>       
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<Employee> GetSearchRecords(string search, int pageNumber, int pageSize, out int totalCount);

       
        
        /// <summary>
        /// Adds new employee to the Employee table
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
        /// Get recently rated employee details
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
        /// retrieve total employees count
        /// </summary>
        /// <returns></returns>
        int GetEmployeesCount();

        /// <summary>
        /// retrieve the employee deails according to id
        /// </summary>
        /// <returns></returns>
        Employee GetProfile(string id);

        /// <summary>
        /// Get Skill details of an employee from table Skillrating
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        List<SkillRating> GetSkillDetails(string employeeCode);

        /// <summary>
        /// Finding skill name from table Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        string FindSkillName(int skillId);

        /// <summary>
        /// Finding skill value from table Rating
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        int FindSkillValue(int ratingId);

        /// <summary>
        /// Finding employee name from table Employee
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        string FindEmployeeName(string employeeCode);
        
        /// <summary>
        ///  Finding note of a rating value from table Rating
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        string FindRatingNote(int ratingId);
    }
}
