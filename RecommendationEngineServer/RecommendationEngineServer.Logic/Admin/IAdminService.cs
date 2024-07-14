using RecommendationEngineServer.Common.DTO;

namespace RecommendationEngineServer.Service.Admin
{
    public interface IAdminService
    {
        Task<int> AddMenuItem(AddMenuItemRequest addMenuItemRequest);
        Task RemoveMenuItem(int menuId);
        Task UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest);
        Task<List<MenuListModel>> GetMenuList();
    }
}
