using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Skillset_DAL.Repositories;

namespace Common.Extensions
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
        public IEnumerable<DTO.ReportingStaff> GetEmployeeDetails(string managerCode)
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
                                }).Cast<DTO.ReportingStaff>().ToList();
            return staffDetails;
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
                                      join s in skills on sr.SkillId equals s.skillId
                                      select new
                                      {
                                          Skill = s.skillName,
                                          Rating = r.Value,
                                          RatingDate = sr.RatingDate,
                                          Note = sr.Note
                                      }).Cast<StaffSkills>().ToList();
            return skillRatingDetails;
        }
    }
}
