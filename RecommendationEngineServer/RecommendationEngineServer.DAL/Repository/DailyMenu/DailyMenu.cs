using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.DailyMenu
{
    public class DailyMenu : GenericRepository<Models.DailyMenu>, IDailyMenu
    {
        public DailyMenu(DbContext context) : base(context)
        {
        }
    }
}
