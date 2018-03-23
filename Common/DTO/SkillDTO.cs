using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class SkillDTO
    {
        int skillId;
        string skillName;
        string skillDescription;

        public int SkillId
        {
            get
            {
                return skillId;
            }

            set
            {
                skillId = value;
            }
        }

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
