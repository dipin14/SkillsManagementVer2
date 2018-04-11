using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_BLL.Services
{
    /// <summary>
    /// Service interface for Skill manage
    /// </summary>
    public interface ISkillService
    {
        /// <summary>
        /// Insert skill into table Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Create(SkillDTO skill);

        /// <summary>
        /// Update skill in Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Update(SkillDTO skill);

        /// <summary>
        /// Remove skill from table Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Delete(string skillName);

        /// <summary>
        /// Get Skill using unique skill name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        SkillDTO GetBySkillName(string skillName);

        /// <summary>
        /// Retrieve all skills from Skill table
        /// </summary>
        /// <returns></returns>
        IList<SkillDTO> GetAllSkills();


        /// <summary>
        /// Retrieve total skill count
        /// </summary>
        /// <returns></returns>
        int GetSkillsCount();
    }
}
