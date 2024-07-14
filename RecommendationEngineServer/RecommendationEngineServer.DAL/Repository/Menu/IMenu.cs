using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Menu
{
    public interface IMenu : IGenericRepository<Models.Menu>
    {
        Task<List<MenuListModel>> GetMenuList();
        Task<DailyMenuListModel> GetMenuItemById(int menuId, DateTime currentDate);
        Task<List<UserOrderMenuModel>> GetMenuItemsByOrderId(int orderId);
        Task<RecommendedMenuModel> GetMenuDetailByMenuId(int menuId);
    }
}
