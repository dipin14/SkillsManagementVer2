using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillset_DAL.Models;
namespace Skillset_DAL.Repositories
{
    public interface ISkillRatingRepository
    {
        /// <summary>
        /// Insert skill into Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Create(IList<SkillRating> skillRating);

    }
}
