using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._30_Services.Admin
{
    public class AdminService : IAdminService
    {
        private RequestServices _requestServices;

        public AdminService(RequestServices requestServices) 
        {
            _requestServices = requestServices;
        }

        #region Public methods
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

            Console.ReadKey();
        }

        public async Task AddMenuItem()
        {
            var addMenuItemRequest = AddMenuDisplay();

            if (addMenuItemRequest == null) return;

            var requestObject = JsonConvert.SerializeObject(addMenuItemRequest);
            DataObject requestData = new DataObject()
            {
                Controller = "Admin",
                Action = "AddMenuItem",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<BaseResponseDTO>(jsonResponse);

            BaseResponsePrint(response);       
        }

        public async Task RemoveMenuItem()
        {
            var removeMenuItemRequest = await RemoveMenuDisplay();

            if(removeMenuItemRequest == 0) return;

            var requestObject = JsonConvert.SerializeObject(removeMenuItemRequest);
            DataObject requestData = new DataObject()
            {
                Controller = "Admin",
                Action = "RemoveMenuItem",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<BaseResponseDTO>(jsonResponse);
            BaseResponsePrint(response);

        }

        public async Task UpdateMenuItem()
        {
            var updateMenuItemRequest = await UpdateMenuDisplay();
            if (updateMenuItemRequest == null) return;

            var requestObject = JsonConvert.SerializeObject(updateMenuItemRequest);
            DataObject requestData = new DataObject()
            {
                Controller = "Admin",
                Action = "UpdateMenuItem",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<MenuListResponse>(jsonResponse);
            BaseResponsePrint(response);
        }
        #endregion

        #region Private Methods
        private AddMenuItemRequest AddMenuDisplay()
        {
            string mealTypeInput;
            string foodItemInput;
            Console.WriteLine("Enter the Meal Type");
            Console.WriteLine("1. Breakfast\n 2. Lunch\n 3. Dinner");
            mealTypeInput = Console.ReadLine();

            if (!int.TryParse(mealTypeInput, out int mealInputId) || mealInputId < 0 || mealInputId > 3)
            {
                Console.WriteLine("\nInvalit Input\n");
            }

            Console.WriteLine("Enter Food Item");
            foodItemInput = Console.ReadLine();

            if (string.IsNullOrEmpty(foodItemInput))
            {
                Console.WriteLine("Input cannot be Null or empty");
                return null;
            }
            AddMenuItemRequest addMenuItemRequest = new AddMenuItemRequest()
            {
                FoodItemName = foodItemInput,
                MealTypeId = mealInputId
            };

            return addMenuItemRequest;
        }

        private async Task<int> RemoveMenuDisplay()
        {
            Console.WriteLine("Menu List");
            await GetMenuList();
            Console.WriteLine("Enter the menu Id you want to remove");
            string menuIdInput = Console.ReadLine();
            if(!int.TryParse(menuIdInput, out int menuId) || menuId < 0)
            {
                Console.WriteLine("\nInvalit Input\n");
                return 0;
            }
            return menuId;
        }

        private async Task<UpdateMenuItemRequest> UpdateMenuDisplay()
        {
            string menuIdInput;
            string mealTypeInput;
            string foodItemInput;

            Console.WriteLine("Menu List");
            await GetMenuList();

            Console.WriteLine("Enter the MenuId");
             menuIdInput = Console.ReadLine();
            if (!int.TryParse(menuIdInput, out int menuId) || menuId < 0)
            {
                Console.WriteLine("\nInvalit Input\n");
                return null;
            }

            Console.WriteLine("Enter New FoodItem name");
             foodItemInput = Console.ReadLine();
            if(string.IsNullOrEmpty(foodItemInput))
            {
                Console.WriteLine("\nInvalit Input\n");
                return null;
            }

            Console.WriteLine("Enter the Meal Type");
            Console.WriteLine("1. Breakfast\n 2. Lunch\n 3. Dinner");
            mealTypeInput = Console.ReadLine();

            if (!int.TryParse(mealTypeInput, out int mealInputId) || mealInputId < 0 || mealInputId > 3)
            {
                Console.WriteLine("\nInvalit Input\n");
            }

            return new UpdateMenuItemRequest
            {
                MenuId = menuId,
                FoodItemName = foodItemInput,
                MealTypeId = mealInputId
            };

        }

        private void BaseResponsePrint(BaseResponseDTO response)
        {
            Console.WriteLine($"Status : {response.Status}, Message : {response.Message}\n");
        }
        #endregion
    }
}
