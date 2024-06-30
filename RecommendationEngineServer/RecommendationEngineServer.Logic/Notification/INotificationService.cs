using RecommendationEngineServer.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Logic.Notification
{
    public interface INotificationService
    {
        public Task<List<RecommendedMenuModel>> GetMonthlyDiscardedMenuNotification(DateTime currentDate, List<RecommendedMenuModel> recommendedMenus);
    }
}
