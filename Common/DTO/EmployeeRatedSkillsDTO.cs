using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
   public class EmployeeRatedSkillsDTO
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int EmployeeId { get; set; }

        public string SkillDescription { get; set; }
        public string SkillName { get; set; }
        public int RatedValue { get; set; }
        public DateTime RatedDate { get; set; }
        public string RatedNote { get; set; }
    }
}
