using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.UserOrder
{
    public class UserOrder : GenericRepository<Models.UserOrder>, IUserOrder
    {
        public UserOrder(DbContext context) : base(context)
        {
        }
    }
}
