using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.UserFoodPreference
{
    public class UserFoodPreference : GenericRepository<Models.UserFoodPreference>, IUserFoodPreference
    {
        public UserFoodPreference(DbContext context) : base(context)
        {
        }
    }
}
