using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Service.Chef
{
    public interface IChefService
    {
        Task SendDailyMenuNotification(DateTime currentDate);
        Task<int> AddDailyMenuItem(MenuItem menuItem);
        Task<List<RecommendedMenuModel>> GetMenuListItems();
        Task DiscardMenuItem(int menuId);
    }
}
