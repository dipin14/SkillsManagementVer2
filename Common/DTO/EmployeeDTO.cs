using System;

namespace Common.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string DesignationId { get; set; }
        public string RoleId { get; set; }
        public string QualificationId { get; set; }
        public int Experience { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmployeeId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public double MobileNumber { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
    }
}
