using Common.DTO;
using Common.Extensions;
using Skillset_DAL.Models;
using Skillset_DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Skillset_BLL.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private IEmployeeRepository _repository;

        public EmployeeServices(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public int AddNewEmployee(EmployeeDTO employee)
        {
            return _repository.AddEmployee(employee.EmployeeDTOtoModel());
        }

        public int DeleteEmployeeById(string id)
        {
            return _repository.DeleteEmployee(id);
        }

        public int EditEmployeeById(EmployeeDTO employee)
        {
            return _repository.EditEmployee(employee.EmployeeDTOtoModel());

        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var list = _repository.GetAllEmployees();
            var dtoList = new List<EmployeeDTO>();
            foreach(Employee item in list)
            {
                dtoList.Add(item.EmployeeModeltoDTO());
            }
            return dtoList;          
        }

        public EmployeeDTO GetEmployeeDetailsById(string id)
        {
            return _repository.GetEmployeeDetails(id).EmployeeModeltoDTO();
        }

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

        public string GetDesignationName(string id)
        {
            return _repository.GetDesignationName(id);
        }

        public string GetQualificationName(string id)
        {
            return _repository.GetQualificationName(id);
        }

        public string GetManagerName(string id)
        {
            return _repository.GetManagerName(id);
        }

        public string GetRoleName(string id)
        {
            return _repository.GetRoleName(id);
        }

        public IEnumerable<EmployeeDTO> ViewSearchRecords(string option, string search)
        {
            var list = _repository.GetSearchRecords(option,search);
            var dtoList = new List<EmployeeDTO>();
            foreach (Employee item in list)
            {
                dtoList.Add(item.EmployeeModeltoDTO());
            }
            return dtoList;
        }
        public List<EmployeeDTO> GetRecentEmployees()
        {
            return _repository.GetRecentEmployees().ListEmployeeModeltoDTO();
        }

        public IQueryable<string> GetEmployeeRatedSkill()
        {
            return _repository.GetEmployeeRatedSkill();
        }

        public string GetEmployeeRating()
        {
            return _repository.GetEmployeeRating();
        }


    }
}