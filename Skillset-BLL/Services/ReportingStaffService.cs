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
    public class ReportingStaffService : IReportingStaffService
    {

        private readonly IReportingStaffRepository _reportingStaff;
        /// <summary>
        /// Dependency Injection for IReportingStaffRepository
        /// </summary>
        /// <param name="reportingStaff"></param>
        public ReportingStaffService(IReportingStaffRepository reportingStaff)
        {
            this._reportingStaff = reportingStaff;
        }

        /// <summary>
        /// Retrieve details of an employee along with designation.
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns>IEnumerable<Common.DTO.ReportingStaff></returns>
        public IEnumerable<Common.DTO.ReportingStaff> GetEmployeeDetails(string managerCode)
        {
            var designations = _reportingStaff.GetDesignationDetails(managerCode);
            var staff = _reportingStaff.GetEmployeeDetails(managerCode);
            if (staff.Any())
            {
                var staffDetails = (from s in staff
                                    join d in designations on s.DesignationId equals d.Id
                                    select new
                                    {
                                        EmployeeCode = s.EmployeeCode == null || s.EmployeeCode == string.Empty ? "Not available" : s.EmployeeCode,
                                        Name = s.Name == null || s.Name == string.Empty ? "Not available" : s.Name,
                                        Email = s.Email == null || s.Email == string.Empty ? "Not available" : s.Email,
                                        Designation = d == null ? "Not available" : d.Name
                                    }).Distinct().ToList();
                return staffDetails.Select(employee => new Common.DTO.ReportingStaff
                {
                    EmployeeCode = employee.EmployeeCode,
                    Name = employee.Name,
                    Email = employee.Email,
                    Designation = employee.Designation

                }).ToList();

            }
            else
                return Enumerable.Empty<Common.DTO.ReportingStaff>().ToList();

        }

        /// <summary>
        /// Returns skill ratings of an employee along with rating value corresponding to an employee code.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>IEnumerable<StaffSkills></returns>
        public IEnumerable<StaffSkills> GetSkillRatingsDetails(string employeeCode)
        {
            var ratings = _reportingStaff.GetRatingDetails();
            var skills = _reportingStaff.GetSkillDetails();
            var skillRatings = _reportingStaff.GetSkillRatingsDetails(employeeCode);
            if (skillRatings.Any())
            {
                var skillRatingDetails = (from sr in skillRatings
                                          join r in ratings on sr.RatingId equals r.Id
                                          join s in skills on sr.SkillId equals s.SkillId
                                          select new StaffSkills
                                          {
                                              Skill = s.SkillName == null || s.SkillName == string.Empty ? "Not available" : s.SkillName,
                                              Rating = r == null ? default(int) : r.Value,
                                              RatingNote = r.Note == null||r.Note.Trim()==string.Empty? "No Description":r.Note,
                                              RatingDate = sr.RatingDate,
                                              Note = sr.Note == null || sr.Note == string.Empty ? "Not available" : sr.Note
                                          }).Distinct().ToList();
                return skillRatingDetails;
            }
            else
                return Enumerable.Empty<StaffSkills>().ToList();
        }

        /// <summary>
        /// Return employee profile corresponding to an employee code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EmployeeDTO</returns>
        public EmployeeDTO GetProfile(string id)
        {
            return _reportingStaff.GetProfile(id).EmployeeModeltoDTO();
        }
    }
}
