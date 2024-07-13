using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.UnitOfWork;
using RecommendationEngineServer.DAL.Models;
using System.Text;

namespace RecommendationEngineServer.Service.Notifications
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Public Methods
        public async Task<NotificationResponse> GetNotification(NotificationRequest notificationRequest)
        {
            var lastSeenUserNotification = (await _unitOfWork.UserNotification.GetAll())
                                            .Where(x => x.UserId == notificationRequest.UserId)
                                            .FirstOrDefault();

            int? lastSeenNotificationId = lastSeenUserNotification?.LastSeenNotificationId;
            var allNotification = (await _unitOfWork.Notification.GetAll()).Where(d => d.CreatedDate == notificationRequest.CurrentDate);

            var latestNotification = allNotification
                                    .Where(n => lastSeenNotificationId == null || n.Id > lastSeenNotificationId)
                                    .FirstOrDefault();

            if (latestNotification == null)
            {
                return new NotificationResponse
                {
                    NotificationMessgae = string.Empty
                };

            }

            if (lastSeenNotificationId == null)
            {
                UserNotification userNotification = new UserNotification()
                {
                    UserId = notificationRequest.UserId,
                    LastSeenNotificationId = latestNotification.Id
                };

                await _unitOfWork.UserNotification.Create(userNotification);
            }
            else
            {
                lastSeenUserNotification.LastSeenNotificationId = latestNotification.Id;
            }
            await _unitOfWork.Complete();
            return new NotificationResponse
            {
                NotificationMessgae = latestNotification.Message,
                NotificationTypeId = latestNotification.NotificationTypeId,
            };

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

        public async Task AddNewNotificationForDiscardedMenuFeedback(MenuImprovementNotificationRequest menuImprovement)
        {
            var feedBackQuestions = (await _unitOfWork.MenuFeedbackQuestion.GetAll()).ToList();
            var improveMenuItem = "We are trying to improve your experience with <Food Item>. Please provide your feedback and help us.";
            var menuItem = await _unitOfWork.Menu.GetById(menuImprovement.MenuId);
            StringBuilder notificationMessage = new StringBuilder();

            foreach (var question in feedBackQuestions)
            {
                string message = $"Q{question.Id} {question.Question.Replace("<Food Item>",menuItem.FoodItem.FoodName)}";
                notificationMessage.AppendLine(message);
            }

            notificationMessage.Insert(0, $"\n{improveMenuItem.Replace("<Food Item>",menuItem.FoodItem.FoodName)}\n");

            Notification addNotification = new Notification()
            {
                Message = notificationMessage.ToString(),
                CreatedDate = menuImprovement.CurrentDate,
                NotificationTypeId = 4
            };

            await _unitOfWork.Notification.Create(addNotification);
            await _unitOfWork.Complete();
        }

        #endregion

        #region Private Methods
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
        #endregion


    }
}
