using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Logic.Notifications
{
    public interface INotificationService
    {
        public Task<List<RecommendedMenuModel>> GetMonthlyDiscardedMenuNotification(DateTime currentDate, List<RecommendedMenuModel> recommendedMenus);
    }
}
