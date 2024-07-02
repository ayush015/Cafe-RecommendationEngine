using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.DTO;
using RecommendationEngineClient._10_Common.Enum;
using RecommendationEngineClient._10_Common;

namespace RecommendationEngineClient._20_ClientOperations.Employee
{
    public class EmployeeClientOperations : BaseClientOperations,IEmployeeClientOperations
    {
        private int currentOrderId;

        public EmployeeClientOperations(RequestServices requestServices) : base(requestServices) 
        {
        }

        #region Public Method
        public async Task<int> GetNotification(int userId)
        {
            var currentDate = (await DateStore.LoadDataAsync()).CurrentDate;
            NotificationRequest notificationRequest = new NotificationRequest()
            {
                UserId = userId,
                CurrentDate = currentDate,
            };
            var notificationMessage = await SendRequestAsync<NotificationResponse>(ApiEndpoints.EmployeeController, ApiEndpoints.GetNotification, notificationRequest);

            if(notificationMessage.Status.Equals(ApplicationConstants.StatusSuccess) && !string.IsNullOrEmpty(notificationMessage.NotificationMessgae))
            {
                Console.WriteLine($"New Notification : \n{notificationMessage.NotificationMessgae}");
                if (notificationMessage.NotificationTypeId == (int)NotificationType.MenuImprovement)
                {
                  await SendMenuImprovement(userId);
                }
                else if(notificationMessage.NotificationTypeId == (int)NotificationType.NewDailyMenuItem)
                {
                    await GetRolledOutMenu(userId);
                }
                return 1;
            }
            else if(!notificationMessage.IsNewNotification)
            { 
                Console.WriteLine("No New Notifications\n");
                return 0;
            }
            else
            {
                Console.WriteLine($"{notificationMessage.Message}\n");
                return 0;
            }

            
        }

        public async Task GiveFeedBack(int userId)
        {
           var currentOrderList = await GetOrderByOrderId();
            if(currentOrderList == null || currentOrderList.Count == 0)
            {
                Console.WriteLine("No Order to take feedback");
            }
            var feedbackList = GetFeedbackDisplayMenu(userId, currentOrderList);
            if (feedbackList == null) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.EmployeeController, ApiEndpoints.GiveFeedBack, feedbackList);

            PrintBaseResponse(response);
        }

        public async Task SelectFoodItemsFromDailyMenu(int userId)
        {
            var selectedFoodItemIds = SelectFoodItemDisplayMenu();

            if (selectedFoodItemIds == null) return;

            OrderRequest orderRequest = new OrderRequest()
            { 
              DailyMenuIds = selectedFoodItemIds,
              UserId = userId
            };

            var response = await SendRequestAsync<OrderResponse>(ApiEndpoints.EmployeeController, ApiEndpoints.SelectFoodItemsFromDailyMenu, orderRequest);

           
            currentOrderId = response.OrderId;
            PrintBaseResponse(response);
        }

