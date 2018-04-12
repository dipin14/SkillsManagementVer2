using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface IReportingStaffRepository
    { 
        /// <summary>
        /// Retrieve List of employees under a manager
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of Employees</returns>
        IEnumerable<Employee> GetEmployeeDetails(string managerCode);

        /// <summary>
        /// Retrieve List of skill ratings of an employee
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of SkillRatings</returns>
        IEnumerable<SkillRating> GetSkillRatingsDetails(string employeeCode);

        /// <summary>
        /// Retrieve designations of all employees under a manager
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>List of Designations</returns>
        IEnumerable<Designation> GetDesignationDetails(string managerCode);

        /// <summary>
        /// Retrieve all skills
        /// </summary>
        /// <returns>List of Skills</returns>
        IEnumerable<Skill> GetSkillDetails();

        /// <summary>
        /// Retrieve all details regarding rating values.
        /// </summary>
        /// <returns>List of Ratings</returns>
        IEnumerable<Rating> GetRatingDetails();

        /// <summary>
        /// Retrieve details of an employee corresponding to an Employee Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee</returns>
        Employee GetProfile(string id);

        /// <summary>
        /// Get employee id corresponding to an employee code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>int</returns>
        int GetEmployeeId(string employeeCode);
    }
}
