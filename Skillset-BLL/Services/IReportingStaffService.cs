using System.Collections.Generic;
using Common.DTO;

namespace Skillset_BLL.Services
{
    public interface IReportingStaffService
    {
        /// <summary>
        /// Retrieve details of an employee along with designation.
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>IEnumerable<Common.DTO.ReportingStaff></returns>
        IEnumerable<ReportingStaff> GetEmployeeDetails(string managerCode);

        /// <summary>
        /// Returns skill ratings of an employee along with rating value corresponding to an employee code.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>IEnumerable<StaffSkills></returns>
        IEnumerable<StaffSkills> GetSkillRatingsDetails(string employeeCode);

        /// <summary>
        /// Return employee profile corresponding to an employee code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EmployeeDTO</returns>
        EmployeeDTO GetProfile(string id);

        /// <summary>
        /// Calculate average of skill rating of an employee
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>averageSkillRating</returns>
        double AverageSkillRating(string employeeCode);
    }
}
