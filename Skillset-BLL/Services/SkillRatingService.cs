using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Extensions;
using Skillset_DAL.Repositories;
namespace Skillset_BLL.Services
{
   public class SkillRatingService:ISkillRatingService
    {
        private readonly ISkillRatingRepository _iSkillRatingRepository;

        //DI to UserRepository
        public SkillRatingService(ISkillRatingRepository skillRatingRepository)
        {
            _iSkillRatingRepository = skillRatingRepository;
        }
        public int Create(IList<EmployeeSkillRatingDTO>skillRatingDto)
        {    
            return _iSkillRatingRepository.Create(skillRatingDto.ToSkillRatingModelList());
        }
        //Get All employeee ratings to ratedDto
        public IList<EmployeeRatedSkillsDTO> GetRatedSkills(int empId)
        { var RatingValuesList = _iSkillRatingRepository.GetAllRatingValues();
            var RaitingList=_iSkillRatingRepository.GetAllRatings(empId);
            var SkillList= _iSkillRatingRepository.GetAllSkills();
            var RatingViewList = (from r in RaitingList
                                  join s in SkillList on r.SkillId equals s.SkillId
                                  join rt in RatingValuesList on r.RatingId equals rt.Id
                                  select new
                                  {   Id=r.Id,
                                      EmployeeId = r.EmployeeId,
                                      SkillName =r.IsSpecialSkill==true?"Special skill":s.SkillName,
                                      RaitedValue = rt.Value,
                                      RaitedNote=r.Note,
                                      RaitedDate=r.RatingDate
                                      
                                  }).ToList();
            return RatingViewList.Select(employee => new EmployeeRatedSkillsDTO
            {   Id=employee.Id,
                EmployeeId = employee.EmployeeId,
                SkillName = employee.SkillName,
                RaitedNote = employee.RaitedNote,
                RaitedValue = employee.RaitedValue,
                RaitedDate=employee.RaitedDate

            }).ToList();
        }
        public int Delete(int SkillRatingId)
        {
            //Retrieves skill id from unique skill name
            return _iSkillRatingRepository.Delete(SkillRatingId);

         
        }

    }
}
