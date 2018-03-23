using Skillset_DAL.ContextClass;
using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        public int Create(Skill skill)
        {
            try
            {
                if (skill != null)
                {
                    using (var db = new SkillsetDbContext())
                    {
                        skill.status = true;
                        db.Skills.Add(skill);
                        db.SaveChanges();
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Update(Skill skill)
        {
            if (skill != null)
            {
                using (var db = new SkillsetDbContext())
                {
                    db.Entry(skill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return 1;
            }
            return 0;
        }

        public int Delete(int? skillId)
        {
            if (skillId != null)
            {
                using (var db = new SkillsetDbContext())
                {
                    var deleteSkill = db.Skills.Find(skillId);
                    deleteSkill.status = false;
                    db.Entry(deleteSkill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return 1;
            }
            return 0;
        }

        public IList<Skill> GetAllSkills()
        {
            using (var db = new SkillsetDbContext())
            {
                var skillList = db.Skills.Where(s => s.status == true).ToList();
                return skillList;
            }
        }

        public int GetIdBySkillName(string skillName)
        {
            using (var db = new SkillsetDbContext())
            {
                var skillId = db.Skills.Where(s => s.skillName == skillName).FirstOrDefault().skillId;
                return skillId;
            }
        }

        public Skill GetSkillBySkillName(string skillName)
        {
            using (var db = new SkillsetDbContext())
            {
                var skillDetails = db.Skills.Where(s => s.skillName == skillName).FirstOrDefault();
                return skillDetails;
            }
        }
    }
}
