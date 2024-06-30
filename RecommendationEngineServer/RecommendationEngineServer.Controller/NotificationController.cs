using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Logic.Notification;
using RecommendationEngineServer.Service.Employee;

namespace RecommendationEngineServer.Controller
{
    public class NotificationController
    {
        private INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<NotificationResponse> GetMonthlyNotification(DateTime currentDate)
        {
            try
            {
                var notification = await _notificationService.GetMonthlyNotification(currentDate);
                if(string.IsNullOrEmpty(notification))
                {
                    return new NotificationResponse
                    {
                        Status = ApplicationConstants.StatusSuccess,
                        IsNewNotification = false,
                    };
                }

                return new NotificationResponse
                {
                    Status = ApplicationConstants.StatusSuccess,
                    NotificationMessgae = notification,
                    Message = ApplicationConstants.SentNotificationSuccessfully,
                    IsNewNotification = true,
                };
            }
            catch (Exception ex)
            {
                return new NotificationResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
                };
            }
        }
    }
}

