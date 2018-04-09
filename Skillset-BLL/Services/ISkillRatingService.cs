﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
namespace Skillset_BLL.Services
{
   public interface ISkillRatingService
    {
        /// <summary>
        /// Insert skillRating into table SkillRating
        /// </summary>
        /// <param name="skillRating"></param>
        /// <returns></returns>
        int Create(IList< EmployeeSkillRatingDTO> skillRating);

        /// <summary>
        /// Get all rated skills of employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        IList<EmployeeRatedSkillsDTO> GetRatedSkills(int empId);

        /// <summary>
        /// Remove skillrating from table Skillrating
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        int Delete(int SkillRatingId);

        /// <summary>
        /// Retrieve skill names of skills rated by employee
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkillName();

        /// <summary>
        /// Retrieve skill names of skills rated by employee excluding special skill
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetEmployeeRatedSkillExcludeSpecial();

        /// <summary>
        /// Retrieve total ratings for each skill given by employees
        /// </summary>
        /// <returns></returns>
        string GetEmployeeRating();

        /// <summary>
        /// Retrieve total skill ratings count
        /// </summary>
        /// <returns></returns>
        string GetRatingAverage();

        /// <summary>
        /// Retrieve average ratings for primary skills
        /// </summary>
        /// <returns></returns>
        int GetSkillRatingsCount();
    }
}
