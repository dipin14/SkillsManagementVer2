using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Models
{
    /// <summary>
    /// Model class of Skill containing SkillId, SkillName, SkillDescription and Status
    /// </summary>
    public class Skill
    {
        //Primary Key for Skill record
        public int SkillId { get; set; }

        //String name of Skill
        public string SkillName { get; set; }

        //String text of Description of Skill
        public string SkillDescription { get; set; }

        //Boolean value for Status of Skill
        public bool Status { get; set; }
    }
}
