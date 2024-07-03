﻿using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.UnitOfWork;
using RecommendationEngineServer.DAL.Models;
using System.Text;

namespace RecommendationEngineServer.Logic.Notifications
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Public Methods
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

        public async Task AddNewNotificationForDiscardedMenuFeedback(DateTime currentDate,int menuId)
        {
            var feedBackQuestions = (await _unitOfWork.MenuFeedbackQuestion.GetAll()).ToList();
            var improveMenuItem = "We are trying to improve your experience with <Food Item>. Please provide your feedback and help us.";
            var menuItem = await _unitOfWork.Menu.GetById(menuId);
            StringBuilder notificationMessage = new StringBuilder();

            foreach (var question in feedBackQuestions)
            {
                string message = $"{question.Question.Replace("<Food Item>",menuItem.FoodItem.FoodName)}";
                notificationMessage.AppendLine(message);
            }

            notificationMessage.Insert(0, $"\n{improveMenuItem.Replace("<Food Item>",menuItem.FoodItem.FoodName)}\n");

            Notification addNotification = new Notification()
            {
                Message = notificationMessage.ToString(),
                CreatedDate = currentDate,
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
