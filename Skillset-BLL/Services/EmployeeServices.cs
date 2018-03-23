using Common.DTO;
using Common.Extensions;
using Skillset_DAL.Models;
using Skillset_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int DeleteEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO EditEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO GetEmployeeDetailsById(int id)
        {
            throw new NotImplementedException();
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
    }
}