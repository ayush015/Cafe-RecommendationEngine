using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.ImprovementRecord
{
    public class UserMenuFeedbackAnswer : GenericRepository<Models.UserMenuFeedbackAsnwer>, IUserMenuFeedbackAnswer
    {
        public UserMenuFeedbackAnswer(DbContext context) : base(context)
        {
        }
    }
}
