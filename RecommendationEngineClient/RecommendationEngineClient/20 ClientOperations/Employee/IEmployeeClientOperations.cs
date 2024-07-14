namespace RecommendationEngineClient._20_ClientOperations.Employee
{
    public interface IEmployeeClientOperations
    {
        Task<int> GetNotification(int userId);
        Task SelectFoodItemsFromDailyMenu(int userId);
        Task GiveFeedBack(int userId);
        Task AddUserFoodPreference(int userId);
    }
}
