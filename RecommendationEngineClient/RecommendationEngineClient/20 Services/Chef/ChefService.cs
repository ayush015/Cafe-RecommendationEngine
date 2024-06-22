using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationEngineClient._10_Common.DTO;

namespace RecommendationEngineClient._20_Services.Chef
{
    public class ChefService : BaseService, IChefService
    {
        public ChefService(RequestServices requestServices) : base(requestServices)
        {
        }

        #region Public Methods
        public async Task GetMenuList()
        {
            var menuList = await SendRequestAsync<RecommendedMenuResponse>(ApplicationConstants.ChefController, "GetMenuListItems");

            if (menuList.Status.Equals(ApplicationConstants.StatusFailed))
            {
                Console.WriteLine($"{menuList.Message}");
                return;
            }

            if (menuList.RecommendedMenus == null || menuList.RecommendedMenus.Count == 0)
            {
                Console.WriteLine("Menu is Empty.");
                return;
            }

            Console.WriteLine($"{"MenuId",-10} {"Item Name",-30} {"MealType",-20} {"Rating",-10}");
            foreach (var item in menuList.RecommendedMenus)
            {
                Console.WriteLine($"{item.MenuId,-10} {item.FoodItemName,-30} {item.MealTypeName,-20} {item.RecommendationScore,-10:F1}");
            }
            Console.ReadKey();
        }

        public async Task AddDailyMenuItem()
        {
            var menuItemIds = RollOutMenuDisplay();
            if (menuItemIds == null) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApplicationConstants.ChefController, "AddDailyMenuItem", menuItemIds);
            PrintBaseResponse(response);
        }

        public async Task SendNotification()
        {
            var response = await SendRequestAsync<BaseResponseDTO>(ApplicationConstants.ChefController, "SendNotification");
            PrintBaseResponse(response);
        }
        #endregion

        #region Private Methods
        private List<int> RollOutMenuDisplay()
        {
            Console.WriteLine("Enter the Number of Menu Items you want to roll out:");
            if (!int.TryParse(Console.ReadLine(), out int numberOfMenuItems))
            {
                Console.WriteLine("\nInvalid Input\n");
                return null;
            }

            List<int> menuItemsIds = new List<int>();
            Console.WriteLine("Enter the Menu Ids:");
            for (int itemsId = 0; itemsId < numberOfMenuItems; itemsId++)
            {
                if (int.TryParse(Console.ReadLine(), out int menuId))
                {
                    menuItemsIds.Add(menuId);
                }
                else
                {
                    Console.WriteLine("\nInvalid Input\n");
                    return null;
                }
            }
            return menuItemsIds;
        }
        #endregion
    }
}
