using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Models
{
    public class SkillRating
    {
        public int Id { get; set; }
<<<<<<< HEAD
=======
        public int EmployeeId { get; set; }
>>>>>>> c0546a2e8b80f71dcc01bfac16fe36b1d8116f3d
        public int SkillId { get; set; }
        public int RatingId { get; set; }
        public DateTime RatingDate { get; set; }
        public string Note { get; set; }
        public bool IsSpecialSkill { get; set; }
        public bool Status { get; set; }
<<<<<<< HEAD
        public int EmployeeId { get; set; }
=======
>>>>>>> c0546a2e8b80f71dcc01bfac16fe36b1d8116f3d
    }
}
