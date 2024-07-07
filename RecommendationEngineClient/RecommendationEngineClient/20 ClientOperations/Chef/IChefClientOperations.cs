namespace RecommendationEngineClient._20_ClientOperations.Chef
{
    public interface IChefClientOperations
    {
        Task GetMenuList();
        Task AddDailyMenuItem();
        Task SendDailyMenuNotification();
        Task GetMonthlyNotification();
    }
}
