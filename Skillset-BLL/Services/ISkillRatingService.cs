using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
namespace Skillset_BLL.Services
{
   public interface ISkillRatingService
    {
        /// <summary>
        /// Insert skillRating into table SkillRating
        /// </summary>
        /// <param name="skillRating"></param>
        /// <returns></returns>
        int Create(IList< EmployeeSkillRatingDTO> skillRating);
        /// <summary>
        /// Get all rated skills of employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        IList<EmployeeRatedSkillsDTO> GetRatedSkills(int empId);
        /// <summary>
        /// Remove skillrating from table Skillrating
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Delete(int SkillRatingId);

        IQueryable<string> GetEmployeeRatedSkill();
        IQueryable<string> GetEmployeeRatedSkillExcludeSpecial();
        string GetEmployeeRating();
        string GetRatingAverage();
        int GetSkillRatingsCount();
    }
}
