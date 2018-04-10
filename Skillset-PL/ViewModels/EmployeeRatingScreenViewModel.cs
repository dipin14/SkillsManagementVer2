using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skillset_PL.ViewModels;
namespace Skillset_PL.ViewModels
{
    public class EmployeeRatingScreenViewModel
    {
       public IEnumerable<EmployeeRatedSkillsViewModel> RatedSkills { get; set; }
       public  IEnumerable<SkillViewModel> SkillRatings { get; set; }
    }
}