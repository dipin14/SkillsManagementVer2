using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class AdministratorEmployeeViewModel
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }

        public AdministratorEmployeeViewModel()
        {

        }
        public AdministratorEmployeeViewModel(string employeeCode, string name, string designation)
        {
            EmployeeCode = employeeCode;
            Name = name;
            Designation = designation;
        }
    }
}