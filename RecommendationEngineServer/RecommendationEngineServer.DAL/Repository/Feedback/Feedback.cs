using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Feedback
{
    public class Feedback : GenericRepository<Models.Feedback>, IFeedback
    {
        private RecommendationEngineDBContext _dbContext;
        public Feedback(DbContext context) : base(context)
        {
            _dbContext = (RecommendationEngineDBContext)context;
        }

        public async Task AddUserFeebacks(IEnumerable<Models.Feedback> feedbacks)
        {
            await _dbContext.Feedbacks.AddRangeAsync(feedbacks);
        }
    }
}
