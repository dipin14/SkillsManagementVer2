using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class EmployeeViewModel
    {

        [Required(ErrorMessage ="Please enter Employee code")]
        [Display(Name = "Employee Code")]
        [RegularExpression("^E([0-9]){3,8}$", ErrorMessage = "Code must begin with 'E' followed by atleast 3 numbers")]
        public string EmployeeCode { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [RegularExpression("([a-zA-Z.&'-]+)", ErrorMessage = "Please enter a valid Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Joining Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Joining Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfJoining { get; set; }
        [Required(ErrorMessage = "Please select a Designation")]
        [Display(Name = "Designation")]
        public string DesignationId { get; set; }
        [Required(ErrorMessage = "Please select a Role")]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Please select a Qualification")]
        [Display(Name = "Qualification")]
        public string QualificationId { get; set; }
        [Required(ErrorMessage = "Please enter experience")]
        [Display(Name = "Experience(years)")]
        [Range(0,50,ErrorMessage ="Experience should be between 0 and 50")]
        public int Experience { get; set; }
        [Required(ErrorMessage = "Please enter Birth Date")]
        [DataType(DataType.Date)]      
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please select a Manager")]
        [Display(Name = "Manager")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Mobile Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Mobile Number should contain 10 digits")]
        [Display(Name = "Mobile Number")]
        public double MobileNumber { get; set; }
        [Required(ErrorMessage = "Please choose a Gender")]
        public string Gender { get; set; }
    }
}