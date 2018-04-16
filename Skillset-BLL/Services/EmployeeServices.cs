using Common.DTO;
using Common.Extensions;
using Skillset_DAL.Models;
using Skillset_DAL.Repositories;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Skillset_BLL.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private IEmployeeRepository _repository;

        /// <summary>
        /// Dependency Injection for IEmployeeRepository
        /// </summary>
        /// <param name="repository"></param>
        /// <summary>
        /// Dependency injection for Employee repository
        /// </summary>
        /// <param name="repository"></param>
        public EmployeeServices(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adds new employee to the Employee table
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int AddNewEmployee(EmployeeDTO employee)
        {
            return _repository.AddEmployee(employee.EmployeeDTOtoModel());
        }

        /// <summary>
        ///Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEmployeeById(string id)
        {
            return _repository.DeleteEmployee(id);
        }

        /// <summary>
        ///Updates the employee record by id
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int EditEmployeeById(EmployeeDTO employee)
        {
            return _repository.EditEmployee(employee.EmployeeDTOtoModel());

        }

        /// <summary>
        /// Retrieve the details of the employee with particular id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeDTO GetEmployeeDetailsById(string id)
        {
            return _repository.GetEmployeeDetails(id).EmployeeModeltoDTO();
        }

        /// <summary>
        /// Retrieves the list of designations
        /// </summary>
        /// <returns></returns>     
        public List<DesignationDTO> GetDesignations()
        {
            var list = _repository.GetDesignations();
            var dto = new List<DesignationDTO>();
            foreach (Designation item in list)
            {
                dto.Add(new DesignationDTO { Id = item.Id, Name = item.Name });
            }
            return dto;
        }

        /// <summary>
        /// Retrieves the list of qualifications
        /// </summary>
        /// <returns></returns>
        public List<QualificationDTO> GetQualifications()
        {
            var list = _repository.GetQualifications();
            var dto = new List<QualificationDTO>();
            foreach (Qualification item in list)
            {
                dto.Add(new QualificationDTO { Id = item.Id, Name = item.Name });
            }
            return dto;
        }

        /// <summary>
        /// Retrieves the list of managers
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDTO> GetManagers()
        {
            var list = _repository.GetManagers();
            var dto = new List<EmployeeDTO>();
            foreach (Employee item in list)
            {
                dto.Add(new EmployeeDTO { Id = item.Id, Name = item.Name });
            }
            return dto;
        }

        /// <summary>
        /// Retrieves the list of roles
        /// </summary>
        /// <returns></returns>
        public List<RoleDTO> GetRoles()
        {
            var list = _repository.GetRole();
            var dto = new List<RoleDTO>();
            foreach (Role item in list)
            {
                dto.Add(new RoleDTO { Id = item.Id, Name = item.Name });
            }
            return dto;
        }

        /// <summary>
        /// Retrieve the name for a particular designation id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDesignationName(string id)
        {
            return _repository.GetDesignationName(id);
        }

        /// <summary>
        /// Retrieve the name for a particular qualification id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetQualificationName(string id)
        {
            return _repository.GetQualificationName(id);
        }

        /// <summary>
        /// Retrieve the name for a particular Manager id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetManagerName(string id)
        {
            return _repository.GetManagerName(id);
        }

        /// <summary>
        /// Retrieve the name for a particular role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetRoleName(string id)
        {
            return _repository.GetRoleName(id);
        }

        /// <summary>
        /// Retrieves the employee record according to the search key if no search key retreives the employee list
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>  
        public IEnumerable<EmployeeDTO> ViewSearchRecords(string search, int pageNumber, int pageSize, out int totalCount)
        {
            
            var list = _repository.GetSearchRecords(search,pageNumber,pageSize, out  totalCount);
            var dtoList = new List<EmployeeDTO>();
            foreach (Employee item in list)
            {
                dtoList.Add(item.EmployeeModeltoDTO());
            }
            return dtoList;
        }

        /// <summary>

        /// Retrieve total employees count
        /// </summary>
        /// <returns></returns>
        public int GetEmployeesCount()
        {
            return _repository.GetEmployeesCount();
        }
      
        /// <summary>
        /// Get employee profile details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeDTO GetProfile(string id)
        {
            return _repository.GetProfile(id).EmployeeModeltoDTO();
        }



        /// <summary>
        /// Get Skill details of an employee from table Skillrating
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public IEnumerable<AdminSkillDTO> GetSkillDetails(string employeeCode)
        {
            //getting list of skills of a particular employee
            List<SkillRating> skillList = _repository.GetSkillDetails(employeeCode);
            List<string> skillNameList = new List<string>();
            List<int> skillValueList = new List<int>();
            List<string> RatingNoteList = new List<string>();
            foreach (var item in skillList)
            {
                //finding skill name from skill id
                string skillName = _repository.FindSkillName(item.SkillId);
                skillNameList.Add(skillName);
                //finding skill value from rating id
                int skillValue = _repository.FindSkillValue(item.RatingId);
                skillValueList.Add(skillValue);
                string ratingNote = _repository.FindRatingNote(item.RatingId);
                if (!string.IsNullOrWhiteSpace(ratingNote))
                {
                    RatingNoteList.Add(ratingNote);
                }
                else
                {
                    RatingNoteList.Add("No Description");
                }

            }
            List<AdminSkillDTO> skillDetailsList = new List<AdminSkillDTO>();
            for (int loopVar = 0; loopVar < skillList.Count; loopVar++)
            {
                AdminSkillDTO skill = new AdminSkillDTO();
                skill.SkillName = skillNameList[loopVar];
                skill.SkillValue = skillValueList[loopVar];
                skill.RatingNote = RatingNoteList[loopVar];
                skill.RatingDate = skillList[loopVar].RatingDate;
                skill.Note = skillList[loopVar].Note == null || skillList[loopVar].Note == string.Empty ? "Not available" : skillList[loopVar].Note;
                skillDetailsList.Add(skill);
            }
            return skillDetailsList;
        }

        /// <summary>
        /// Finding employee name from table Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetEmployeeName(string id)
        {
            return _repository.FindEmployeeName(id);
        }

        /// <summary>
        /// Retrieve employee name and skill name of those employee who top rated that skill
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetTopRatedRecentEmployees(string search)
        {
            var topEmployees = _repository.GetTopRatedRecentEmployees();
            if(!string.IsNullOrWhiteSpace(search))
            {
                var searchResult=topEmployees.FindAll(s => s.Key.ToLower().Contains(search.ToLower()) || s.Value.ToLower().Contains(search.ToLower()));
                topEmployees = searchResult;
            }
           
            
            return topEmployees;

        }

       
    }
}