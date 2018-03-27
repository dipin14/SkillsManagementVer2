using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_BLL.Services
{
    public interface IAdminEmployeeSkillService
    {
        /// <summary>
        /// Get Employee details from table Employee
        /// </summary>
        /// <param name="option"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IEnumerable<AdminEmployeeDTO> ViewSearchedRecords(string option, string searchKey);
        /// <summary>
        /// Get Skill details of an employee from table Skillrating
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        IEnumerable<AdminSkillDTO> GetSkillDetails(string employeeCode);
        /// <summary>
        /// Finding employee name from table Employee
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        string FindEmployeeName(string employeeCode);
    }
}
