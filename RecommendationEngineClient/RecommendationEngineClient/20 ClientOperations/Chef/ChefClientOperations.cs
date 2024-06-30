using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationEngineClient._10_Common.DTO;

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
            await SetDateFromChef();
            var menuItem = await RollOutMenuDisplay();
            if (menuItem.MenuItemsIds == null) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApplicationConstants.ChefController, "AddDailyMenuItem", menuItem);
            PrintBaseResponse(response);
        }

        public async Task SendNotification()
        {
            var currentDate = (await DateStore.LoadDataAsync()).CurrentDate;
            var response = await SendRequestAsync<BaseResponseDTO>(ApplicationConstants.ChefController, "SendNotification", currentDate);
            PrintBaseResponse(response);
          

            
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

        private static async Task SetDateFromChef()
        {
            Console.Write("Please enter the date (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                DateDTO newdate = new DateDTO { CurrentDate = date };
                await DateStore.SaveDataAsync(newdate);
                Console.WriteLine("Date saved successfully");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }

            var currentDate = await DateStore.LoadDataAsync();

            Console.WriteLine(currentDate.CurrentDate);

        }
        #endregion
    }
}
