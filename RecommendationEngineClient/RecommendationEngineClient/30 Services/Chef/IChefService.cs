namespace RecommendationEngineClient._30_Services.Chef
{
    public interface IChefService
    {
        Task GetMenuList();
        Task GetRecommendedMenuList();
        Task AddDailyMenuItem();
        Task SendNotification();
    }
}
