using System;
using System.Collections.Generic;
using Common.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Skillset_DAL.Repositories;
using Skillset_DAL.Models;

namespace Skillset_BLL.Services
{
    public class ReportingStaffExtensions : IReportingStaffExtensions
    {

        private readonly IReportingStaff _reportingStaff;
        public ReportingStaffExtensions(IReportingStaff reportingStaff)
        {
            this._reportingStaff = reportingStaff;
        }

        /// <summary>
        /// Staff details combined.
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns></returns>
        public IEnumerable<Common.DTO.ReportingStaff> GetEmployeeDetails(string managerCode)
        {
            var designations = _reportingStaff.GetDesignationDetails(managerCode);
            var staff = _reportingStaff.GetEmployeeDetails(managerCode);
            var staffDetails = (from s in staff
                                join d in designations on s.DesignationId equals d.Id
                                select new
                                {
                                    EmployeeCode = s.EmployeeCode,
                                    Name = s.Name,
                                    Email = s.Email,
                                    Designation = d.Name
                                }).ToList();
            return staffDetails.Select(employee => new Common.DTO.ReportingStaff
            {
                EmployeeCode=employee.EmployeeCode,
                Name=employee.Name,
                Email=employee.Email,
                Designation=employee.Designation

            }).ToList();
            
        }
        /// <summary>
        /// skill ratings of an employee combined.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public IEnumerable<StaffSkills> GetSkillRatingsDetails(string employeeCode)
        {
            var ratings = _reportingStaff.GetRatingDetails();
            var skills = _reportingStaff.GetSkillDetails();
            var skillRatings = _reportingStaff.GetSkillRatingsDetails(employeeCode);
            var skillRatingDetails = (from sr in skillRatings
                                      join r in ratings on sr.RatingId equals r.Id
                                      join s in skills on sr.SkillId equals s.SkillId
                                      select new StaffSkills
                                      {
                                          Skill = s.SkillName,
                                          Rating = r.Value,
                                          RatingDate = sr.RatingDate,
                                          Note = sr.Note
                                      }).ToList();
            return skillRatingDetails;
        }

        public EmployeeDTO GetProfile(int id)
        {
            return _reportingStaff.GetProfile(id).EmployeeModeltoDTO();
        }
    }
}
