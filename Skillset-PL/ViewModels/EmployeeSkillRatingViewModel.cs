using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class EmployeeSkillRatingViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public int RatingScore { get; set; }
        public DateTime RatingDate { get; set; }
        public string Note { get; set; }
        [DefaultValue(true)]
        public bool IsSpecialSkill { get; set; }
        public bool Status { get; set; }
    }
}