using System.Collections.Generic;
using Common.DTO;

namespace Skillset_BLL.Services
{
    public interface IReportingStaffExtensions
    {
        IEnumerable<ReportingStaff> GetEmployeeDetails(string managerCode);
        IEnumerable<StaffSkills> GetSkillRatingsDetails(string employeeCode);
        EmployeeDTO GetProfile(int id);
    }
}
