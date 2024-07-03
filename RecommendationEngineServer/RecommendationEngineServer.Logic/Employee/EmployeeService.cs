using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.Repository.DailyMenu;
using RecommendationEngineServer.DAL.UnitOfWork;


namespace RecommendationEngineServer.Service.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
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

            if(latestNotification == null)
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

        public async Task AddUserMenuImprovementFeedback(MenuImprovementFeedbackRequest menuImprovementFeedback)
        {
            List<UserMenuFeedbackAnswer> userMenuFeedbackAsnwers = new List<UserMenuFeedbackAnswer>();
            var menuItem = (await _unitOfWork.Menu.GetAll()).Where(m => m.FoodItem.FoodName.ToLower() == menuImprovementFeedback.FoodItemName.ToLower()).FirstOrDefault();
           foreach(var improvementFeedback in menuImprovementFeedback.ImprovementFeedbacks)
           {
                UserMenuFeedbackAnswer newAnswer = new UserMenuFeedbackAnswer()
                {
                   MenuId = menuItem.Id,
                    MenuFeedbackQuestionId = improvementFeedback.QuestionId,
                   UserId = menuImprovementFeedback.UserId,
                   Answer = improvementFeedback.Answer
                };

                userMenuFeedbackAsnwers.Add(newAnswer);
           }

           await _unitOfWork.UserMenuFeedbackAnswer.AddMenuImprovementFeedbacks(userMenuFeedbackAsnwers);
        }

        public async Task<int> SelectFoodItemsFromDailyMenu(OrderRequest orderRequest)
        {
           int userId = orderRequest.UserId;
            List<UserOrder> userOrders = new List<UserOrder>();
            Order newOrder = new Order()
            {
                UserId = userId,
                IsDeleted = false,
                IsFeedbackGiven = false,
            };

            await _unitOfWork.Order.Create(newOrder);
            await _unitOfWork.Complete();

            foreach( var dailyMenuId in orderRequest.DailyMenuIds) 
            {
                UserOrder newUserOrder = new UserOrder()
                {
                    DailyMenuId = dailyMenuId,
                    OrderId = newOrder.Id
                };
                userOrders.Add(newUserOrder);
            }

            await _unitOfWork.UserOrder.AddUserOrders(userOrders);
            await _unitOfWork.Complete();
            return newOrder.Id;
        }

        public async Task GiveFeedBack(List<GiveFeedBackRequest> giveFeedBackRequest)
        {
            List<Feedback> feedbackList = new List<Feedback>();

            foreach(var feedbacks in giveFeedBackRequest)
            {
                var menu = await _unitOfWork.Menu.GetById(feedbacks.MenuIds);
                var dailyMenu = await _unitOfWork.DailyMenu.GetById(feedbacks.DailyMenuId);
                var orderOfUser = (await _unitOfWork.Order.GetAll())
                                       .Where(o => o.UserId == feedbacks.UserId && o.IsFeedbackGiven == false)
                                       .FirstOrDefault();
                orderOfUser.IsFeedbackGiven = true;
                Feedback newFeedback = new Feedback()
                {
                    MenuId = menu.Id,
                    UserId = feedbacks.UserId,
                    Rating = feedbacks.Rating,
                    Comment = feedbacks.Comments,
                    Created = dailyMenu.Date,
                    IsDeleted = false,
                };

                feedbackList.Add(newFeedback);
            }

           await _unitOfWork.Feedback.AddUserFeebacks(feedbackList);
           await _unitOfWork.Complete();
        }

        public async Task<List<UserOrderMenuModel>> GetMenuItemsByOrderId(int orderId)
        {
            return await _unitOfWork.Menu.GetMenuItemsByOrderId(orderId);
        }

        public async Task<List<FeedbackQuestion>> GetMenuFeedBackQuestions()
        {
            List<FeedbackQuestion> feedbackQuestionList = new List<FeedbackQuestion>();
            var allFeedbackQuestion = (await _unitOfWork.MenuFeedbackQuestion.GetAll()).ToList();
            foreach (var question in allFeedbackQuestion)
            {
                FeedbackQuestion feedbackQuestion = new FeedbackQuestion()
                {
                     QuestionId = question.Id,
                      Question = question.Question
                };

                feedbackQuestionList.Add(feedbackQuestion);
            }

            return feedbackQuestionList;
        }
        #endregion
    }
}
