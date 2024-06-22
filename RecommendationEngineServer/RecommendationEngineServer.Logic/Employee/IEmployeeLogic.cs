using RecommendationEngineServer.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Logic.Employee
{
    public interface IEmployeeLogic
    {
        Task<string> GetNotification(int userId);
        Task<int> SelectFoodItemsFromDailyMenu(OrderRequest orderRequest);
        Task GiveFeedBack(List<GiveFeedBackRequest> giveFeedBackRequest);
        Task<List<UserOrderMenuModel>> GetMenuItemsByOrderId(int orderId);
    }
}
