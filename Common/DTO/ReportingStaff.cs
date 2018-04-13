using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ReportingStaff
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ManagerCode { get; set; }
        public string Email { get; set; }
        public int RatedSkillsCount { get; set; }
        public double AverageRating { get; set; }
    }
}
