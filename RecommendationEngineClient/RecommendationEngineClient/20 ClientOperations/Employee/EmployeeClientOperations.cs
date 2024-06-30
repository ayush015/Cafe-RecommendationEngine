using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.DTO;
using RecommendationEngineClient._10_Common.Enum;

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
            var notificationMessage = await SendRequestAsync<NotificationResponse>(ApplicationConstants.EmployeeController, "GetNotification", notificationRequest);
           
            if(notificationMessage.Status.Equals(ApplicationConstants.StatusSuccess) && !string.IsNullOrEmpty(notificationMessage.NotificationMessgae))
            {
                Console.WriteLine($"New Notification : Menu Items for Today\n{notificationMessage.NotificationMessgae}");
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

            var response = await SendRequestAsync<BaseResponseDTO>(ApplicationConstants.EmployeeController, "GiveFeedBack", feedbackList);

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

            var response = await SendRequestAsync<OrderResponse>(ApplicationConstants.EmployeeController, "SelectFoodItemsFromDailyMenu", orderRequest);

           
            currentOrderId = response.OrderId;
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
            var response = await SendRequestAsync<UserOrderMenuListResponse>(ApplicationConstants.EmployeeController, "GetMenuItemByOrderId", currentOrderId.ToString());
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
        #endregion

    }
}
