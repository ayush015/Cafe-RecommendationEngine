using Newtonsoft.Json;
using RecommendationEngineClient._30_Services.Chef;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;

namespace RecommendationEngineClient._30_Services
{
    public class ChefService : IChefService
    {
        private RequestServices _requestServices;

        public ChefService(RequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        #region Public Methods
        public async Task GetMenuList()
        {
            DataObject requestData = new DataObject()
            {
                Controller = "Admin",
                Action = "GetMenuList",
                Data = null
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var response = await _requestServices.SendRequestAsync(jsonRequest);
            var menuList = JsonConvert.DeserializeObject<MenuListResponse>(response);

            if (menuList.Status.Equals(ApplicationConstants.StatusFailed))
            {
                Console.WriteLine($"{menuList.Message}");
                return;
            }

            if (menuList.MenuList != null && menuList.MenuList.Count == 0)
            {
                Console.WriteLine($"Menu is Empty.");
            }

            foreach (var item in menuList.MenuList)
            {
                Console.WriteLine($"{item.MenuId}\t {item.FoodItemName}\t {item.MealTypeName}");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public Task GetRecommendedMenuList()
        {
            throw new NotImplementedException();
        }

        public async Task AddDailyMenuItem()
        {
            var menuItemIds = RollOutMenuDisplay();

            if (menuItemIds == null) return;

            var requestObject = JsonConvert.SerializeObject(menuItemIds);
            DataObject requestData = new DataObject()
            {
                Controller = "Chef",
                Action = "AddDailyMenuItem",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<BaseResponseDTO>(jsonResponse);

            BaseResponsePrint(response);
        }

        public async Task SendNotification()
        {
           
            DataObject requestData = new DataObject()
            {
                Controller = "Chef",
                Action = "SendNotification",
                Data = null
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<BaseResponseDTO>(jsonResponse);

            BaseResponsePrint(response);
        }
        #endregion

        #region Private Methods
        private List<int> RollOutMenuDisplay()
        {
            string numberOfMenuItemInput;

            List<int> menuItemsIds = new List<int>();

            Console.WriteLine("Enter the Number of Menu Items you want to roll out");
            numberOfMenuItemInput = Console.ReadLine();

            if (!int.TryParse(numberOfMenuItemInput, out int numberOfMenuItems))
            {
                Console.WriteLine("\nInvalit Input\n");
                return null;
            }

            Console.WriteLine("Enter the Menu Ids");
            for(int itemsId = 0; itemsId < numberOfMenuItems; itemsId++)
            {
                int menuId = Convert.ToInt32(Console.ReadLine());
                menuItemsIds.Add(menuId);
            }

            return menuItemsIds;
        }

        private void BaseResponsePrint(BaseResponseDTO response)
        {
            Console.WriteLine($"Status : {response.Status}, Message : {response.Message}\n");
        }
        #endregion
    }
}
