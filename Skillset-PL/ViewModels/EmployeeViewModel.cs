using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfJoining { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public string DesignationId { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "Qualification")]
        public string QualificationId { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name ="Manager")]
        public string EmployeeId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public double MobileNumber { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}