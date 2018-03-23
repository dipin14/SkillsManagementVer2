using Common.DTO;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_PL.ViewModelExtensions
{
    public static class ViewModelExtension
    {
        /// <summary>
        /// Convert SkillDTO to SkillViewModel
        /// </summary>
        /// <param name="skillDTO"></param>
        /// <returns></returns>
        public static SkillViewModel ToViewModel(this SkillDTO skillDTO)
        {
            return new SkillViewModel
            {
                skillId = skillDTO.SkillId,
                SkillName = skillDTO.SkillName,
                SkillDescription = skillDTO.SkillDescription
            };
        }

        /// <summary>
        /// Convert IList of SkillDTO to IList of SkillViewModel
        /// </summary>
        /// <param name="skillDTOList"></param>
        /// <returns></returns>
        public static IList<SkillViewModel> ToViewModelList(this IList<SkillDTO> skillDTOList)
        {
            return skillDTOList.Select(skill => new SkillViewModel
            {
                skillId = skill.SkillId,
                SkillDescription = skill.SkillDescription,
                SkillName = skill.SkillName
            }).ToList(); ;
        }

        /// <summary>
        /// Convert SkillViewModel to SkillDTO
        /// </summary>
        /// <param name="skillVM"></param>
        /// <returns></returns>
        public static SkillDTO ToDTO(this SkillViewModel skillVM)
        {
            return new SkillDTO
            {
                SkillId = skillVM.skillId,
                SkillName = skillVM.SkillName,
                SkillDescription = skillVM.SkillDescription
            };
        }

        /// <summary>
        /// Convert IList of SkillViewModel to SkillDTO
        /// </summary>
        /// <param name="skillVMList"></param>
        /// <returns></returns>
        public static IList<SkillDTO> ToDTOList(this IList<SkillViewModel> skillVMList)
        {
            return skillVMList.Select(skill => new SkillDTO
            {
                SkillId = skill.skillId,
                SkillName = skill.SkillName,
                SkillDescription = skill.SkillDescription
            }).ToList(); ;
        }
    }
}
