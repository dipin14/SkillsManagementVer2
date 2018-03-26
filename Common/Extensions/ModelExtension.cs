using Common.DTO;
using Skillset_DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class ModelExtension
    {
        /// <summary>
        /// Convert SkillDTO to Skill model
        /// </summary>
        /// <param name="skillDTO"></param>
        /// <returns></returns>
        public static Skill ToModel(this SkillDTO skillDTO)
        {
            return new Skill
            {
                SkillId = skillDTO.SkillId,
                SkillName = skillDTO.SkillName,
                SkillDescription = skillDTO.SkillDescription
            };
        }

        /// <summary>
        /// Convert IList of Skill model to SkillDTO
        /// </summary>
        /// <param name="skillList"></param>
        /// <returns></returns>
        public static IList<SkillDTO> ToDtoList(this IList<Skill> skillList)
        {
            return skillList.Select(skill => new SkillDTO
            {
                SkillDescription = skill.SkillDescription,
                SkillId = skill.SkillId,
                SkillName = skill.SkillName
            }).ToList(); ;
        }

        public static SkillDTO ToDTO(this Skill skill)
        {
            return new SkillDTO
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName,
                SkillDescription = skill.SkillDescription
            };
        }
    }
}
