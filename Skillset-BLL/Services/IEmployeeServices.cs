using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_BLL.Services
{

    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDTO> GetAllEmployees();
        int AddNewEmployee(EmployeeDTO employee);
        int EditEmployeeById(EmployeeDTO employee);
        int DeleteEmployeeById(string id);
        EmployeeDTO GetEmployeeDetailsById(string id);       
        List<DesignationDTO> GetDesignations();
        List<QualificationDTO> GetQualifications();
        List<EmployeeDTO> GetManagers();
        List<RoleDTO> GetRoles();
        List<EmployeeDTO> GetRecentEmployees();
        string GetDesignationName(string id);
        string GetQualificationName(string id);
        string GetManagerName(string id);
        string GetRoleName(string id);
        IQueryable<string> GetEmployeeRatedSkill();
        IQueryable<string> GetEmployeeRating();
    }
}
