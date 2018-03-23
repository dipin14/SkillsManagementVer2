using System.Collections.Generic;
using Common.DTO;

namespace Common.Extensions
{
    public interface IReportingStaffExtensions
    {
        IEnumerable<ReportingStaff> GetEmployeeDetails(string managerCode);
        IEnumerable<StaffSkills> GetSkillRatingsDetails(string employeeCode);
    }
}