        public async Task AddUserFoodPreference(int userId)
        {
            var userPreference = DisplayUserPreferenceOptions(userId);

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.EmployeeController, ApiEndpoints.AddUserPreference, userPreference);
            PrintBaseResponse(response);
        }
        #endregion

        #region Private Method
        private List<int> SelectFoodItemDisplayMenu()
        {
            string numberOfMenuItemInput;

            List<int> dailyMenuIds = new List<int>();

            Console.WriteLine("Enter Number of items you want tomorrow");
            Console.Write("Enter : ");
            numberOfMenuItemInput = Console.ReadLine();
            Console.WriteLine();

            if (!int.TryParse(numberOfMenuItemInput, out int numberOfMenuItems))
            {
                Console.WriteLine("\nInvalit Input\n");
            }
            Console.WriteLine("Enter MenuIds to place the order");
            for (int itemsId = 0; itemsId < numberOfMenuItems; itemsId++)
            {
                int menuId = Convert.ToInt32(Console.ReadLine());
                dailyMenuIds.Add(menuId);
            }

            return dailyMenuIds;
        }

        private async Task<List<UserOrderMenu>> GetOrderByOrderId()
        {
            var response = await SendRequestAsync<UserOrderMenuListResponse>(ApiEndpoints.EmployeeController, ApiEndpoints.GetMenuItemByOrderId, currentOrderId.ToString());
            return response.UserOrderMenus;
        }

        private List<GiveFeedBackRequest> GetFeedbackDisplayMenu(int userId, List<UserOrderMenu> currentOrderList)
        {
            List<GiveFeedBackRequest> giveFeedBackList = new List<GiveFeedBackRequest>();
            Console.WriteLine("You can give rating form 1 to 5");
            foreach (var item in currentOrderList)
            {
                Console.WriteLine($"Give feedback for {item.FoodItemName}");
                Console.Write("Rating : ");
                string ratingInput = Console.ReadLine();

                if(!int.TryParse(ratingInput, out int rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Invalid input");
                    break;
                }

                Console.Write("\nAny Comments : ");
                string comment = Console.ReadLine();

                if(string.IsNullOrEmpty(comment) || string.IsNullOrWhiteSpace(comment))
                {
                    Console.WriteLine("Invalid input");
                    break;
                }

                GiveFeedBackRequest giveFeedBack = new GiveFeedBackRequest()
                { 
                    MenuIds = item.MenuId,
                    DailyMenuId = item.DailyMenuId,
                    UserId = userId,
                    Rating = rating,
                    Comments = comment
                };

                giveFeedBackList.Add(giveFeedBack);

            }

            return giveFeedBackList;
        }

        private async Task SendMenuImprovement(int userId)
        {
            var menuFeedbackQuestions = await SendRequestAsync<FeedbackQuestionResponse>(ApiEndpoints.EmployeeController, ApiEndpoints.GetMenuFeedBackQuestions);
            List<ImprovementFeedback> improvementFeedbackList = new List<ImprovementFeedback>();
            Console.Write("Enter Item Name : ");
            string foodItemName = Console.ReadLine();
            Console.WriteLine();
            foreach (var item in menuFeedbackQuestions.FeedbackQuestions) 
            {
                Console.Write($" Answer to Q{item.QuestionId} : ");
                string answer = Console.ReadLine();
                ImprovementFeedback improvementFeedback = new ImprovementFeedback()
                {
                     QuestionId = item.QuestionId,
                     Answer = answer,
                };
                improvementFeedbackList.Add(improvementFeedback);
            }

            MenuImprovementFeedbackRequest menuImprovementFeedbackRequest = new MenuImprovementFeedbackRequest()
            {
                FoodItemName = foodItemName,
                UserId = userId,
                ImprovementFeedbacks = improvementFeedbackList
            };

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.EmployeeController, ApiEndpoints.AddMenuImprovementFeedbacks, menuImprovementFeedbackRequest);

            PrintBaseResponse(response);
        }

        private UserPreferenceRequest DisplayUserPreferenceOptions(int userId)
        {
            Console.WriteLine("1) Please select Food Type\n1. Vegetarian\n2. Non Vegetarian\n3. Eggetarian");
            int userFoodTypePreference = GetUserInputChoice();

            Console.WriteLine("2) Please select your spice level\n1. High\n2. Medium\n3. Low");
            int userSpiceLevelPreference = GetUserInputChoice();

            Console.WriteLine("3) What do you prefer most?\n1. North Indian\n2. South Indian\n3. Others");
            int userPreferredCuisinePreference = GetUserInputChoice();

            Console.WriteLine("4) Do you have a sweet tooth\n1. Yes\n2. No");
            int userSweetToothPreference = GetUserInputChoice();

            return new UserPreferenceRequest
            {
                UserId = userId,
                FoodTypeId = userFoodTypePreference,
                SpiceLevelId = userSpiceLevelPreference,
                PreferredCuisineId = userPreferredCuisinePreference,
                HasSweetTooth = userSweetToothPreference == 1 ? true : false,
            };
        }

        private async Task GetRolledOutMenu(int userId)
        {
            var currentDate = (await DateStore.LoadDataAsync()).CurrentDate;
            DailyRolledOutMenuRequest dailyRolledOutMenuRequest = new DailyRolledOutMenuRequest()
            {
                CurrentDate = currentDate,
                UserId = userId,
            };
            var response = await SendRequestAsync<DailyRolledOutMenuResponse>(ApiEndpoints.EmployeeController, ApiEndpoints.GetDailyRolledOutMenu, dailyRolledOutMenuRequest);
            PrintBaseResponse(response);
            printDailyRolledOutMenu(response.RolledOutMenu);
        }

        private void printDailyRolledOutMenu(List<RolledOutMenu> rolledOutMenu)
        {
            Console.WriteLine("{0,-20} {1,-15} {2,-15}", "Food Item Name", "Daily Menu Id", "Meal Type");
            Console.WriteLine(new string('-', 60));

            foreach (var menu in rolledOutMenu)
            {
                Console.WriteLine("{0,-20} {1,-15} {2,-15}", menu.FoodItemName, menu.DailyMenuId, menu.MealType);
            }
        }
        #endregion

    }
}
