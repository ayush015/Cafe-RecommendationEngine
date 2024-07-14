using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.UserRole
{
    public class UserRole : GenericRepository<Models.UserRole>, IUserRole
    {
        public UserRole(DbContext context) : base(context)
        {
        }
    }
}
