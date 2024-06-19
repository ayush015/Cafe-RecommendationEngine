using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._30_Services.Employee
{
    public interface IEmployeeService
    {
        Task GetNotification(int userId);
        Task SelectFoodItemsFromDailyMenu(int userId);
        Task GiveFeedBack(int userId);
    }
}
