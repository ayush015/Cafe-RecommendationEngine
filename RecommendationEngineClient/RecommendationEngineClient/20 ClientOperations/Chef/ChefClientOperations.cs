using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationEngineClient._10_Common.DTO;
using RecommendationEngineClient._10_Common.Enum;
using RecommendationEngineClient._10_Common;

namespace RecommendationEngineClient._20_ClientOperations.Chef
{
    public class ChefClientOperations : BaseClientOperations, IChefClientOperations
    {
        public ChefClientOperations(RequestServices requestServices) : base(requestServices)
        {
        }

        #region Public Methods
        public async Task GetMenuList()
        {
            var menuList = await SendRequestAsync<RecommendedMenuResponse>(ApiEndpoints.ChefController, "GetMenuListItems");

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
            var menuItem = await RollOutMenuDisplay();
            if (menuItem.MenuItemsIds == null) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.ChefController, "AddDailyMenuItem", menuItem);
            PrintBaseResponse(response);
        }

        public async Task SendNotification()
        {
            var currentDate = (await DateStore.LoadDataAsync()).CurrentDate;
            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.ChefController, "SendNotification", currentDate);
            PrintBaseResponse(response);
          

            
        }

        public async Task GetMonthlyNotification()
        {
            var currentDate = (await DateStore.LoadDataAsync()).CurrentDate;
            var response = await SendRequestAsync<DiscardedMenuResponse>(ApiEndpoints.NotificationController, "GetMonthlyNotification", currentDate);
            DisplayDiscardedMenuItems(response.DiscardedMenus);

            if(response.DiscardedMenus.Count > 0)
            {
               await DisplayActionsForDiscardedMenuItems();
            }
        }
        #endregion

        #region Private Methods


        private async Task<MenuItem> RollOutMenuDisplay()
        {
            Console.WriteLine("Enter the Number of Menu Items you want to roll out:");
            if (!int.TryParse(Console.ReadLine(), out int numberOfMenuItems))
            {
                Console.WriteLine("\nInvalid Input\n");
                return null;
            }

            MenuItem menuItem = new MenuItem();

            menuItem.CurrentDate = (await DateStore.LoadDataAsync()).CurrentDate;

            menuItem.MenuItemsIds = new List<int>();
            Console.WriteLine("Enter the Menu Ids:");
            for (int itemsId = 0; itemsId < numberOfMenuItems; itemsId++)
            {
                if (int.TryParse(Console.ReadLine(), out int menuId))
                {
                    menuItem.MenuItemsIds.Add(menuId);
                }
                else
                {
                    Console.WriteLine("\nInvalid Input\n");
                    return null;
                }
            }
            return menuItem;
        }

       

        private void DisplayDiscardedMenuItems(List<RecommendedMenu> recommendedMenus)
        {
            if(recommendedMenus.Count == 0) { return; }

            Console.WriteLine("\nNew Notification");
            Console.WriteLine("Time to review some menu Items with low rating\n");
            Console.WriteLine($"{"MenuId",-10} {"Item Name",-30} {"MealType",-20} {"Rating",-10}\n");
            foreach (var item in recommendedMenus)
            {
                Console.WriteLine($"{item.MenuId,-10} {item.FoodItemName,-30} {item.MealTypeName,-20} {item.RecommendationScore,-10:F1}\n");
            }
        }


        private async Task DisplayActionsForDiscardedMenuItems()
        {
            Console.WriteLine("Choose an action for DiscardedMenuItem");
            while (true)
            {
                Console.WriteLine("1. Discard a menu Item\n2. Improve a menu Item\n");
                int choice = GetUserInputChoice();
                if(choice == (int)DiscardedMenuChoice.DiscardedMenuItem)
                {
                    Console.WriteLine("Enter the Menu Id\n");
                    int menuId = GetUserInputChoice();
                    await DisacrdMenuItem(menuId);
                    break;
                }
                else if(choice == (int)DiscardedMenuChoice.ImproveMenuItem)
                {
                    Console.WriteLine("Enter the Menu Id\n");
                    int menuId = GetUserInputChoice();
                    await SendNotificationForImprovingMenuItem(menuId);
                    break;

                }
                else
                {
                    await Console.Out.WriteLineAsync("Invalid Choice");
                }
            }
        }

        private async Task DisacrdMenuItem(int menuId)
        {
            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.ChefController, "DiscardMenu", menuId);
            PrintBaseResponse(response);

        }

        private async Task SendNotificationForImprovingMenuItem(int menuId)
        {
            var currentDate = (await DateStore.LoadDataAsync()).CurrentDate;
            MenuImprovementNotification menuImprovementNotification = new MenuImprovementNotification()
            {
                 CurrentDate = currentDate,
                  MenuId = menuId
            };
            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.NotificationController, "AddNewNotificationForDiscardedMenuFeedback", menuImprovementNotification);
            PrintBaseResponse(response);
        }
        #endregion
    }
}
