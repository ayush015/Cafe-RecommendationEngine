using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Service.Employee
{
    public interface IEmployeeService
    {
        Task<int> SelectFoodItemsFromDailyMenu(OrderRequest orderRequest);
        Task GiveFeedBack(List<GiveFeedBackRequest> giveFeedBackRequest);
        Task<List<UserOrderMenuModel>> GetMenuItemsByOrderId(int orderId);
        Task AddUserMenuImprovementFeedback(MenuImprovementFeedbackRequest menuImprovementFeedback);
        Task<List<FeedbackQuestion>> GetMenuFeedBackQuestions();
        Task AddUserPreference(UserPreferenceRequest userPreferenceRequest);
        Task<List<RolledOutMenu>> GetRolledOutMenus(DailyRolledOutMenuRequest dailyRolledOutMenuRequest);
    }
}
