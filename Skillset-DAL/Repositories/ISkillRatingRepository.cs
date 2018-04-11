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
        /// Insert skillRating into SkillRating table
        /// </summary>
        /// <param name="skillRating"></param>
        /// <returns></returns>
        int Create(IList<SkillRating> skillRating);
       
        /// <summary>
        /// Get all skillratings from skillrating table
        /// </summary>
        /// <param name="empId "></param>
        /// <returns></returns>
        IList<SkillRating> GetAllRatings(int empId);
       
        /// <summary>
        /// Get all skills
        /// </summary
        /// <returns></returns>
         IList<Skill> GetAllSkills();
      
        /// <summary>
        /// Get all Rating values
        /// </summary
        /// <returns></returns>
       IList<Rating> GetAllRatingValues();

        /// <summary>
        /// Sets status of skillrating to false
        /// </summary>
        /// <param name="SkillRatingId"></param>
        /// <returns></returns>
        int Delete(int? SkillRatingId);

        /// <summary>
        /// Retrieve skill names of skills rated by employee excluding special skill
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkillExcludeSpecial();

        /// <summary>
        /// Retrieve total skill ratings count
        /// </summary>
        /// <returns></returns>
        int GetSkillRatingsCount();

        /// <summary>
        /// Retrieve average ratings for primary skills
        /// </summary>
        /// <returns></returns>
        string GetRatingAverage();
    }
}
