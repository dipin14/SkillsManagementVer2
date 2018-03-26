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
    }
}
