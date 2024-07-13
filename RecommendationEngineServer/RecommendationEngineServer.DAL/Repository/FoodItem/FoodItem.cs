using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;
using RecommendationEngineServer.DAL.Repository.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.DAL.Repository.FoodItem
{
    public class FoodItem : GenericRepository<Models.FoodItem>, IFoodItem
    {
        public FoodItem(DbContext context) : base(context)
        {
        }
    }
}
