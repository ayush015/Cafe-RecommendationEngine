using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Service.Notifications
{
    public interface INotificationService
    {
        Task<NotificationResponse> GetNotification(NotificationRequest notificationRequest);
        public Task<List<RecommendedMenuModel>> GetMonthlyDiscardedMenuNotification(DateTime currentDate, List<RecommendedMenuModel> recommendedMenus);
        Task AddNewNotificationForDiscardedMenuFeedback(MenuImprovementNotificationRequest menuImprovement);
    }
}
