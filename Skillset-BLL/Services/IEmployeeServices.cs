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
        EmployeeDTO EditEmployeeById(int id);
        int DeleteEmployeeById(int id);
        EmployeeDTO GetEmployeeDetailsById(int id);       
        List<DesignationDTO> GetDesignations();
        List<QualificationDTO> GetQualifications();
        List<EmployeeDTO> GetManagers();
        List<RoleDTO> GetRoles();
        List<EmployeeDTO> GetRecentEmployees();
        
    }
}
