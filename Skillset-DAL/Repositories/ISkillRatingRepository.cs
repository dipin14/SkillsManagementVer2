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
        /// <summary>
        /// Get all skillratings from skillrating table
        /// </summary>
        /// <param name="employee id"></param>
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
        /// <param name="skillrating Id"></param>
        /// <returns></returns>
        int Delete(int? SkillRatingId);

        /// <summary>
        /// Retrieve skill names of skills rated by employee
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkillName();

        /// <summary>
        /// Retrieve skill names of skills rated by employee excluding special skill
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkillExcludeSpecial();

        /// <summary>
        /// Retrieve total ratings for each skill given by employees
        /// </summary>
        /// <returns></returns>
        string GetEmployeeRating();

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
