﻿using Common.DTO;
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
        IEnumerable<EmployeeDTO> ViewSearchRecords(string option, string search);
        int AddNewEmployee(EmployeeDTO employee);
        int EditEmployeeById(EmployeeDTO employee);
        int DeleteEmployeeById(string id);
        EmployeeDTO GetEmployeeDetailsById(string id);       
        List<DesignationDTO> GetDesignations();
        List<QualificationDTO> GetQualifications();
        List<EmployeeDTO> GetManagers();
        List<RoleDTO> GetRoles();
        string GetDesignationName(string id);
        string GetQualificationName(string id);
        string GetManagerName(string id);
        string GetRoleName(string id);
    }
}
