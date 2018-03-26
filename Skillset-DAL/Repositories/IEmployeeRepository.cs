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
        Employee EditEmployee(int id);
        int DeleteEmployee(int id);
        Employee GetEmployeeDetails(int id);
        int CheckDuplicateEmployee(List<Employee> employeeList, Employee newEmployee);
        List<Designation> GetDesignations();
        List<Qualification> GetQualifications();
        List<Employee> GetManagers();
        List<Role> GetRole();
        List<Employee> GetRecentEmployees();
    }
}
