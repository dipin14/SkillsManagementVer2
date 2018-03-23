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
        List<Employee> GetEmployeeDetails(string option, string searchKey);
        string FindDesignation(int designationId);
        List<SkillRating> GetSkillDetails(string employeeCode);
        string FindSkillName(int skillId);
        int FindSkillValue(int ratingId);
    }
}
