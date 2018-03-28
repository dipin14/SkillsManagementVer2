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

        /// <summary>
        /// Get Searched Employee details from table Employee
        /// </summary>
        /// <param name="option"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IEnumerable<AdminEmployeeDTO> ViewSearchedRecords(string option, string searchKey)
        {
            //getting list of searched employees
            List<Employee> employeeList = _iEmpSkillRepository.GetEmployeeDetails(option, searchKey);
            List<string> designationList = new List<string>();
            foreach (var emp in employeeList)
            {
                //finding designation from designation id
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

        /// <summary>
        /// Get Skill details of an employee from table Skillrating
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public IEnumerable<AdminSkillDTO> GetSkillDetails(string employeeCode)
        {
            //getting list of skills of a particular employee
            List<SkillRating> skillList = _iEmpSkillRepository.GetSkillDetails(employeeCode);
            List<string> skillNameList = new List<string>();
            List<int> skillValueList = new List<int>();
            foreach (var item in skillList)
            {
                //finding skill name from skill id
                string skillName = _iEmpSkillRepository.FindSkillName(item.SkillId);
                skillNameList.Add(skillName);
                //finding skill value from rating id
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
        /// <summary>
        /// Finding employee name from table Employee
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public string FindEmployeeName(string employeeCode)
        {
            return _iEmpSkillRepository.FindEmployeeName(employeeCode);
        }
    }
}
