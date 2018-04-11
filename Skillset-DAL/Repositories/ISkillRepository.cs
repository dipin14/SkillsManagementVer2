using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    /// <summary>
    /// Repository interface for Skill manage
    /// </summary>
    public interface ISkillRepository
    {
        /// <summary>
        /// Insert skill into Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Create(Skill skill);

        /// <summary>
        /// Update skill in Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Update(Skill skill);

        /// <summary>
        /// Sets status of skill to false using skill Id
        /// </summary>
        /// <param name="skill Id"></param>
        /// <returns></returns>
        int Delete(int? skillId);

        /// <summary>
        /// Retrieve all skills from Skill table
        /// </summary>
        /// <returns></returns>
        IList<Skill> GetAllSkills();

        /// <summary>
        /// Retrieve skill id using unique skill name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        int GetIdBySkillName(string skillName);

        /// <summary>
        /// Retrieve skill object using skill name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        Skill GetSkillBySkillName(string skillName);

        /// <summary>
        /// Retrieve total skill count
        /// </summary>
        /// <returns></returns>
        int GetSkillsCount();
    }
}
