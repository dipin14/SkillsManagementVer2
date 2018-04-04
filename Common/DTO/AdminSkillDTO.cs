using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class AdminSkillDTO
    {
        public string SkillName { get; set; }
        public int SkillValue { get; set; }
        public DateTime RatingDate { get; set; }
        public string Note { get; set; }
        public string RatingNote { get; set; }
    }
}
