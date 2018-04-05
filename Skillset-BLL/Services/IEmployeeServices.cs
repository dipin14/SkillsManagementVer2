using Common.DTO;
using System.Collections.Generic;


namespace Skillset_BLL.Services
{

    public interface IEmployeeServices
    {
        /// <summary>
        /// Retrieves the employee record according to the search key if no search key retreives the employee list
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        
        IEnumerable<EmployeeDTO> ViewSearchRecords(string search,int pageNumber, int pageSize, out int totalCount);
        /// <summary>
        /// Adds new employee to the Employee table
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int AddNewEmployee(EmployeeDTO employee);
        /// <summary>
        ///Updates the employee record
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int EditEmployeeById(EmployeeDTO employee);
        /// <summary>
        /// Set the status of employee to false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteEmployeeById(string id);
        /// <summary>
        /// Retrieve the details of the employee with particular id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmployeeDTO GetEmployeeDetailsById(string id);
        /// <summary>
        /// retrieves the list of designations
        /// </summary>
        /// <returns></returns>      
        List<DesignationDTO> GetDesignations();
        /// <summary>
        /// retrieves the list of qualifications
        /// </summary>
        /// <returns></returns>
        List<QualificationDTO> GetQualifications();
        /// <summary>
        /// retrieves the list of managers
        /// </summary>
        /// <returns></returns>
        List<EmployeeDTO> GetManagers();
        /// <summary>
        /// retrieves the list of roles
        /// </summary>
        /// <returns></returns>
        List<RoleDTO> GetRoles();
        List<EmployeeDTO> GetRecentEmployees();
        
        int GetEmployeesCount();
        string GetDesignationName(string id);
        string GetQualificationName(string id);
        string GetManagerName(string id);
        string GetRoleName(string id);
       
        EmployeeDTO GetProfile(string id);
    }
}
