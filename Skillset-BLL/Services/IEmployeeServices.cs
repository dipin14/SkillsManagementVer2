using Common.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Skillset_BLL.Services
{

    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDTO> GetAllEmployees();
        IEnumerable<EmployeeDTO> ViewSearchRecords(string search);
        int AddNewEmployee(EmployeeDTO employee);
        int EditEmployeeById(EmployeeDTO employee);
        int DeleteEmployeeById(string id);
        EmployeeDTO GetEmployeeDetailsById(string id);       
        List<DesignationDTO> GetDesignations();
        List<QualificationDTO> GetQualifications();
        List<EmployeeDTO> GetManagers();
        List<RoleDTO> GetRoles();
        List<EmployeeDTO> GetRecentEmployees();
        int GetSkillsCount();
        int GetEmployeesCount();
        int GetSkillRatingsCount();
        string GetDesignationName(string id);
        string GetQualificationName(string id);
        string GetManagerName(string id);
        string GetRoleName(string id);
        IQueryable<string> GetEmployeeRatedSkill();
        IQueryable<string> GetEmployeeRatedSkillExcludeSpecial();
        string GetEmployeeRating();
        string GetRatingAverage();
        void Dispose();
        EmployeeDTO GetProfile(string id);
    }
}
