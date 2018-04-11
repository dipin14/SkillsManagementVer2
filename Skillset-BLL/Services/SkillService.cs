using System;
using System.Collections.Generic;
using System.Linq;
using Common.DTO;
using Skillset_DAL.Repositories;
using Common.Extensions;

namespace Skillset_BLL.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _iSkillRepository;

        /// <summary>
        /// Dependency Injection for Skill Repository
        /// </summary>
        /// <param name="skillRepository"></param>
        public SkillService(ISkillRepository skillRepository)
        {
            _iSkillRepository = skillRepository;
        }

        /// <summary>
        /// Insert skill into table Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public int Create(SkillDTO skill)
        {
            return _iSkillRepository.Create(skill.ToModel());
        }

        /// <summary>
        /// Remove skill from table Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public int Delete(string skillName)
        {
            //Retrieves skill id from unique skill name
            var skillId = _iSkillRepository.GetIdBySkillName(skillName);

            return _iSkillRepository.Delete(skillId);
        }

        /// <summary>
        /// Retrieve all skills from Skill table
        /// </summary>
        /// <returns></returns>
        public IList<SkillDTO> GetAllSkills()
        {
            return _iSkillRepository.GetAllSkills().ToDtoList();
        }

        /// <summary>
        /// Get Skill using unique skill name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        public SkillDTO GetBySkillName(string skillName)
        {
            return _iSkillRepository.GetSkillBySkillName(skillName).ToDTO();

        }

        /// <summary>
        /// Update skill in Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public int Update(SkillDTO skill)
        {
            return _iSkillRepository.Update(skill.ToModel());
        }

        /// <summary>
        /// Retrieve total skill count
        /// </summary>
        /// <returns></returns>
        public int GetSkillsCount()
        {
            return _iSkillRepository.GetSkillsCount();
        }
    }
}
