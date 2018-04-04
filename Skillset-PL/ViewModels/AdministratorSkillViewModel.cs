using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class AdministratorSkillViewModel
    {
        public string SkillName { get; set; }
        public int SkillValue { get; set; }
        public DateTime RatingDate { get; set; }
        public string Note { get; set; }
        public string RatingNote { get; set; }

        public AdministratorSkillViewModel()
        {

        }
        public AdministratorSkillViewModel(string skillName, int skillValue, DateTime ratingDate,string note, string ratingNote)
        {
            SkillName = skillName;
            SkillValue = skillValue;
            RatingDate = ratingDate;
            RatingNote = ratingNote;
            Note = note;
        }
    }
}