using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Order
{
    public class Order : GenericRepository<Models.Order>, IOrder
    {
        public Order(DbContext context) : base(context)
        {
        }
    }
}
