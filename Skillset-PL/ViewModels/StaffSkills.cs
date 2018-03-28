using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_PL.ViewModels
{
    public class StaffSkills
    {
        public string EmployeeCode { get; set; }
        public string Skill { get; set; }
        public int Rating { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Rated Date")]
        public DateTime RatingDate { get; set; }
        public string Note { get; set; }
        public bool IsSpecialSkill { get; set; }
        public bool Status { get; set; }
    }
}
