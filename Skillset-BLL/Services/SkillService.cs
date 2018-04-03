using System;
using System.Collections.Generic;
using System.Linq;
using Common.DTO;
using Skillset_DAL.Repositories;
using Common.Extensions;

namespace Skillset_BLL.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _iSkillRepository;

        //DI to UserRepository
        public SkillService(ISkillRepository skillRepository)
        {
            _iSkillRepository = skillRepository;
        }

        public int Create(SkillDTO skill)
        {
            return _iSkillRepository.Create(skill.ToModel());
        }

        public int Delete(string skillName)
        {
            //Retrieves skill id from unique skill name
            var skillId = _iSkillRepository.GetIdBySkillName(skillName);

            return _iSkillRepository.Delete(skillId);
        }

        public IList<SkillDTO> GetAllSkills()
        {
            return _iSkillRepository.GetAllSkills().ToDtoList();
        }

        public SkillDTO GetBySkillName(string skillName)
        {
            return _iSkillRepository.GetSkillBySkillName(skillName).ToDTO(); 

        }

        public int Update(SkillDTO skill)
        {
            return _iSkillRepository.Update(skill.ToModel());
        }

        
    }
}
