using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.MealType
{
    public class MealType : GenericRepository<Models.MealType>, IMealType
    {
        public MealType(DbContext context) : base(context)
        {
        }
    }
}
