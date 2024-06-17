using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Logic.Admin
{
    public interface IAdminLogic
    {
        Task<int> AddMenuItem(AddMenuItemRequest addMenuItemRequest);
        Task RemoveMenuItem(int menuId);
        Task UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest);
        Task<List<MenuListViewModel>> GetMenuList();
    }
}
