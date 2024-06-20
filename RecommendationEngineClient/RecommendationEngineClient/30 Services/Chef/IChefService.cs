namespace RecommendationEngineClient._30_Services.Chef
{
    public interface IChefService
    {
        Task GetMenuList();
        Task AddDailyMenuItem();
        Task SendNotification();
    }
}
