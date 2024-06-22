namespace RecommendationEngineClient._20_Services.Chef
{
    public interface IChefService
    {
        Task GetMenuList();
        Task AddDailyMenuItem();
        Task SendNotification();
    }
}
