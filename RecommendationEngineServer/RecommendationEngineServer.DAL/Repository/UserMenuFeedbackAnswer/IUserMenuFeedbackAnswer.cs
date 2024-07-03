using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.ImprovementRecord
{
    public interface IUserMenuFeedbackAnswer : IGenericRepository<Models.UserMenuFeedbackAnswer>
    {
        Task AddMenuImprovementFeedbacks(IEnumerable<Models.UserMenuFeedbackAnswer> imporvementFeedbacks);
    }
}
