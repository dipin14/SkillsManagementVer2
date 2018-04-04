using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class StaffSkills
    {
        public string EmployeeCode { get; set; }
        public string Skill { get; set; }
        public int Rating { get; set; }
        public DateTime RatingDate { get; set; }
        public string Note { get; set; }
        public bool IsSpecialSkill { get; set; }
        public bool Status { get; set; }
        public string RatingNote { get; set; }
    }
}
