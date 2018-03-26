﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Skillset_DAL.Repositories;

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
           
            if (staff.Any())
            {
                var staffDetails = (from s in staff
                                    join d in designations on s.DesignationId equals d.Id
                                    select new
                                    {
                                        EmployeeCode = s.EmployeeCode == null|| s.EmployeeCode == string.Empty ? "Not available" : s.EmployeeCode,
                                        Name = s.Name == null || s.Name == string.Empty ? "Not available" : s.Name,
                                        Email = s.Email == null || s.Email == string.Empty ? "Not available" : s.Email,
                                        Designation =d==null?"Not available": d.Name
                                    }).ToList();
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
        /// skill ratings of an employee combined.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public IEnumerable<StaffSkills> GetSkillRatingsDetails(string employeeCode)
        {
            var ratings = _reportingStaff.GetRatingDetails();
            var skills = _reportingStaff.GetSkillDetails();
            var skillRatings = _reportingStaff.GetSkillRatingsDetails(employeeCode);

            if (skillRatings.Any())
            {
                var skillRatingDetails = (from sr in skillRatings
                                          join r in ratings on sr.RatingId equals r.Id
                                          join s in skills on sr.SkillId equals s.skillId
                                          select new StaffSkills
                                          {
                                              Skill = s.skillName == null || s.skillName == string.Empty ? "Not available" : s.skillName,
                                              Rating =r==null? default(int) : r.Value,
                                              RatingDate = sr.RatingDate,
                                              Note = sr.Note == null || sr.Note == string.Empty ? "Not available" : sr.Note
                                          }).ToList();
                return skillRatingDetails;
            }
            else
               return Enumerable.Empty<StaffSkills>().ToList();
        }
    }
}
