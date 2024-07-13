using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
