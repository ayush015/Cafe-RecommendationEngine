using RecommendationEngineServer.DAL.UnitOfWork;
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
        public Task<string> GetMonthlyNotification(DateTime currentDate)
        {
            throw new NotImplementedException();
        }
    }
}
