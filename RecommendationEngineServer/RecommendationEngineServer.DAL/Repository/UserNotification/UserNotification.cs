using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.UserNotification
{
    public class UserNotification : GenericRepository<Models.UserNotification>, IUserNotification
    {
        public UserNotification(DbContext context) : base(context)
        {
        }
    }
}
