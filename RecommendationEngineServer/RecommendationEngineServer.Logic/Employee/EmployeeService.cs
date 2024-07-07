using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Enum;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.DAL.Models;
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

            if (menuItem == null) throw new MenuItemNotFoundException();

           foreach (var improvementFeedback in menuImprovementFeedback.ImprovementFeedbacks)
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

        public async Task AddUserPreference(UserPreferenceRequest userPreferenceRequest)
        {
            var userPreference = (await _unitOfWork.UserFoodPreference.GetAll()).Where(u => u.UserId == userPreferenceRequest.UserId).FirstOrDefault();

            if(userPreference != null)
            {
                userPreference.FoodTypeId = (int)(FoodType)userPreferenceRequest.FoodTypeId;
                userPreference.PreferredCuisineId = (int)(CuisineType)userPreferenceRequest.PreferredCuisineId;
                userPreference.SpiceLevelId = (int)(SpiceLevel)userPreferenceRequest.SpiceLevelId;
                userPreference.HasSweetTooth = userPreferenceRequest.HasSweetTooth;
                await _unitOfWork.Complete();
                return;
            }

            UserFoodPreference userFoodPreference = new UserFoodPreference()
            { 
              UserId = userPreferenceRequest.UserId,
              FoodTypeId = (int)(FoodType)userPreferenceRequest.FoodTypeId, 
              PreferredCuisineId = (int)(CuisineType)userPreferenceRequest.PreferredCuisineId,
              SpiceLevelId = (int)(SpiceLevel)userPreferenceRequest.SpiceLevelId,
              HasSweetTooth = userPreferenceRequest.HasSweetTooth
            };

            await _unitOfWork.UserFoodPreference.Create(userFoodPreference);
            await _unitOfWork.Complete();

        }

        public async Task<List<RolledOutMenu>> GetRolledOutMenus(DailyRolledOutMenuRequest dailyRolledOutMenuRequest)
        {
            List<RolledOutMenu> dailyMenuList = new List<RolledOutMenu>();
            var allDailyMenuItem = (await _unitOfWork.DailyMenu.GetAll())
                              .Where(m => m.IsNotificationSent == false && m.Date == dailyRolledOutMenuRequest.CurrentDate)
                              .OrderBy(m => m.Menu.MealTypeId)
                              .ToList();

            if (allDailyMenuItem.Count == 0)
            {
                throw new NoNewDailyMenuItemAddedException();
            }

            foreach (var dailyMenuItem in allDailyMenuItem)
            {
                int preferenceSocre = await GetPreferenceScore(dailyRolledOutMenuRequest.UserId, dailyMenuItem);
                RolledOutMenu dailyMenu = new RolledOutMenu()
                {
                    DailyMenuId = dailyMenuItem.Id,
                    MenuId = dailyMenuItem.MenuId,
                    PreferenceScore = preferenceSocre,
                };

                dailyMenuList.Add(dailyMenu);
            }

            dailyMenuList.OrderBy(d => d.PreferenceScore);
            foreach (var dailyMenuItem in dailyMenuList)
            {
                var menuItem = await _unitOfWork.Menu.GetMenuItemById(dailyMenuItem.MenuId, dailyRolledOutMenuRequest.CurrentDate);
                var dailyMenu = await _unitOfWork.DailyMenu.GetById(dailyMenuItem.MenuId);
                dailyMenu.IsNotificationSent = true;
                dailyMenuItem.FoodItemName = menuItem.FoodItemName;
                dailyMenuItem.MealType = menuItem.MealTypeName;
            }

            await _unitOfWork.Complete();
            return dailyMenuList;
        }
        #endregion


        private async Task<int> GetPreferenceScore(int userId, DailyMenu dailyMenu)
        {
            var userPreference = (await _unitOfWork.UserFoodPreference.GetAll()).Where(u => u.UserId == userId).FirstOrDefault();

            int preferenceScore = 0;

            if (userPreference.FoodTypeId == dailyMenu.Menu.FoodTypeId)
            {
                preferenceScore += 10;
            }
            if (userPreference.PreferredCuisineId == dailyMenu.Menu.CuisineTypeId)
            {
                preferenceScore += 5;
            }
            if (userPreference.SpiceLevelId == dailyMenu.Menu.SpiceLevelId)
            {
                preferenceScore += 3;
            }
            if (userPreference.HasSweetTooth == dailyMenu.Menu.IsSweet)
            {
                preferenceScore += 2;
            }

            return preferenceScore;
        }

    }
}
