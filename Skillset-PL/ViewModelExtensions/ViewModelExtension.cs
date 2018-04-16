using Common.DTO;
using Skillset_PL.ViewModels;
using System;
using System.Collections;
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
            }).ToList();
        }

        /// <summary>
        ///Convert staff details dto list of datatype IEnumerable<Common.DTO.ReportingStaff> ReportingStaff to viewmodel of datatype IEnumerable<ViewModels.ReportingStaff>.
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>IEnumerable<ViewModels.ReportingStaff></returns>
        public static IEnumerable<ViewModels.ReportingStaff> ToReportingStaffViewmodel(this IEnumerable<Common.DTO.ReportingStaff> staffList)
        {
            if (staffList != null && staffList.Any())
            {
                return staffList.Select(staff => new ViewModels.ReportingStaff
                {
                    EmployeeCode = staff.EmployeeCode,
                    Name = staff.Name,
                    Email = staff.Email,
                    Designation = staff.Designation,
                    RatedSkillsCount = staff.RatedSkillsCount,
                    AverageRating = staff.AverageRating
                }).Distinct().ToList().OrderBy(staff => staff.EmployeeCode).ThenBy(staff => staff.Name).ToList();
            }
            else
            {
                return Enumerable.Empty<ViewModels.ReportingStaff>().ToList();
            }

        }

        /// <summary>
        /// <summary>
        /// convert employee view model to dto
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static EmployeeDTO EmployeeViewModeltoDTO(this EmployeeViewModel employee)
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.EmployeeCode = employee.EmployeeCode;
            dto.Name = employee.Name;
            dto.DateOfBirth = employee.DateOfBirth;
            dto.DateOfJoining = employee.DateOfJoining;
            dto.DesignationId = employee.DesignationId;
            dto.RoleId = employee.RoleId;
            dto.Experience = employee.Experience;
            dto.QualificationId = employee.QualificationId;
            dto.Address = employee.Address;
            dto.MobileNumber = employee.MobileNumber;
            dto.Email = employee.Email;
            dto.Gender = employee.Gender;
            dto.EmployeeId = employee.EmployeeId;

            return dto;
        }
        /// <summary>
        /// convert dto model to employee view model
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>EmployeeViewModel</returns>
        public static EmployeeViewModel EmployeeDTOtoViewModel(this EmployeeDTO dto)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            employee.EmployeeCode = dto.EmployeeCode;
            employee.Name = dto.Name;
            employee.DateOfBirth = dto.DateOfBirth;
            employee.DateOfJoining = dto.DateOfJoining;
            employee.DesignationId = dto.DesignationId;
            employee.RoleId = dto.RoleId;
            employee.Experience = dto.Experience;
            employee.QualificationId = dto.QualificationId;
            employee.Address = dto.Address;
            employee.MobileNumber = dto.MobileNumber;
            employee.Email = dto.Email;
            employee.Gender = dto.Gender;
            employee.EmployeeId = dto.EmployeeId;

            return employee;
        }


        /// <summary>
        /// Convert skill ratings of an employee in StaffSkills dto to StaffSkills viewmodel.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>IEnumerable<ViewModels.StaffSkills></returns>
        public static IEnumerable<ViewModels.StaffSkills> ToSkillRatingsViewmodel(this IEnumerable<Common.DTO.StaffSkills> skillList)
        {
            if (skillList != null && skillList.Any())
            {


                return skillList.Select(skill => new ViewModels.StaffSkills
                {
                    Skill = skill.Skill,
                    Rating = skill.Rating,
                    RatingNote = skill.RatingNote,
                    RatingDate = skill.RatingDate,
                    Note = skill.Note
                }).Distinct().ToList().OrderByDescending(skillRating => skillRating.Rating).ThenByDescending(skillRating => skillRating.RatingDate).ToList();

            }

            else
            {
                return Enumerable.Empty<ViewModels.StaffSkills>().ToList();
            }
        }
        /// <summary>
        /// Convert IList of EmployeeRatingViewModel to EmployeeRatingDto
        /// </summary>
        /// <param name="SkillRatingVMList"></param>
        /// <returns>IList<EmployeeSkillRatingDTO></returns>
        public static IList<EmployeeSkillRatingDTO> ToSkillRatingDTOList(this IList<EmployeeSkillRatingViewModel> SkillRatingVMList)
        {
            return SkillRatingVMList.Select(skill => new EmployeeSkillRatingDTO
            {
                EmployeeId = skill.EmployeeId,
                SkillId = skill.SkillId,
                Note = skill.Note,
                RatingDate = skill.RatingDate,
                IsSpecialSkill = skill.IsSpecialSkill,
                RatingScore = skill.RatingScore,
                Status = skill.Status

            }).ToList();
        }
        /// <summary>
        /// Convert Ilist of RatedSkilldto to RatedSkillViewModel 
        /// </summary>
        /// <param name="SkillRatedListDto"></param>
        /// <returns>IEnumerable<ViewModels.EmployeeRatedSkillsViewModel></returns>
        public static IEnumerable<ViewModels.EmployeeRatedSkillsViewModel> ToSkillRatedViewmodel(this IEnumerable<Common.DTO.EmployeeRatedSkillsDTO> skillList)
        {
            return skillList.Select(employee => new ViewModels.EmployeeRatedSkillsViewModel
            {
                Id = employee.Id,
                SkillId = employee.SkillId,
                SkillDescription =employee.SkillDescription,
                EmployeeId = employee.EmployeeId,
                SkillName = employee.SkillName,
                RatedNote = employee.RatedNote,
                RatedValue = employee.RatedValue,
                RatedDate = employee.RatedDate
            }).ToList().OrderByDescending(s => s.RatedDate).ThenByDescending(s => s.RatedValue).ToList();
        }



    }
}
