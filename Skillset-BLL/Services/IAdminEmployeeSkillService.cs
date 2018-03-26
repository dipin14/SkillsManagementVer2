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
        IEnumerable<AdminEmployeeDTO> ViewSearchedRecords(string option, string searchKey);
        IEnumerable<AdminSkillDTO> GetSkillDetails(string employeeCode);
    }
}
