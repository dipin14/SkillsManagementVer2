using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Models
{
    public class Skill
    {
        public int skillId { get; set; }
        public string skillName { get; set; }
        public string skillDescription { get; set; }
        public bool status { get; set; }
    }
}
