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
        private readonly IEmployeeRepository _employeeRepository;
        /// <summary>
        /// Dependency Injection for IReportingStaffRepository
        /// </summary>
        /// <param name="reportingStaff"></param>
        public ReportingStaffService(IReportingStaffRepository reportingStaff, IEmployeeRepository employeeRepository)
        {
            this._reportingStaff = reportingStaff;
            this._employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Retrieve details of an employee along with designation,rated skills count and average skill rating.
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
                                        Designation = d == null ? "Not available" : d.Name,
                                        RatedSkillsCount=(_employeeRepository.GetSkillDetails(s.EmployeeCode).Select(x=>x.IsSpecialSkill==true).ToList()).Any() ? _employeeRepository.GetSkillDetails(s.EmployeeCode).Count()-1 : _employeeRepository.GetSkillDetails(s.EmployeeCode).Count(),
                                        AverageRating =AverageSkillRating(s.EmployeeCode)
                                    }).Distinct().ToList();
                return staffDetails.Select(employee => new Common.DTO.ReportingStaff
                {
                    EmployeeCode = employee.EmployeeCode,
                    Name = employee.Name,
                    Email = employee.Email,
                    Designation = employee.Designation,
                    RatedSkillsCount = employee.RatedSkillsCount,
                    AverageRating = employee.AverageRating

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

        /// <summary>
        /// Calculate average of skill rating of an employee excluding special skill.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>averageSkillRating</returns>
        public double AverageSkillRating(string employeeCode)
        {
            double sumOfSkillRating= default(double), averageSkillRating;
            int RatedSkillsCount = default(int);
            var employeeSkillRatingList = _employeeRepository.GetSkillDetails(employeeCode);
            if(employeeSkillRatingList.Select(x=>x.IsSpecialSkill==true).ToList().Any())
            {
                RatedSkillsCount = employeeSkillRatingList.Count()-1;
            }
            else
            {
                RatedSkillsCount = employeeSkillRatingList.Count();
            }
            if(RatedSkillsCount != 0)
            {
                foreach (var s in employeeSkillRatingList)
                {
                    if(!s.IsSpecialSkill)
                    sumOfSkillRating = sumOfSkillRating + _employeeRepository.FindSkillValue(s.RatingId);
                }
                averageSkillRating = sumOfSkillRating / RatedSkillsCount;
            }
            else
            {
                //If no rated skills then average of skill ratings is shown as 0.
                averageSkillRating = default(double);
            }
            
            return averageSkillRating;
        }
    }
}
