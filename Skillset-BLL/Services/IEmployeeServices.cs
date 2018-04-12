using Common.DTO;
using System.Collections;
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
        ///Updates the employee record by id
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int EditEmployeeById(EmployeeDTO employee);
        
        /// <summary>
        ///Delete employee by id
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
        /// Retrieves the list of designations
        /// </summary>
        /// <returns></returns>      
        List<DesignationDTO> GetDesignations();

        /// <summary>
        /// Retrieves the list of qualifications
        /// </summary>
        /// <returns></returns>
        List<QualificationDTO> GetQualifications();

        /// <summary>
        /// Retrieves the list of managers
        /// </summary>
        /// <returns></returns>
        List<EmployeeDTO> GetManagers();

        /// <summary>
        /// Retrieves the list of roles
        /// </summary>
        /// <returns></returns>
        List<RoleDTO> GetRoles();

        /// <summary>
        /// Retrieve total employees count
        /// </summary>
        /// <returns></returns>
        int GetEmployeesCount();

        /// <summary>
        /// Retrieve the name for a particular designation id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetDesignationName(string id);

        /// <summary>
        /// Retrieve the name for a particular qualification id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetQualificationName(string id);

        /// <summary>
        /// Retrieve the name for a particular Manager id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetManagerName(string id);

        /// <summary>
        /// Retrieve the name for a particular role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetRoleName(string id);
       
        EmployeeDTO GetProfile(string id);

        /// <summary>
        /// Get Skill details of an employee from table Skillrating
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        IEnumerable<AdminSkillDTO> GetSkillDetails(string employeeCode);

        /// <summary>
        /// Finding employee name from table Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetEmployeeName(string id);

        /// <summary>
        /// Retrieve employee name and skill name of those employee who top rated that skill
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<string, string>> GetTopRatedRecentEmployees(string search);


    }
}
