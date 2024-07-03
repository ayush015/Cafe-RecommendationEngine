using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.ImprovementRecord
{
    public interface IUserMenuFeedbackAnswer : IGenericRepository<Models.UserMenuFeedbackAsnwer>
    {
        Task AddMenuImprovementFeedbacks(IEnumerable<Models.UserMenuFeedbackAsnwer> imporvementFeedbacks);
    }
}
