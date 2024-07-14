namespace RecommendationEngineClient._20_ClientService.Chef
{
    public interface IChefService
    {
        Task GetMenuList();
        Task AddDailyMenuItem();
        Task SendDailyMenuNotification();
        Task GetMonthlyNotification();
    }
}
