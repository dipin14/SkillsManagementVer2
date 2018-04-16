using System;
using System.Collections.Generic;
using System.Linq;
using Skillset_DAL.Models;
using Skillset_DAL.ContextClass;
using System.Data.Entity;
using System.Text;

namespace Skillset_DAL.Repositories
{
    public class SkillRatingRepository : ISkillRatingRepository
    {  /// <summary>
       /// Insert skillRatings into SkillRating table
       /// </summary>
       /// <param name="skillRatingList"></param>
       /// <returns></returns>
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
        /// <summary>
        /// Retrieve all skillsRatings from SkillRatings table
        /// </summary>
        /// <returns></returns>
        public IList<SkillRating> GetAllRatings(int empId)
        {

            using (var db = new SkillsetDbContext())
            {
                var skillRatingList = db.SkillRatings.Where(s => s.EmployeeId == empId && s.Status == true).OrderByDescending(s => s.RatingDate).ToList();
                return skillRatingList;
            }

        }
        /// <summary>
        /// Retrieve all skills from Skill table
        /// </summary>
        /// <returns></returns>
        public IList<Skill> GetAllSkills()
        {
            using (var db = new SkillsetDbContext())
            {
                var skillList = db.Skills.ToList();
                return skillList;
            }
        }
        /// <summary>
        /// Retrieve all skillsRatingsValues from Rating table
        /// </summary>
        /// <returns></returns>
        public IList<Rating> GetAllRatingValues()
        {
            using (var db = new SkillsetDbContext())
            {
                var RatingValues = db.Ratings.ToList();
                return RatingValues;
            }
        }


        /// <summary>
        /// Sets status of skillRating to false using SkillRating table
        /// </summary>
        /// <param name="SkillRatingId "></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieve skill names of skills rated by employee excluding special skill
        /// </summary>
        /// <returns></returns>
        public IQueryable<string> GetEmployeeRatedSkillExcludeSpecial()
        {
            SkillsetDbContext context = new SkillsetDbContext();
            {
                //Get distinct skill name and skill id 
                var skills = (from s in context.SkillRatings
                              join j in context.Skills
                              on s.SkillId equals j.SkillId
                              where s.Status == true && j.Status == true
                              select new { j.SkillName, j.SkillId }).Distinct();

                //Get skill name and skill id excluding special skill
                var skill = from s in skills
                            orderby s.SkillId descending
                            where s.SkillName != "Special Skill"
                            select s.SkillName;

                return skill;
            }
        }

        /// <summary>
        /// Retrieve average ratings for primary skills
        /// </summary>
        /// <returns></returns>
        public string GetRatingAverage()
        {
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                List<int> totalValues = new List<int>();
                List<int> specificValues = new List<int>();
                StringBuilder result = new StringBuilder();
                string id = string.Empty;

                //Get total ratings count for each skill
                totalValues = GetTotalValue();

                //Get sum of actual ratings for each skill
                specificValues = GetSpecificValue();
               
                for (int i = 0; i < specificValues.Count; i++)
                {
                    //Calculate average rating for each skill
                    float ratingAvg = (float)specificValues.ElementAt(i) / (float)totalValues.ElementAt(i);
                    result.Append(ratingAvg);
                    result.Append(", ");
                }
                return result.ToString();

            }
        }

        /// <summary>
        /// Retrieve total ratings count for each skill
        /// </summary>
        /// <returns></returns>
        public List<int> GetTotalValue()
        {
            List<int> totalValues = new List<int>();
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                var rating = context.SkillRatings.Where(x => x.Status == true);
                var groupRating = rating.GroupBy(x => x.SkillId).Select(x => new { Id = x.Key, Values = x.Distinct().Count() });

                foreach (var r in groupRating.OrderByDescending(x => x.Id).Select(x => x.Values))
                {
                    //Get total no of rating for each skill
                    totalValues.Add(r);
                }
                return totalValues;
            }
        }

        /// <summary>
        /// Retrieve sum of actual ratings for each skill
        /// </summary>
        /// <returns></returns>
        public List<int> GetSpecificValue()
        {
            List<int> specificValues = new List<int>();
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
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
                    //Get acual rating for each skill
                    specificValues.Add(r);
                }
                return specificValues;
            }
        }

        /// <summary>
        /// Retrieve total skill ratings count
        /// </summary>
        /// <returns></returns>
        public int GetSkillRatingsCount()
        {
            int skillRatingsCount = default(int);
            using (SkillsetDbContext context = new SkillsetDbContext())
            {
                //Get distinct skill ratings count excluding special skill
                skillRatingsCount = context.SkillRatings.Where(s => s.Status).Where(s => s.SkillId != 1).Distinct().Count();
            }
            return skillRatingsCount;
        } 
    }
}
