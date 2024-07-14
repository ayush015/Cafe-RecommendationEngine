namespace RecommendationEngineClient._20_ClientService.Employee
{
    public interface IEmployeeService
    {
        Task<int> GetNotification(int userId);
        Task SelectFoodItemsFromDailyMenu(int userId);
        Task GiveFeedBack(int userId);
        Task AddUserFoodPreference(int userId);
    }
}
