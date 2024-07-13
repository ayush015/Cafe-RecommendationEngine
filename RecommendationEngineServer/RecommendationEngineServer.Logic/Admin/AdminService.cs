using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Enum;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.UnitOfWork;

namespace RecommendationEngineServer.Service.Admin
{
    //public class AdminService : IAdminService
    //{
    //    private IUnitOfWork _unitOfWork;
    //    public AdminService(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    #region Public Methods
    //    public async Task<int> AddMenuItem(AddMenuItemRequest addMenuItemRequest)
    //    {
    //        if(addMenuItemRequest.FoodItemName == null || addMenuItemRequest.MealTypeId == 0)
    //        {
    //            throw new ArgumentException(ApplicationConstants.InvalidMenuItem);
    //        }

    //        var foodItem = (await _unitOfWork.FoodItem.GetAll())
    //                       .FirstOrDefault
    //                        (
    //                        f => f.FoodName.ToLower().Equals(addMenuItemRequest.FoodItemName.ToLower())
    //                        );

    //        var IsMenuItemPresent = foodItem != null ? (await _unitOfWork.Menu.GetAll()).Any(m => m.FoodItemId == foodItem.Id && m.MealTypeId == addMenuItemRequest.MealTypeId) : false;

    //        if (IsMenuItemPresent)
    //        {
    //            throw new MenuItemAlreadyPresentException();
    //        }
    //        else if(foodItem != null && foodItem.IsDeleted && !IsMenuItemPresent)
    //        {
    //            foodItem.IsDeleted = false;
    //            await _unitOfWork.Complete(); 
    //            return foodItem.Id;
    //        }
    //        else
    //        {
    //            FoodItem newFoodItem = new FoodItem()
    //            {
    //                FoodName = addMenuItemRequest.FoodItemName,
    //            };
    //            var addFoodItem = await _unitOfWork.FoodItem.Create(newFoodItem);
    //            await _unitOfWork.Complete();

    //            Menu newMenuItem = new Menu()
    //            {
    //                FoodItemId = addFoodItem.Id,
    //                MealTypeId = addMenuItemRequest.MealTypeId,
    //                CuisineTypeId =(int)(CuisineType)addMenuItemRequest.CuisineId,
    //                FoodTypeId = (int)(FoodType)addMenuItemRequest.FoodTypeId,
    //                SpiceLevelId = (int)(SpiceLevel)addMenuItemRequest.SpiceLevelId,
    //                IsSweet = addMenuItemRequest.IsSweet,
    //                IsDeleted = false,
    //            };

    //            var addMenuItem = await _unitOfWork.Menu.Create(newMenuItem);
    //            await _unitOfWork.Complete();
    //            return addMenuItem.Id;
    //        }
    //    }

    //    public async Task<List<MenuListModel>> GetMenuList()
    //    {
    //        var menuList = await _unitOfWork.Menu.GetMenuList();
    //        return menuList;
    //    }

    //    public async Task RemoveMenuItem(int menuId)
    //    {
    //        var menuItem = await _unitOfWork.Menu.GetById(menuId);

    //        if (menuItem == null || menuItem.IsDeleted == true)
    //        {
    //            throw new MenuItemNotFoundException();
    //        }

    //        menuItem.IsDeleted = true;
    //        await _unitOfWork.Complete();
    //    }

    //    public async Task UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest)
    //    {
    //        var menuItem = await _unitOfWork.Menu.GetById(updateMenuItemRequest.MenuId);
    //        if (menuItem == null ||  menuItem.IsDeleted == true)

    //        {
    //            throw new MenuItemNotFoundException();
    //        }

    //        var foodItem = await _unitOfWork.FoodItem.GetById(menuItem.FoodItemId);

    //        if(updateMenuItemRequest.MealTypeId != 0)
    //        {
    //            menuItem.MealTypeId = updateMenuItemRequest.MealTypeId;
    //        }

    //        foodItem.FoodName = updateMenuItemRequest.FoodItemName;

