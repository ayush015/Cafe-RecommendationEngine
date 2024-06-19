using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.UserOrder
{
    public class UserOrder : GenericRepository<Models.UserOrder>, IUserOrder
    {
        RecommendationEngineDBContext _dbContext;
        public UserOrder(DbContext context) : base(context)
        {
            _dbContext = (RecommendationEngineDBContext)context;
        }

        public async Task AddUserOrders(IEnumerable<Models.UserOrder> userOrders)
        {
            await _dbContext.UserOrders.AddRangeAsync(userOrders);
        }
    }
}
