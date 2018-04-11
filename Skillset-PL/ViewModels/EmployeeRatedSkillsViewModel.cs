using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModels
{
    public class EmployeeRatedSkillsViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string SkillName { get; set; }
        public int RatedValue { get; set; }
        public DateTime RatedDate { get; set; }
        public string RatedNote { get; set; }
    }
}