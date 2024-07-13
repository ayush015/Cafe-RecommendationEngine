using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.ImprovementQuestion
{
    public class MenuFeedbackQuestion : GenericRepository<Models.MenuFeedbackQuestion>, IMenuFeedbackQuestion
    {
        public MenuFeedbackQuestion(DbContext context) : base(context)
        {
        }
    }
}
