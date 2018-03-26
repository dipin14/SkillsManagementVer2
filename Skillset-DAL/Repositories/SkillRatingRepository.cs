using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillset_DAL.Models;
using Skillset_DAL.ContextClass;
namespace Skillset_DAL.Repositories
{
   public class SkillRatingRepository:ISkillRatingRepository
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
                            }
                            else
                            {
                                var obj = db.SkillRatings.Find(skillRating.SkillId);

                                db.SkillRatings.Add(skillRating);
                            }
                            }
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
    }
}
