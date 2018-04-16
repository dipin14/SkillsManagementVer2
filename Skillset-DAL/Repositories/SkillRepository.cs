using Skillset_DAL.ContextClass;
using Skillset_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Skillset_DAL.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        /// <summary>
        /// Insert skill into Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public int Create(Skill skill)
        {
            try
            {
                if (skill != null)
                {
                    using (var db = new SkillsetDbContext())
                    {
                        skill.Status = true;
                        //Check if Skill Name exists already
                        if (db.Skills.Where(s => s.SkillName.ToLower() == skill.SkillName.ToLower()).Count() != 0)
                        {
                            return -1;
                        }
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

        /// <summary>
        /// Update skill in Skill table
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public int Update(Skill skill)
        {
            if (skill != null)
            {
                using (var db = new SkillsetDbContext())
                {
                    skill.Status = true;
                    //Check if Skill Name exists already
                    if (db.Skills.Where(s => s.SkillName.ToLower() == skill.SkillName.ToLower() && s.SkillId != skill.SkillId).Count() != 0)
                    {
                        return -1;
                    }
                    db.Entry(skill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Sets status of skill to false using Skill Is
        /// </summary>
        /// <param name="skill Id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieve all skills from Skill table
        /// </summary>
        /// <returns></returns>
        public IList<Skill> GetAllSkills()
        {
            using (var db = new SkillsetDbContext())
            {
                var skillList = db.Skills.Where(s => s.Status == true).ToList();
                return skillList;
            }
        }


        /// <summary>
        /// Retrieve skill id using unique skill name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        public int GetIdBySkillName(string skillName)
        {
            using (var db = new SkillsetDbContext())
            {
                var skillId = db.Skills.Where(s => s.SkillName == skillName && s.Status == true).FirstOrDefault().SkillId;
                return skillId;
            }
        }

        /// <summary>
        /// Retrieve skill object using skill name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        public Skill GetSkillBySkillName(string skillName)
        {
            using (var db = new SkillsetDbContext())
            {
                var skillDetails = db.Skills.Where(s => s.SkillName == skillName && s.Status == true).FirstOrDefault();
                return skillDetails;
            }
        }

        /// <summary>
        /// Retrieve total skill count
        /// </summary>
        /// <returns></returns>
        public int GetSkillsCount()
        {
            int skillsCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                //Get distinct count of skills where status of skill is true
                skillsCount = context.Skills.Where(s => s.Status).Distinct().Count();
            }
            return skillsCount;
        }
    }
}
