using System;
using System.Collections.Generic;
using System.Linq;
using Skillset_DAL.Models;
using Skillset_DAL.ContextClass;
using System.Data.Entity;

namespace Skillset_DAL.Repositories
{
    public class SkillRatingRepository : ISkillRatingRepository
    {
        public int Create(IList<SkillRating> skillRatingList)
        {
            try
            {
                if (skillRatingList != null)
                {
                    using (var db = new SkillsetDbContext())
                    {
                        foreach (var skillRating in skillRatingList)
                        {
                            if (db.SkillRatings.Any(o => o.EmployeeId == skillRating.EmployeeId && o.SkillId == skillRating.SkillId))
                            {
                                var OldratingObj = db.SkillRatings.SingleOrDefault(o => o.EmployeeId == skillRating.EmployeeId && o.SkillId == skillRating.SkillId);
                                var id = OldratingObj.Id;
                                var updateObj = db.SkillRatings.Find(id);
                                updateObj.RatingId = skillRating.RatingId;
                                updateObj.Note = skillRating.Note;
                                updateObj.RatingDate = DateTime.Now;
                                updateObj.Status = true;
                            }
                            else
                            {

                                db.SkillRatings.Add(skillRating);
                            }
                        }
                        db.SaveChanges();
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public IList<SkillRating> GetAllRatings(int empId)
        {

            using (var db = new SkillsetDbContext())
            {
                var skillRatingList = db.SkillRatings.Where(s => s.EmployeeId == empId && s.Status == true).OrderByDescending(s => s.RatingDate).ToList();
                return skillRatingList;
            }

        }
        public IList<Skill> GetAllSkills()
        {
            using (var db = new SkillsetDbContext())
            {
                var skillList = db.Skills.ToList();
                return skillList;
            }
        }

        public IList<Rating> GetAllRatingValues()
        {
            using (var db = new SkillsetDbContext())
            {
                var RatingValues = db.Ratings.ToList();
                return RatingValues;
            }
        }
        public int Delete(int? SkillRatingId)
        {
            if (SkillRatingId != null)
            {
                using (var db = new SkillsetDbContext())
                {
                    var deleteSkillRating = db.SkillRatings.Find(SkillRatingId);
                    deleteSkillRating.Status = false;
                    //Changin skill name of deleted skill to prevent conflict
                    db.Entry(deleteSkillRating).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return 1;
            }
            return 0;
        }

        public IQueryable<string> GetEmployeeRatedSkillExcludeSpecial()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where s.Status == true && j.Status == true
                              select new { j.SkillName, j.SkillId }).Distinct();

                var skill = from s in skills
                            orderby s.SkillId descending
                            where s.SkillName != "Special Skill"
                            select s.SkillName;

                return skill;
            }
        }

        public string GetRatingAverage()
        {
            using(SkillsetDbContext context = new SkillsetDbContext())
            {
                List<int> totalValues = new List<int>();
                List<int> specificValues = new List<int>();
                var result = string.Empty;
                string id = string.Empty;
                var rating = context.SkillRatings.Where(x => x.Status == true);
                var groupRating = rating.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Distinct().Count() });

                foreach (var r in groupRating.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    totalValues.Add(r);
                }

                var skill = (from sr in context.SkillRatings
                             join r in context.Ratings
                             on sr.RatingId equals r.Id
                             join s in context.Skills
                             on sr.SkillId equals s.SkillId
                             where sr.Status == true && s.Status == true
                             select new { sr.SkillId, r.Value });

                var groupSkill = skill.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Select(s => s.Value).Sum() });
                foreach (var r in groupSkill.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    specificValues.Add(r);
                }
                for (int i = 0; i < specificValues.Count; i++)
                {
                    float ratingAvg = (float)specificValues.ElementAt(i) / (float)totalValues.ElementAt(i);
                    result += ratingAvg;
                    result += ", ";
                }
                return result;

            }
        }
        
        public string GetTopEmployeeRating()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                string result = string.Empty;
                string id = string.Empty;
                var MaximumRatingId = context.Ratings.Where(s => s.Value == 5).Select(s => s.Id).FirstOrDefault();
                int RatingId = Convert.ToInt32(MaximumRatingId);

                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where j.Status == true && s.Status == true && s.RatingId==RatingId
                              select s);
                var groupRating = skills.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Distinct().Count() });

                foreach (var r in groupRating.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    result += r;
                    result += ", ";
                }
                return result;
            }
        }

        public string GetLeastEmployeeRating()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                string result = string.Empty;
                string id = string.Empty;
                var MaximumRatingId = context.Ratings.Where(s => s.Value == 1).Select(s => s.Id).FirstOrDefault();
                int RatingId = Convert.ToInt32(MaximumRatingId);

                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where j.Status == true && s.Status == true && s.RatingId==RatingId
                              select s);
                var groupRating = skills.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Distinct().Count() });

                foreach (var r in groupRating.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    result += r;
                    result += ", ";
                }
                return result;
            }
        }

        public int GetSkillRatingsCount()
        {
            int skillRatingsCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                skillRatingsCount = context.SkillRatings.Where(s => s.Status).Where(s => s.SkillId != 1).Distinct().Count();
            }
            return skillRatingsCount;
        }

        public IQueryable<string> GetEmployeeRatedSkillName()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where s.Status == true && j.Status == true
                              select new { j.SkillName, j.SkillId }).Distinct();

                var skill = from s in skills
                            orderby s.SkillId descending
                            select s.SkillName;

                return skill;
            }
        }
    }
}
