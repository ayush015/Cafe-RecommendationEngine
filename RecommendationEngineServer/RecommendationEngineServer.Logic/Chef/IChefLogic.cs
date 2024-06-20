using RecommendationEngineServer.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Logic.Chef
{
    public interface IChefLogic
    {
        Task SendNotification();
        Task<int> AddDailyMenuItem(List<int> menuIds);
        Task<List<RecommendedMenu>> GetMenuListItems();
    }
}
