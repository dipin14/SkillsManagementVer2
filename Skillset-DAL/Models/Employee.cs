using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int DesignationId { get; set; }
        public virtual Designation Designations { get; set; }
        public int RoleId { get; set; }

        public virtual Role Roles { get; set; }
        public int QualificationId { get; set; }
        public virtual Qualification Qualifications { get; set; }
        public int Experience { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Manager { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public double MobileNumber { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }

    }
}
