using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class SkillViewModel
    {
        public int skillId { get; set; }
        string skillName;
        string skillDescription;
        
        [Required]
        [Display(Name = "Skill Name")]
        public string SkillName
        {
            get
            {
                return skillName;
            }

            set
            {
                skillName = value;
            }
        }

        [Required]
        [Display(Name = "Skill Description")]
        public string SkillDescription
        {
            get
            {
                return skillDescription;
            }

            set
            {
                skillDescription = value;
            }
        }
    }
}