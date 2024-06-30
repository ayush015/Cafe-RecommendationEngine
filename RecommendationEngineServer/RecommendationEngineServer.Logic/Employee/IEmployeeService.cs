using RecommendationEngineServer.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Service.Employee
{
    public interface IEmployeeService
    {
        Task<string> GetNotification(NotificationRequest notificationRequest);
        Task<int> SelectFoodItemsFromDailyMenu(OrderRequest orderRequest);
        Task GiveFeedBack(List<GiveFeedBackRequest> giveFeedBackRequest);
        Task<List<UserOrderMenuModel>> GetMenuItemsByOrderId(int orderId);
    }
}
