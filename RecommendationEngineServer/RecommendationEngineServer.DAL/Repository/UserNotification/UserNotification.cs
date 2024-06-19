using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;
using RecommendationEngineServer.DAL.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.DAL.Repository.UserNotification
{
    public class UserNotification : GenericRepository<Models.UserNotification>, IUserNotification
    {
        public UserNotification(DbContext context) : base(context)
        {
        }
    }
}
