using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.FoodItem
{
    public class FoodItem : GenericRepository<Models.FoodItem>, IFoodItem
    {
        public FoodItem(DbContext context) : base(context)
        {
        }
    }
}
