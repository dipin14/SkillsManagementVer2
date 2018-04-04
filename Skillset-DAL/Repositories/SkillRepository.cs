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
                        skill.Status = true;
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
                    skill.Status = true;
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
                    deleteSkill.Status = false;

                    StringBuilder builder = new StringBuilder();
                    //Changing skill name of deleted skill to prevent conflict

                    builder.Append(deleteSkill.SkillName).Append("-deleted-").Append(deleteSkill.SkillId);
                    deleteSkill.SkillName = builder.ToString();
                    db.Entry(deleteSkill).State = EntityState.Modified;

                    //Corresponding ratings in skill has to be deleted
                    var deleteRatingSkill = db.SkillRatings.Where(s => s.SkillId == skillId).ToList();
                    deleteRatingSkill.ForEach(a => a.Status = false);
                    foreach (var ratingSkill in deleteRatingSkill)
                    {
                        db.Entry(ratingSkill).State = EntityState.Modified;
                    }
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
                var skillList = db.Skills.Where(s => s.Status == true).ToList();
                return skillList;
            }
        }

        public int GetIdBySkillName(string skillName)
        {
            using (var db = new SkillsetDbContext())
            {
                var skillId = db.Skills.Where(s => s.SkillName == skillName && s.Status == true).FirstOrDefault().SkillId;
                return skillId;
            }
        }

        public Skill GetSkillBySkillName(string skillName)
        {
            using (var db = new SkillsetDbContext())
            {
                var skillDetails = db.Skills.Where(s => s.SkillName == skillName && s.Status == true).FirstOrDefault();
                return skillDetails;
            }
        }

        public int GetSkillsCount()
        {
            int skillsCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                skillsCount = context.Skills.Where(s => s.Status).Distinct().Count();
            }
            return skillsCount;
        }
    }
}
