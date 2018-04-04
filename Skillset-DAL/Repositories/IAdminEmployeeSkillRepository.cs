using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface IAdminEmployeeSkillRepository
    {
        /// <summary>
        /// Get Searched Employee details from table Employee
        /// </summary>
        /// <param name="option"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        List<Employee> GetEmployeeDetails(string searchKey, int pageNumber, int pageSize, out int totalCount);
        
        /// <summary>
        /// Finding designation from table Designation
        /// </summary>
        /// <param name="designationId"></param>
        /// <returns></returns>
        string FindDesignation(int designationId);
        
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
