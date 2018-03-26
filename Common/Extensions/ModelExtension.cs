﻿using Common.DTO;
using Skillset_DAL.Models;
using System;
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

        /// <summary>
        /// Convert employee dto to datalayer model
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Employee EmployeeDTOtoModel(this EmployeeDTO dto)
        {
            Employee employee = new Employee();
            employee.EmployeeCode = dto.EmployeeCode;
            employee.Name = dto.Name;
            employee.DateOfBirth = dto.DateOfBirth;
            employee.DateOfJoining = dto.DateOfJoining;
            employee.DesignationId = Convert.ToInt32(dto.DesignationId);
            employee.RoleId = Convert.ToInt32(dto.RoleId);
            employee.Experience = dto.Experience;
            employee.QualificationId = Convert.ToInt32(dto.QualificationId);
            employee.Address = dto.Address;
            employee.MobileNumber = dto.MobileNumber;
            employee.Email = dto.Email;
            employee.Gender = dto.Gender;
            employee.EmployeeId = Convert.ToInt32(dto.EmployeeId);

            return employee;
        }
        /// <summary>
        /// Convert employee  datalayer model to dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EmployeeDTO EmployeeModeltoDTO(this Employee employee)
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.EmployeeCode = employee.EmployeeCode;
            dto.Name = employee.Name;
            dto.DateOfBirth = employee.DateOfBirth;
            dto.DateOfJoining = employee.DateOfJoining;
            dto.DesignationId = employee.DesignationId.ToString();
            dto.RoleId = employee.RoleId.ToString();
            dto.Experience = employee.Experience;
            dto.QualificationId = employee.QualificationId.ToString();
            dto.Address = employee.Address;
            dto.MobileNumber = employee.MobileNumber;
            dto.Email = employee.Email;
            dto.Gender = employee.Gender;
            dto.EmployeeId = employee.EmployeeId.ToString();

            return dto;
        }
        /// <summary>
        /// Convert IList of SkillRatingDTO model to SkillRatingModel
        /// </summary>
        /// <param name="skillRatingDTOList"></param>
        /// <returns></returns>
        public static IList<SkillRating> ToSkillRatingModelList(this IList<EmployeeSkillRatingDTO> skillRatingDtoList)
        {
            return skillRatingDtoList.Select(skill => new SkillRating
            {
                EmployeeId = skill.EmployeeId,
                SkillId = skill.SkillId,
                Note = skill.Note,
                RatingDate = skill.RatingDate,
                IsSpecialSkill = skill.IsSpecialSkill,
                RatingId = skill.RatingScore,
                Status = skill.Status
            }).ToList(); ;
        }
    }
}
