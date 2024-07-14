using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.UserOrder
{
    public interface IUserOrder : IGenericRepository<Models.UserOrder>
    {
        Task AddUserOrders(IEnumerable<Models.UserOrder> userOrders);
    }
}
