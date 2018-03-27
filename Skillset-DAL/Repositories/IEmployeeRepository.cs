using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        int AddEmployee(Employee employee);
        int EditEmployee(Employee employee);
        int DeleteEmployee(string id);
        Employee GetEmployeeDetails(string id);
        int CheckDuplicateEmployee(List<Employee> employeeList, Employee newEmployee);
        List<Designation> GetDesignations();
        List<Qualification> GetQualifications();
        List<Employee> GetManagers();
        List<Role> GetRole();
        List<Employee> GetRecentEmployees();
        string GetDesignationName(string id);
        string GetQualificationName(string id);
        string GetManagerName(string id);
        string GetRoleName(string id);
        IQueryable<string> GetEmployeeRatedSkill();
        IQueryable<string> GetEmployeeRating();
    }
}