    //        await _unitOfWork.Complete();

    //    }
    //    #endregion
    //}

    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Public Methods

        public async Task<int> AddMenuItem(AddMenuItemRequest addMenuItemRequest)
        {
            if (addMenuItemRequest.FoodItemName == null || addMenuItemRequest.MealTypeId == 0)
            {
                throw new ArgumentException(ApplicationConstants.InvalidMenuItem);
            }

            var foodItem = await GetExistingFoodItem(addMenuItemRequest.FoodItemName);
            await CheckIfMenuItemExists(addMenuItemRequest, foodItem);

            if (foodItem != null && foodItem.IsDeleted)
            {
                return await ReactivateFoodItem(foodItem);
            }

            return await CreateNewMenuItem(addMenuItemRequest, foodItem);
        }

        public async Task<List<MenuListModel>> GetMenuList()
        {
            return await _unitOfWork.Menu.GetMenuList();
        }

        public async Task RemoveMenuItem(int menuId)
        {
            var menuItem = await _unitOfWork.Menu.GetById(menuId);

            if (menuItem == null || menuItem.IsDeleted)
            {
                throw new MenuItemNotFoundException();
            }

            menuItem.IsDeleted = true;
            await _unitOfWork.Complete();
        }

        public async Task UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest)
        {
            var menuItem = await _unitOfWork.Menu.GetById(updateMenuItemRequest.MenuId);

            if (menuItem == null || menuItem.IsDeleted)
            {
                throw new MenuItemNotFoundException();
            }

            await UpdateFoodItemName(menuItem.FoodItemId, updateMenuItemRequest.FoodItemName);
            if (updateMenuItemRequest.MealTypeId != 0)
            {
                menuItem.MealTypeId = updateMenuItemRequest.MealTypeId;
            }
            await _unitOfWork.Complete();
        }

        #endregion

        #region Private Methods

        private async Task<FoodItem> GetExistingFoodItem(string foodItemName)
        {
            return (await _unitOfWork.FoodItem.GetAll())
                .FirstOrDefault(f => f.FoodName.ToLower().Equals(foodItemName.ToLower()));
        }

        private async Task CheckIfMenuItemExists(AddMenuItemRequest request, FoodItem foodItem)
        {
            var isMenuItemPresent = foodItem != null
                ? (await _unitOfWork.Menu.GetAll())
                    .Any(m => m.FoodItemId == foodItem.Id && m.MealTypeId == request.MealTypeId)
                : false;

            if (isMenuItemPresent)
            {
                throw new MenuItemAlreadyPresentException();
            }
        }

        private async Task<int> ReactivateFoodItem(FoodItem foodItem)
        {
            foodItem.IsDeleted = false;
            await _unitOfWork.Complete();
            return foodItem.Id;
        }

        private async Task<int> CreateNewMenuItem(AddMenuItemRequest request, FoodItem foodItem)
        {
            if (foodItem == null)
            {
                foodItem = new FoodItem
                {
                    FoodName = request.FoodItemName
                };
                foodItem = await _unitOfWork.FoodItem.Create(foodItem);
                await _unitOfWork.Complete();
            }

            var newMenuItem = new Menu
            {
                FoodItemId = foodItem.Id,
                MealTypeId = request.MealTypeId,
                CuisineTypeId = (int)(CuisineType)request.CuisineId,
                FoodTypeId = (int)(FoodType)request.FoodTypeId,
                SpiceLevelId = (int)(SpiceLevel)request.SpiceLevelId,
                IsSweet = request.IsSweet,
                IsDeleted = false
            };

            var addedMenuItem = await _unitOfWork.Menu.Create(newMenuItem);
            await _unitOfWork.Complete();
            return addedMenuItem.Id;
        }

        private async Task UpdateFoodItemName(int foodItemId, string newFoodName)
        {
            var foodItem = await _unitOfWork.FoodItem.GetById(foodItemId);
            foodItem.FoodName = newFoodName;
        }
        #endregion
    }
}
