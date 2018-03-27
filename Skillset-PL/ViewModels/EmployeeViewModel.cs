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
        [Display(Name = "Code")]
        [RegularExpression("^E([0-9]){3,8}$", ErrorMessage = "Code must begin with 'E' followed by atleast 3 and at maximum 8 numbers")]
        public string EmployeeCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Joining Date")]
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
        [Display(Name = "Birth Date")]
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
        [RegularExpression("^([0-9]){10}$", ErrorMessage = "Mobile Number should contain only 10 digits")]
        public double MobileNumber { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}