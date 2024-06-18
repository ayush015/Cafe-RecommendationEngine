using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.DailyMenu
{
    public interface IDailyMenu : IGenericRepository<Models.DailyMenu>
    {
        Task AddDailyMenuList(IEnumerable<Models.DailyMenu> dailyMenus);
    }
}
