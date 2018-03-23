using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skillset_PL.ViewModelExtensions
{
    public class ManagerExtension
    {
        private readonly Common.Extensions.IReportingStaffExtensions _reportingStaff;

        public ManagerExtension()
        {
        }

        public ManagerExtension(Common.Extensions.IReportingStaffExtensions reportingStaff)
        {
            this._reportingStaff = reportingStaff;
        }
        /// <summary>
        /// Staff details dto to viewmodel.
        /// </summary>
        /// <param name="managerCode"></param>
        /// <returns></returns>
        public IEnumerable<ViewModels.ReportingStaff> GetEmployeeDetails(string managerCode)
        {
            
            var staff = _reportingStaff.GetEmployeeDetails(managerCode);
            var staffDetails = (from s in staff
                                select new
                                {
                                    EmployeeCode = s.EmployeeCode,
                                    Name = s.Name,
                                    Email = s.Email,
                                    Designation = s.Designation
                                }).Cast<ViewModels.ReportingStaff>().ToList();
            return staffDetails;
        }
        /// <summary>
        /// skill ratings of an employee dto to viewmodel.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public IEnumerable<ViewModels.StaffSkills> GetSkillRatingsDetails(string employeeCode)
        {
            
            var skillRatings = _reportingStaff.GetSkillRatingsDetails(employeeCode);
            var skillRatingDetails = (from sr in skillRatings
                                      select new
                                      {
                                          Skill = sr.Skill,
                                          Rating = sr.Rating,
                                          RatingDate = sr.RatingDate,
                                          Note = sr.Note
                                      }).Cast<ViewModels.StaffSkills>().ToList();
            return skillRatingDetails;
        }
    }
}