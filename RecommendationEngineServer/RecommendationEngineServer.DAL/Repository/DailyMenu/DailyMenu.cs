using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.DailyMenu
{
    public class DailyMenu : GenericRepository<Models.DailyMenu>, IDailyMenu
    {
        private RecommendationEngineDBContext _dbContext;
        public DailyMenu(DbContext context) : base(context)
        {
            _dbContext = (RecommendationEngineDBContext)context;
        }

        public async Task AddDailyMenuList(IEnumerable<Models.DailyMenu> dailyMenus)
        {
            await _dbContext.DailyMenus.AddRangeAsync(dailyMenus);
        }
    }
}
