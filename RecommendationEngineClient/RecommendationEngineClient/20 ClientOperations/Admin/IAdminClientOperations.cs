namespace RecommendationEngineClient._20_ClientOperations.Admin
{
    public interface IAdminClientOperations
    {
        Task GetMenuList();
        Task AddMenuItem();
        Task RemoveMenuItem();
        Task UpdateMenuItem();
    }
}
