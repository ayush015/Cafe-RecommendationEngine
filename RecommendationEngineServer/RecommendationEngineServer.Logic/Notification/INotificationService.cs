using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Service.Notifications
{
    public interface INotificationService
    {
        public Task<List<RecommendedMenuModel>> GetMonthlyDiscardedMenuNotification(DateTime currentDate, List<RecommendedMenuModel> recommendedMenus);
        Task AddNewNotificationForDiscardedMenuFeedback(MenuImprovementNotification menuImprovement);
    }
}
