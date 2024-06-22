namespace RecommendationEngineClient._20_Services.Admin
{
    public interface IAdminService
    {
        Task GetMenuList();
        Task AddMenuItem();
        Task RemoveMenuItem();
        Task UpdateMenuItem();
    }
}
