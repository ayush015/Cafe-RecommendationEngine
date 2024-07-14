namespace RecommendationEngineClient._20_ClientService.Admin
{
    public interface IAdminService
    {
        Task GetMenuList();
        Task AddMenuItem();
        Task RemoveMenuItem();
        Task UpdateMenuItem();
    }
}
