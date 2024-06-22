﻿using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.DTO;
using RecommendationEngineClient._10_Common.Enum;

namespace RecommendationEngineClient._20_Services.Employee
{
    public class EmployeeService : BaseService,IEmployeeService
    {
        private int currentOrderId;

        public EmployeeService(RequestServices requestServices) : base(requestServices) 
        {
        }

        public async Task GetNotification(int userId)
        {
            var notificationMessage = await SendRequestAsync<NotificationResponse>("Employee", "GetNotification", userId.ToString());
           
            if(notificationMessage.Status.Equals(ApplicationConstants.StatusSuccess) && !string.IsNullOrEmpty(notificationMessage.NotificationMessgae))
            {
                Console.WriteLine($"New Notification : Menu Items for Today\n{notificationMessage.NotificationMessgae}");
            }
            else if(!notificationMessage.IsNewNotification)
            {
                Console.WriteLine("No New Notifications\n");
            }
            else
            {
                Console.WriteLine($"{notificationMessage.Message}\n");
            }
            Console.ReadKey();
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

            var response = await SendRequestAsync<BaseResponseDTO>("Employee", "GiveFeedBack", feedbackList);

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

            var response = await SendRequestAsync<OrderResponse>("Employee", "SelectFoodItemsFromDailyMenu", orderRequest);

           
            currentOrderId = response.OrderId;
            PrintBaseResponse(response);
        }

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
            var response = await SendRequestAsync<UserOrderMenuListResponse>("Employee", "GetMenuItemByOrderId", currentOrderId.ToString());
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

       
    }
}