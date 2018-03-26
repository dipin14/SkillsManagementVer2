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
            }).ToList(); 
        }

        /// <summary>
        /// Staff details dto to viewmodel.
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModels.ReportingStaff> ToReportingStaffViewmodel(this IEnumerable<Common.DTO.ReportingStaff> staffList)
        {

          return  staffList.Select(staff =>new ViewModels.ReportingStaff
          {
                                    EmployeeCode = staff.EmployeeCode,
                                    Name = staff.Name,
                                    Email = staff.Email,
                                    Designation = staff.Designation
                                }).ToList();
            
        }

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
        /// <returns></returns>
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
        /// skill ratings of an employee dto to viewmodel.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModels.StaffSkills> ToSkillRatingsViewmodel(this IEnumerable<Common.DTO.StaffSkills> skillList)
        {
         return skillList.Select(skill=>new ViewModels.StaffSkills
         {
                                          Skill = skill.Skill,
                                          Rating = skill.Rating,
                                          RatingDate = skill.RatingDate,
                                          Note = skill.Note
                                      }).ToList();
        }

    }
}
