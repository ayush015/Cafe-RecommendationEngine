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
        Task SendNotification();
        Task<int> AddDailyMenuItem(List<int> menuIds);
        Task<List<RecommendedMenuModel>> GetMenuListItems();
    }
}
