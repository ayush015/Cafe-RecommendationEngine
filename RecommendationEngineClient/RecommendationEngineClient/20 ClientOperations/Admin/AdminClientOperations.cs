using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common;

namespace RecommendationEngineClient._20_ClientOperations.Admin
{
    public class AdminClientOperations : BaseClientOperations, IAdminClientOperations
    {
        public AdminClientOperations(RequestServices requestServices) : base(requestServices)
        {
        }

        #region Public methods
        public async Task GetMenuList()
        {
            var menuList = await SendRequestAsync<MenuListResponse>(ApiEndpoints.AdminController, "GetMenuList");

            if (menuList.Status.Equals(ApplicationConstants.StatusFailed))
            {
                Console.WriteLine($"{menuList.Message}");
                return;
            }

            if (menuList.MenuList == null || menuList.MenuList.Count == 0)
            {
                Console.WriteLine("Menu is Empty.");
                return;
            }

            PrintMenuList(menuList.MenuList);
        }

        public async Task AddMenuItem()
        {
            var addMenuItemRequest = AddMenuDisplay();

            if (addMenuItemRequest == null) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.AdminController, "AddMenuItem", addMenuItemRequest);
            PrintBaseResponse(response);
        }

        public async Task RemoveMenuItem()
        {
            int menuId = GetMenuIdInput();
            if (menuId == 0) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.AdminController, "RemoveMenuItem", menuId);
            PrintBaseResponse(response);
        }

        public async Task UpdateMenuItem()
        {
            var updateMenuItemRequest = UpdateMenuDisplay();
            if (updateMenuItemRequest == null) return;

            var response = await SendRequestAsync<BaseResponseDTO>(ApiEndpoints.AdminController, "UpdateMenuItem", updateMenuItemRequest);
            PrintBaseResponse(response);
        }
        #endregion

        #region Private Methods
        private AddMenuItemRequest AddMenuDisplay()
        {
            Console.WriteLine("Enter the Meal Type");
            Console.WriteLine("1. Breakfast\n2. Lunch\n3. Dinner");
            if (!int.TryParse(Console.ReadLine(), out int mealInputId) || mealInputId < 1 || mealInputId > 3)
            {
                Console.WriteLine("\nInvalid Input\n");
                return null;
            }

            Console.WriteLine("Enter Food Item");
            string foodItemInput = Console.ReadLine();
            if (string.IsNullOrEmpty(foodItemInput))
            {
                Console.WriteLine("Input cannot be null or empty");
                return null;
            }

            return new AddMenuItemRequest
            {
                FoodItemName = foodItemInput,
                MealTypeId = mealInputId
            };
        }

        private UpdateMenuItemRequest UpdateMenuDisplay()
        {
            Console.WriteLine("Menu List");
            GetMenuList().Wait();

            int menuId = GetMenuIdInput();
            if (menuId == 0) return null;

            Console.WriteLine("Enter New FoodItem name");
            string foodItemInput = Console.ReadLine();
            if (string.IsNullOrEmpty(foodItemInput))
            {
                Console.WriteLine("\nInvalid Input\n");
                return null;
            }

            Console.WriteLine("Enter the Meal Type");
            Console.WriteLine("1. Breakfast\n2. Lunch\n3. Dinner");
            if (!int.TryParse(Console.ReadLine(), out int mealInputId) || mealInputId < 1 || mealInputId > 3)
            {
                Console.WriteLine("\nInvalid Input\n");
                return null;
            }

            return new UpdateMenuItemRequest
            {
                MenuId = menuId,
                FoodItemName = foodItemInput,
                MealTypeId = mealInputId
            };
        }

        public void PrintMenuList(IEnumerable<MenuListViewModel> menuList)
        {
            Console.WriteLine($"{"MenuId",-10} {"Item Name",-30} {"MealType",-20}");
            foreach (var item in menuList)
            {
                Console.WriteLine($"{item.MenuId,-10} {item.FoodItemName,-30} {item.MealTypeName,-20}");
            }
        }

        public int GetMenuIdInput()
        {
            Console.WriteLine("Enter the menu Id:");
            if (int.TryParse(Console.ReadLine(), out int menuId) && menuId >= 0)
            {
                return menuId;
            }
            Console.WriteLine("\nInvalid Input\n");
            return 0;
        }
        #endregion
    }
}
