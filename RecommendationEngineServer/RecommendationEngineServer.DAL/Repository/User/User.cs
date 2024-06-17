using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.User
{
    public class User : GenericRepository<Models.User>, IUser
    {
        public User(DbContext context) : base(context)
        {
        }
    }
}
