using RecommendationEngineServer.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Service.Chef
{
    public interface IChefService
    {
        Task SendDailyMenuNotification(DateTime currentDate);
        Task<int> AddDailyMenuItem(MenuItem menuItem);
        Task<List<RecommendedMenuModel>> GetMenuListItems();
        Task DiscardMenuItem(int menuId);
    }
}
