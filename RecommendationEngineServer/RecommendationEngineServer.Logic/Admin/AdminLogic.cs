using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.UnitOfWork;

namespace RecommendationEngineServer.Logic.Admin
{
    public class AdminLogic : IAdminLogic
    {
        private IUnitOfWork _unitOfWork;
        public AdminLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Public Methods
        public async Task<int> AddMenuItem(AddMenuItemRequest addMenuItemRequest)
        {
            if(addMenuItemRequest.FoodItemName == null || addMenuItemRequest.MealTypeId == 0)
            {
                throw new ArgumentException(ApplicationConstants.InvalidMenuItem);
            }

            var foodItem = (await _unitOfWork.FoodItem.GetAll())
                           .FirstOrDefault
                            (
                            f => f.FoodName.ToLower().Equals(addMenuItemRequest.FoodItemName.ToLower())
                            );

            if (foodItem != null)
            {
                throw new MenuItemAlreadyPresentException();
            }

            FoodItem newFoodItem = new FoodItem()
            { 
              FoodName = addMenuItemRequest.FoodItemName,
            };
            var addFoodItem =  await _unitOfWork.FoodItem.Create(newFoodItem);
            await _unitOfWork.Complete();

            Menu newMenuItem = new Menu()
            {
                FoodItemId = addFoodItem.Id,
                MealTypeId = addMenuItemRequest.MealTypeId,
                IsDeleted = false,
            };

            var addMenuItem = await _unitOfWork.Menu.Create(newMenuItem);
            await _unitOfWork.Complete();
            return addMenuItem.Id;
        }

        public async Task<List<MenuListModel>> GetMenuList()
        {
            var menuList = await _unitOfWork.Menu.GetMenuList();
            return menuList;
        }

        public async Task RemoveMenuItem(int menuId)
        {
            var menuItem = await _unitOfWork.Menu.GetById(menuId);

            if (menuItem == null || menuItem.IsDeleted == true)
            {
                throw new MenuItemNotFoundException();
            }

            menuItem.IsDeleted = true;
            await _unitOfWork.Complete();
        }

        public async Task UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest)
        {
            var menuItem = await _unitOfWork.Menu.GetById(updateMenuItemRequest.MenuId);
            if (menuItem == null ||  menuItem.IsDeleted == true)

            {
                throw new MenuItemNotFoundException();
            }

            var foodItem = await _unitOfWork.FoodItem.GetById(menuItem.FoodItemId);
           
            if(updateMenuItemRequest.MealTypeId != 0)
            {
                menuItem.MealTypeId = updateMenuItemRequest.MealTypeId;
            }

            foodItem.FoodName = updateMenuItemRequest.FoodItemName;

            await _unitOfWork.Complete();

        }
        #endregion
    }
}
