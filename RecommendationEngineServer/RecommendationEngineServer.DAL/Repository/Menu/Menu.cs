using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Menu
{
    public class Menu : GenericRepository<Models.Menu>, IMenu
    {
         private RecommendationEngineDBContext _dbContext;
        public Menu(DbContext context) : base(context)
        {
            _dbContext = (RecommendationEngineDBContext)context;
        }

        public async Task<List<MenuListModel>> GetMenuList()
        {
            var result = (
                          from menu in _dbContext.Menu
                          join foodItem in _dbContext.FoodItems on menu.FoodItemId equals foodItem.Id
                          join mealType in _dbContext.MealTypes on menu.MealTypeId equals mealType.Id
                          where menu.IsDeleted == false
                          select new MenuListModel
                          {
                              MenuId = menu.Id,
                              FoodItemName = foodItem.FoodName,
                              FoodItemId = foodItem.Id,
                              MealTypeId = mealType.Id,
                              MealTypeName = mealType.MealTypeName,  
                          });
            return await result.ToListAsync();
        }

        public async Task<DailyMenuListModel> GetMenuItemById(int menuId)
        {
            var result = (
                        from dailyMenu in _dbContext.DailyMenus
                        join menu in _dbContext.Menu on dailyMenu.MenuId equals menu.Id
                        join foodItem in _dbContext.FoodItems on menu.FoodItemId equals foodItem.Id
                        join mealType in _dbContext.MealTypes on menu.MealTypeId equals mealType.Id
                        where menu.IsDeleted == false && menu.Id == menuId
                        select new DailyMenuListModel
                        {
                            MenuId = menu.Id,
                            DailyMenuId = dailyMenu.Id,
                            FoodItemName = foodItem.FoodName,
                            FoodItemId = foodItem.Id,
                            MealTypeId = mealType.Id,
                            MealTypeName = mealType.MealTypeName,
                        }).FirstOrDefaultAsync();

            return  await result;
        }

        public async Task<List<UserOrderMenuModel>> GetMenuItemsByOrderId(int orderId)
        {
            var result = (
                from order in _dbContext.Orders
                join userOrders in _dbContext.UserOrders on order.Id equals userOrders.OrderId
                join dailyMenu in _dbContext.DailyMenus on userOrders.DailyMenuId equals dailyMenu.Id
                join menu in _dbContext.Menu on dailyMenu.MenuId equals menu.Id
                join foodItem in _dbContext.FoodItems on menu.FoodItemId equals foodItem.Id
                join mealType in _dbContext.MealTypes on menu.MealTypeId equals mealType.Id
                where order.Id == orderId && order.IsFeedbackGiven == false
                select new UserOrderMenuModel
                {
                    MenuId = menu.Id,
                    DailyMenuId = dailyMenu.Id,
                    FoodItemName = foodItem.FoodName,
                    FoodItemId = foodItem.Id,
                    MealTypeId = mealType.Id,
                    MealTypeName = mealType.MealTypeName,
                });
            return await result.ToListAsync();  
        }

       public async Task<RecommendedMenuModel> GetMenuDetailByMenuId(int menuId)
       {
            var result = (
                         from menu in _dbContext.Menu
                         join foodItem in _dbContext.FoodItems on menu.FoodItemId equals foodItem.Id
                         join mealType in _dbContext.MealTypes on menu.MealTypeId equals mealType.Id
                         where menu.IsDeleted == false && menu.Id == menuId
                         select new RecommendedMenuModel
                         {
                             MenuId = menu.Id,
                             FoodItemName = foodItem.FoodName,
                             FoodItemId = foodItem.Id,
                             MealTypeId = mealType.Id,
                             MealTypeName = mealType.MealTypeName,
                         }).FirstOrDefaultAsync();
            return await result;
        }
    }
}
