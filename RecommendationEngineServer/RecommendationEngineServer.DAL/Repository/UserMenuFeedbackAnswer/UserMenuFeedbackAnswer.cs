using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.ImprovementRecord
{
    public class UserMenuFeedbackAnswer : GenericRepository<Models.UserMenuFeedbackAnswer>, IUserMenuFeedbackAnswer
    {
        private RecommendationEngineDBContext _dbContext;
        public UserMenuFeedbackAnswer(DbContext context) : base(context)
        {
            _dbContext = (RecommendationEngineDBContext)context;
        }

        public async Task AddMenuImprovementFeedbacks(IEnumerable<Models.UserMenuFeedbackAnswer> imporvementFeedbacks)
        {
            await _dbContext.AddRangeAsync(imporvementFeedbacks);
            await _dbContext.SaveChangesAsync();
        }

    }
}
