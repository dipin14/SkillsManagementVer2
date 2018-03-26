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
                skillId = skillDTO.SkillId,
                skillName = skillDTO.SkillName,
                skillDescription = skillDTO.SkillDescription
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
                SkillDescription = skill.skillDescription,
                SkillId = skill.skillId,
                SkillName = skill.skillName
            }).ToList(); ;
        }

        public static SkillDTO ToDTO(this Skill skill)
        {
            return new SkillDTO
            {
                SkillId = skill.skillId,
                SkillName = skill.skillName,
                SkillDescription = skill.skillDescription
            };
        }

        public static List<EmployeeDTO> ListEmployeeModeltoDTO(this List<Employee> employees)
        {
            var dto = new List<EmployeeDTO>();
            foreach (Employee employee in employees)
            {
                dto.Add(new EmployeeDTO
                {
                    EmployeeCode = employee.EmployeeCode,
                    Name = employee.Name,
                    DateOfBirth = employee.DateOfBirth,
                    DateOfJoining = employee.DateOfJoining,
                    DesignationId = employee.DesignationId.ToString(),
                    RoleId = employee.RoleId.ToString(),
                    Experience = employee.Experience,
                    QualificationId = employee.QualificationId.ToString(),
                    Address = employee.Address,
                    MobileNumber = employee.MobileNumber,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    EmployeeId = employee.EmployeeId.ToString()
                }
            );
            }
            return dto;
        }

    }
}
