namespace RecommendationEngineClient._30_Services.Admin
{
    public interface IAdminService
    {
        Task GetMenuList();
        Task AddMenuItem();
        Task RemoveMenuItem();
        Task UpdateMenuItem();
    }
}
