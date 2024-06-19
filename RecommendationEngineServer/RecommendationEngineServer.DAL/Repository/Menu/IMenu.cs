using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Menu
{
    public interface IMenu : IGenericRepository<Models.Menu>
    {
        Task<List<MenuListViewModel>> GetMenuList();
        Task<DailyMenuListViewModel> GetMenuItemById(int menuId);
        Task<List<UserOrderMenu>> GetMenuItemsByOrderId(int orderId);
    }
}
