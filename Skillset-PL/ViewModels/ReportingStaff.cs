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
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ManagerCode { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int RatedSkillsCount { get; set; }
        public double AverageRating { get; set; }
    }
}
