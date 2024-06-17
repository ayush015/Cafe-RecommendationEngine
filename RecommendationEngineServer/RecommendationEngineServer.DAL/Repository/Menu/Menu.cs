using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Menu
{
    public class Menu : GenericRepository<Models.Menu>, IMenu
    {
    
        public Menu(DbContext context) : base(context)
        {
        }
    }
}
