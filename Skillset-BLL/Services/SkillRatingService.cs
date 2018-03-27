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
      /*  public IList<SkillDTO> GetAllSkills(int empId)
        {
            _iSkillRatingRepository.
        }*/

    }
}
