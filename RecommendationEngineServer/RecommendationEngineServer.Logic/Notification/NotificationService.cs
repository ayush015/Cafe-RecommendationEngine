using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.UnitOfWork;
using RecommendationEngineServer.Service.Chef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Logic.Notification
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<RecommendedMenuModel>> GetMonthlyDiscardedMenuNotification(DateTime currentDate, List<RecommendedMenuModel> recommendedMenus)
        {
            var firstDailyMenuDate = await GetFirstDailyMenuDate();

            double daysDifference = (currentDate - firstDailyMenuDate).TotalDays;

            if(daysDifference % 30 == 0)
            {
                return recommendedMenus.Where(m => m.RecommendationScore <= 2.0 && m.RecommendationScore > 0.0).ToList();
            }

           return new List<RecommendedMenuModel>(); 
        }

        private async Task<DateTime> GetFirstDailyMenuDate()
        {
            var firstDailyMenu = (await _unitOfWork.DailyMenu.GetAll())
                                 .OrderBy(dm => dm.Date)
                                 .FirstOrDefault();
            if (firstDailyMenu == null)
            {
                throw new Exception("No daily menu found.");
            }
            return firstDailyMenu.Date;
        }
    }
}
