using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
