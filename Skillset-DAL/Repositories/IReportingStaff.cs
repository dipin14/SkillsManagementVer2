using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface IReportingStaff
    {
        IEnumerable<Employee> GetEmployeeDetails(string managerCode);
        IEnumerable<SkillRating> GetSkillRatingsDetails(string employeeCode);
        IEnumerable<Designation> GetDesignationDetails(string managerCode);
        IEnumerable<Skill> GetSkillDetails();
        IEnumerable<Rating> GetRatingDetails();
    }
}
