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

        public async Task<List<MenuListViewModel>> GetMenuList()
        {
            var result = (
                          from menu in _dbContext.Menu
                          join foodItem in _dbContext.FoodItems on menu.FoodItemId equals foodItem.Id
                          join mealType in _dbContext.MealTypes on menu.MealTypeId equals mealType.Id
                          where menu.IsDeleted == false
                          select new MenuListViewModel
                          {
                              MenuId = menu.Id,
                              FoodItemName = foodItem.FoodName,
                              FoodItemId = foodItem.Id,
                              MealTypeId = mealType.Id,
                              MealTypeName = mealType.MealTypeName,  
                          });
            return await result.ToListAsync();
        }
    }
}
