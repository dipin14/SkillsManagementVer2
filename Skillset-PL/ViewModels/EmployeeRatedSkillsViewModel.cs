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
        public int RaitedValue { get; set; }
        public DateTime RaitedDate { get; set; }
        public string RaitedNote { get; set; }
    }
}