using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_PL.ViewModels
{
    public class ReportingStaff
    {
        public string EmployeeCode { get; set; }
        public string Designation { get; set; }

       
        //public DateTime DateOfJoining { get; set; }
 [DataType(DataType.EmailAddress)]
    }
}
