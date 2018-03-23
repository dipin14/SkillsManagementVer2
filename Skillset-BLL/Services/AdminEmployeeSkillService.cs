using Common.DTO;
using Skillset_DAL.Models;
using Skillset_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_BLL.Services
{
    public class AdminEmployeeSkillService : IAdminEmployeeSkillService
    {
        private readonly IAdminEmployeeSkillRepository _iEmpSkillRepository;

        //DI to UserRepository
        public AdminEmployeeSkillService(IAdminEmployeeSkillRepository empSkillRepository)
        {
            _iEmpSkillRepository = empSkillRepository;
        }

        public IEnumerable<AdminEmployeeDTO> ViewSearchedRecords(string option, string searchKey)
        {
            List<Employee> employeeList = _iEmpSkillRepository.GetEmployeeDetails(option, searchKey);
            List<string> designationList = new List<string>();
            foreach (var emp in employeeList)
            {
                string designation = _iEmpSkillRepository.FindDesignation(emp.DesignationId);
                designationList.Add(designation);
            }
            List<AdminEmployeeDTO> employeeDetailsList = new List<AdminEmployeeDTO>();
            for (int loopVar = 0; loopVar < employeeList.Count; loopVar++)
            {
                AdminEmployeeDTO emp = new AdminEmployeeDTO();
                emp.EmployeeCode = employeeList[loopVar].EmployeeCode;
                emp.Name = employeeList[loopVar].Name;
                emp.Designation = designationList[loopVar];
                employeeDetailsList.Add(emp);
            }
            return employeeDetailsList;
        }

        public IEnumerable<AdminSkillDTO> GetSkillDetails(string employeeCode)
        {
            List<SkillRating> skillList = _iEmpSkillRepository.GetSkillDetails(employeeCode);
            List<string> skillNameList = new List<string>();
            List<int> skillValueList = new List<int>();
            foreach (var item in skillList)
            {
                string skillName = _iEmpSkillRepository.FindSkillName(item.SkillId);
                skillNameList.Add(skillName);
                int skillValue = _iEmpSkillRepository.FindSkillValue(item.RatingId);
                skillValueList.Add(skillValue);
            }
            List<AdminSkillDTO> skillDetailsList = new List<AdminSkillDTO>();
            for (int loopVar = 0; loopVar < skillList.Count; loopVar++)
            {
                AdminSkillDTO skill = new AdminSkillDTO();
                skill.SkillName = skillNameList[loopVar];
                skill.SkillValue = skillValueList[loopVar];
                skill.RatingDate = skillList[loopVar].RatingDate;
                skillDetailsList.Add(skill);
            }
            return skillDetailsList;
        }
    }
}
