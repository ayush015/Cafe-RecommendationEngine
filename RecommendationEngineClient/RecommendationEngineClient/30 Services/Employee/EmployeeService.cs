using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.DTO;
using RecommendationEngineClient._10_Common.Enum;

namespace RecommendationEngineClient._30_Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private RequestServices _requestServices;
        private int currentOrderId;

        public EmployeeService(RequestServices requestServices)
        {
            _requestServices = requestServices;
        }
        public async Task GetNotification(int userId)
        {
            DataObject requestData = new DataObject()
            {
                Controller = "Employee",
                Action = "GetNotification",
                Data = userId.ToString(),
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var response = await _requestServices.SendRequestAsync(jsonRequest);
            var notificationMessage = JsonConvert.DeserializeObject<NotificationResponse>(response);
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

            var requestObject = JsonConvert.SerializeObject(feedbackList);
            DataObject requestData = new DataObject()
            {
                Controller = "Employee",
                Action = "GiveFeedBack",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<BaseResponseDTO>(jsonResponse);

            BaseResponsePrint(response);

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


            var requestObject = JsonConvert.SerializeObject(orderRequest);
            DataObject requestData = new DataObject()
            {
                Controller = "Employee",
                Action = "SelectFoodItemsFromDailyMenu",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<OrderResponse>(jsonResponse);
            currentOrderId = response.OrderId;
            BaseResponsePrint(response);
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

        private void BaseResponsePrint(BaseResponseDTO response)
        {
            Console.WriteLine($"Status : {response.Status}, Message : {response.Message}\n");
        }

        private async Task<List<UserOrderMenu>> GetOrderByOrderId()
        {
            
            DataObject requestData = new DataObject()
            {
                Controller = "Employee",
                Action = "GetMenuItemByOrderId",
                Data = currentOrderId.ToString(),
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<UserOrderMenuListResponse>(jsonResponse);
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
                int rating = Convert.ToInt32(Console.ReadLine());

                if(rating < 1 || rating > 5)
                {
                    Console.WriteLine("The rating has to between 1 to 5");
                    return null;
                }

                Console.Write("\nAny Comments : ");
                string comment = CollectComments();

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

        private string CollectComments()
        {
            string userInput;
            Console.WriteLine("\n1. Very Good\n 2. Good\n3. Average\n4. Spicy\n5. Tasty\n6. Bad\n7. Cannot Recommend");
            userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int choice))
            {
                Console.WriteLine("\nInvalit Input\n");
            }

            CommentChoice commentChoice = (CommentChoice)choice;
            switch (commentChoice) 
            {
                case CommentChoice.VeryGood:
                    return CommentChoice.VeryGood.GetDescription();
                case CommentChoice.Good:
                    return CommentChoice.Good.GetDescription();               
                case CommentChoice.Average:
                    return (CommentChoice.Average.GetDescription());
                case CommentChoice.Spicy:
                    return (CommentChoice.Spicy.GetDescription());
                case CommentChoice.Tasty:
                   return CommentChoice.Tasty.GetDescription();
                case CommentChoice.Bad:
                   return CommentChoice.Bad.GetDescription();
                case CommentChoice.CannotRecommend:
                    return CommentChoice.CannotRecommend.GetDescription();
                default:
                    return string.Empty;
            }

        }
    }
}
